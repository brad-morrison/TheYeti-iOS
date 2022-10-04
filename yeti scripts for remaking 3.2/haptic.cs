using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class haptic : MonoBehaviour
{
    int counter = 0;

    private void OnMouseDown() {
        if (counter == 6)
            counter = 0;

        iOSHapticFeedback.Instance.Trigger((iOSHapticFeedback.iOSFeedbackType)counter);

        counter++;
    }
}
