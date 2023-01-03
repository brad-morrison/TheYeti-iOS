using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : MonoBehaviour
{
    Renderer renderer;
    public bool animate;
    public GameObject manager;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * manager.GetComponent<GameManager>().lifeBar_ScrollSpeed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
