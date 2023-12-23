using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleButton : MonoBehaviour
{
    public string name;
    public Cheats cheats;

    private void Start() {
        cheats = this.gameObject.GetComponentInParent<Cheats>();
        name = this.gameObject.name;
    }

    private void OnMouseDown() {
        if (name == "left")
            cheats.LeftTick();
        else 
            cheats.RightTick();
    }
}
