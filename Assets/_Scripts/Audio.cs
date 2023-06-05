using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;

public class Audio : TheYeti {
    public AudioSource source;
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

    private void Start() {
        source = GetComponent<AudioSource>();
    }

    // set music on or off
    public void Music(bool value)
    {
        source.mute = !value;
    }

    // play sound once
    public void PlaySound(AudioClip sound) {
        if (sfxOn)
            source.PlayOneShot(sound);
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
            source.PlayOneShot(sound);
    }
}