using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scrolls the texture on the lifebar object
//

public class TextureScroll : TheYeti
{
    Renderer renderer;
    public bool animate;

    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.time * GM.gameManager.lifeBar_ScrollSpeed;
        renderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
