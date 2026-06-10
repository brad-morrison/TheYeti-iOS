using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banner : MonoBehaviour
{
    public GameObject banner, text, back;
    GameObject target;
    public Vector3 offScreenPos;
    public float moveTime, scaleTime, waitTime;
    // Start is called before the first frame update
    
    private void Awake() {
        target = GameObject.Find("[target] banner");
    }

    void Start()
    {
        MoveIn();
        ScaleUp();
        StartCoroutine(moveOffAfter(waitTime));
        
    }

    public void MoveIn()
    {
        iTween.MoveTo(banner, iTween.Hash("speed",moveTime,"easeType",iTween.EaseType.easeOutCubic, "position",target.transform.position));
    }

    public void MoveOff()
    {
        iTween.MoveTo(banner, iTween.Hash("speed",moveTime,"easeType",iTween.EaseType.easeOutCubic, "position",offScreenPos));
    }

    public void ScaleUp()
    {
        iTween.ScaleTo(back, new Vector3(back.transform.localScale.x ,0.9f,back.transform.localScale.z), scaleTime);
    }

    public void ScaleDown()
    {
        iTween.ScaleTo(back, new Vector3(back.transform.localScale.x ,0,back.transform.localScale.z), scaleTime);
    }

    public IEnumerator moveOffAfter(float time)
    {
        yield return new WaitForSeconds(time);
        MoveOff();
        ScaleDown();
    }
}
