using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreAnimations : MonoBehaviour
{
    public GameObject skull1, skull2, bone;
    float limitLeftX = -2.73f;
    float limitRightX = 2.61f;
    float spawnY = 8.13f;
    public float spawnRate = 0.5f;
    public bool spawning = false;


    // Start is called before the first frame update
    void Start()
    {
        //StartSpawns();
    }

    public void StartSpawns()
    {
        spawning = true;
        StartCoroutine(Spawner());
    }

    public void StopSpawns()
    {
        spawning = false;
        StopCoroutine(Spawner());
    }

    public GameObject RandomObject()
    {
        int roll = Random.Range(1, 4); //random - possible outputs [1, 2, 3]
        switch(roll){
            case 1:
                return skull1;
                break;
            case 2:
                return skull2;
                break;
            case 3:
                return bone;
                break;
        }
        return null;
    }
    public void Spawn()
    {
        Instantiate(RandomObject(), SpawnPosition(), Quaternion.identity);
    }

    public Vector3 SpawnPosition()
    {
        float xPosition = Random.Range(limitLeftX, limitRightX);
        return new Vector3(xPosition, spawnY, 1);
    }

    IEnumerator Spawner()
    {
        while (spawning)
        {
            Spawn();
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
