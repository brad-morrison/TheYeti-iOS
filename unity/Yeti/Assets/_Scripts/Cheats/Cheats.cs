using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour
{
    GameMaster GM;
    int counterLeft;
    int counterRight;
    string cheatCode_unlockCostumes = "LLRRLLRR";
    string currentCheat;

    private void Start() {
        GM = GameObject.Find("#GameMaster").GetComponent<GameMaster>();
    }

    public void LeftTick()
    {
        //counterLeft++;
        currentCheat += "L";
        print(currentCheat);
        CheatCheck();
    }

    public void RightTick()
    {
        //counterRight++;
        currentCheat += "R";
        print(currentCheat);
        CheatCheck(); 
    }

    public void CheatCheck() 
    {
        if (currentCheat == cheatCode_unlockCostumes)
        {
            GM.playerData.SetGlobalUnlockCheat(true);
        }
    }

    // at the moment this is the only cheat, in the next update
    // I will implement a timer coroutine to reset the cheat string
    // if the user has stopped pressing the cheat buttons.

    
}
