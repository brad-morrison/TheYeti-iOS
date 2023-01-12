using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScroll : TheYeti
{
    Renderer renderer;
    public bool animate;

    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * GM.gameManager.lifeBar_ScrollSpeed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
