using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneSpawner : MonoBehaviour
{
    [Header("Spawner")]
    public bool spawn;
    public GameObject skull1;
    public GameObject skull2;
    public GameObject bone;
    public float spawnDistanceX;
    public float spawnRate;
    public float fallSpeed;
    [Header("Bones")]
    public float aliveTime;
    public float scale;
    public bool collide;
    public enum colorChoice
    {
        light,
        medium,
        dark
    };
    public colorChoice color;

    private void Start()
    {
        spawn = false;
        if (spawnRate != 0)
            StartCoroutine(SpawnIn());
    }

    public void Switch()
    {
        spawn = !spawn;
    }

    public GameObject RandomObj()
    {
        GameObject random;
        int roll = Random.Range(1, 4);

        if (roll == 1) return skull1;
        if (roll == 2) return skull2;
        if (roll == 3) return bone;

        return null;
    }

    public float randomXValue()
    {
        return Random.Range(0 - spawnDistanceX, spawnDistanceX);
    }

    public void Spawn()
    {
        GameObject obj = Instantiate(RandomObj(), new Vector3(
            randomXValue(),
            gameObject.transform.position.y, 0),
            Quaternion.identity);

        obj.GetComponent<DestroyGameObject>().DestroySelfAfter(aliveTime);
        obj.GetComponent<BoxCollider2D>().enabled = collide;
        obj.transform.localScale = new Vector3(scale, scale, scale);
        obj.GetComponent<Rigidbody2D>().gravityScale = fallSpeed;
        switch (color)
        {
            case colorChoice.light:
                obj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 255);
                break;
            case colorChoice.medium:
                obj.GetComponent<SpriteRenderer>().color = new Color(0.9f, 0.9f, 0.9f, 255);
                break;
            case colorChoice.dark:
                obj.GetComponent<SpriteRenderer>().color = new Color(0.7f, 0.7f, 0.7f, 255);
                break;
        }
        obj.transform.parent = gameObject.transform;
    }

    public IEnumerator SpawnIn()
    {
        yield return new WaitForSeconds(1/spawnRate);
        if (spawn) Spawn();
        StartCoroutine(SpawnIn());
    }
}
