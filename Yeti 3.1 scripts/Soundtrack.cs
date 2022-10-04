using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour 
{
    public GameObject soundtrackPrefab;

    // Start is called before the first frame update
    void Start()
    {
        create();
    }

    public void create()
    {
        if(GameObject.Find("Soundtrack") == false)
        {
            GameObject soundtrack = Instantiate(soundtrackPrefab, new Vector3(0,0,0), Quaternion.identity);
            DontDestroyOnLoad(soundtrack);
        }
    }
}
