using UnityEngine;
using System.Collections;

public class Audio : GameElement {
    public AudioSource source;
    // yeti sounds
    public AudioClip punchSmall, punchLarge;
    // game event sounds
    public AudioClip gameOver, goldModeFace, goldModeStart, goldModeEnd, crown;
    // others
    public AudioClip hit, pop, coin;

    private void Start() {
        source = GetComponent<AudioSource>();    
    }

    public void PlaySound(AudioClip sound) {
        source.PlayOneShot(sound);
    }

    public void PlaySoundAfter(AudioClip sound, float delay) {
        StartCoroutine(_PlaySoundAfter(sound, delay));
    }

    IEnumerator _PlaySoundAfter(AudioClip sound, float delay) {
        yield return new WaitForSeconds(delay);
        source.PlayOneShot(sound);
    }
}