using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip bleep, crown, emote, gameOver, goldFlying, 
                    goldTapped, goldPoint, goldUp, goldDown, highScoreAlert, 
                    hit, punchLarge, punchSmall, uiDrop;
    AudioSource sound;
    public int on = 1;
    // Start is called before the first frame update
    void Awake()
    {
        sound = GetComponent<AudioSource>();
        on = PlayerPrefs.GetInt("sfx", 1);
    }

    public void Play(AudioClip clip)
    {
        if(on == 1)
            sound.PlayOneShot(clip);
    }

    IEnumerator PlayCoroutine(AudioClip clip)
    {
        sound.PlayOneShot(clip);
        Debug.Log("playing sfx - " + clip);
        yield return new WaitForSeconds(clip.length);
        yield return null;
    }
}
