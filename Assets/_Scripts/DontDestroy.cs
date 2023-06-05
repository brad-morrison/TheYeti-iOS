using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Utility class to stop object being destroyed on scene change
public class DontDestroy : MonoBehaviour
{
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
