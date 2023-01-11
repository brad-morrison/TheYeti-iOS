using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTester : TheYeti
{
    private void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            GM.audio.PlaySound(GM.audio.success);
        }

        if (Input.GetKeyDown("g"))
        {
            GM.Msg();
        }
    }
}
