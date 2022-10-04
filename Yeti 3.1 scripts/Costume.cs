using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Costume : MonoBehaviour
{
    public Sprite[] sprites = new Sprite[6];

    public string name;
    public int unlockScore, unlockKills;
    public bool isUnlocked;

    public Costume(Sprite[] _sprites, string _name, int _unlockScore, int _unlockKills, bool _isUnlocked)
    {
        sprites = _sprites;
        name = _name;
        unlockScore = _unlockScore;
        unlockKills = _unlockKills;
        isUnlocked = _isUnlocked;
    }

    
}
