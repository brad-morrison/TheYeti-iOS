using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using DG.Tweening.Core.Easing;

public class Audio : MonoBehaviour {
    public AudioSource source;
    // yeti sounds
    public AudioClip punchSmall, punchLarge;
    // game event sounds
    public AudioClip gameOver, goldModeFace, goldModeStart, goldModeEnd, crown;
    // others
    public AudioClip hit, pop, coin;
    public AudioClip buttonDown, buttonUp, buttonUp_grey;
    public AudioClip hikerDeath1, hikerDeath2, hikerDeath3;
    public AudioClip frenzyStart1, frenzyStart2, frenzyStart3, frenzyEnd;
    public AudioClip timerTick, timerLow;
    public MasterManager master;
    public bool sfxOn;

    private void Start() {

        source = gameObject.GetComponent<AudioSource>();

        // get master
        master = transform.parent.GetComponent<MasterManager>();

        // init music
        Music(master.playerData.GetMusic());

        // check if player has sfx on
        if (master.playerData.GetSfx())
            sfxOn = true;
        else
            sfxOn = false;

        source = GetComponent<AudioSource>();
        
    }

    public void Music(bool value)
    {
        source.mute = !value;
    }

    public void PlaySound(AudioClip sound) {
        if (sfxOn)
            source.PlayOneShot(sound);
    }

    public void PlaySoundAfter(AudioClip sound, float delay) {
        if (sfxOn)
            StartCoroutine(_PlaySoundAfter(sound, delay));
    }

    IEnumerator _PlaySoundAfter(AudioClip sound, float delay) {

        yield return new WaitForSeconds(delay);
        if (sfxOn)
            source.PlayOneShot(sound);
    }
}