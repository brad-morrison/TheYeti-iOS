using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Actions
{
    public static Action<string> onButtonPressed;

    public static void ButtonPressed(string function)
    {
        onButtonPressed?.Invoke(function);
    }
}

