using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Scriptable object for each Costume
[CreateAssetMenu(fileName = "New Costume", menuName = "Costume")]
public class Costume : ScriptableObject {
    public string name;
    public int best;
    public int kills;
    public Sprite both, idle1, idle2, left, right, dead;
}