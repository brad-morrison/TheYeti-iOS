using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldFace : MonoBehaviour
{
    GameController gameController;
    Audio audio;
    public float speedVertical;
    public float distanceVertical;
    public int side = 0;
    string parentDirection;

    // Start is called before the first frame update
    void Start()
    {

        gameController = GameObject.Find("scripts").GetComponent<GameController>();
        audio = GameObject.Find("Audio").GetComponent<Audio>();
        parentDirection = gameObject.transform.parent.GetComponent<MoveObject>().direction;
        if(audio.on == 0)
            gameObject.GetComponent<AudioSource>().enabled = false;
        MoveToAndBack();
    }

    void MoveToAndBack()
    {
        iTween.MoveTo(gameObject, iTween.Hash("speed",speedVertical,"easeType",iTween.EaseType.easeInOutSine, "loopType", iTween.LoopType.pingPong, "isLocal", true, "position",new Vector3(transform.localPosition.x, transform.localPosition.y + distanceVertical,transform.localPosition.z)));
    }

    private void OnMouseDown() {
        gameController.StartGoldMode();
        Destroy(gameObject);
    }

    private void Update() {
        if(parentDirection == "left" && transform.parent.position.x < -9f)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }

        if(parentDirection == "right" && transform.parent.position.x > 2.35f)
        {
            Destroy(gameObject.transform.parent.gameObject);
        }

        if(gameController.dead == true)
        {
            Destroy(this.gameObject);
            Debug.Log("destroying face becuase dead");
        }
    }

    
}
