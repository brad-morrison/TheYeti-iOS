using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;

public class Audio : TheYeti {
    // two audio sources
    public AudioSource source_music, source_sfx;
    // yeti sounds
    public AudioClip punchSmall, punchLarge;
    // game event sounds
    public AudioClip gameOver, goldModeFace, goldModeStart, goldModeEnd, crown, high_score;
    // others
    public AudioClip hit, pop, coin;
    public AudioClip buttonDown, buttonUp, buttonUp_grey;
    public AudioClip hikerDeath1, hikerDeath2, hikerDeath3;
    public AudioClip frenzyCounter, frenzyStart1, frenzyStart2, frenzyStart3, frenzyEnd;
    public AudioClip timerTick, timerLow;
    public AudioClip error, timer_warning;
    public bool sfxOn;

    public void Start() {
        Debug.Log("started audio script");
        Music(GM.playerData.GetMusic());
        if (GM.playerData.sfxOn == 1)
        {
            sfxOn = true;
        }
        else
        {
            sfxOn = false;
        }
    }

    // set music on or off
    public void Music(bool value)
    {
        Debug.Log("turning music " + value);
        source_music.mute = !value;
    }

    // play sound once
    public void PlaySound(AudioClip sound) {
        if (sfxOn)
            source_sfx.PlayOneShot(sound);
    }

    // play sound after delay in seconds
    public void PlaySoundAfter(AudioClip sound, float delay) {
        if (sfxOn)
            StartCoroutine(_PlaySoundAfter(sound, delay));
    }

    
    // Coroutines
    // ----------

    IEnumerator _PlaySoundAfter(AudioClip sound, float delay) {
        yield return new WaitForSeconds(delay);
        if (sfxOn)
            source_sfx.PlayOneShot(sound);
    }
}