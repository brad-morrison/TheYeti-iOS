using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utilities : GameElement
{
    
        public IEnumerator WaitAndDestroy(float seconds, GameObject obj)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(obj);
        }

}
