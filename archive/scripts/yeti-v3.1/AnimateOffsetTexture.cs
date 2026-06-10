using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateOffsetTexture : MonoBehaviour
{
    Renderer rend;
    public float scrollSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rend = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}
