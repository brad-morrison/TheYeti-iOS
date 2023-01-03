using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Costume", menuName = "Costume")]
public class Costume : ScriptableObject {
    public string name;
    public int best;
    public int kills;
    public Sprite both, left, right, dead;
}