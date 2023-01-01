using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
    public AudioSource source;
    // yeti sounds
    public AudioClip punchSmall, punchLarge;
    // game event sounds
    public AudioClip gameOver, goldModeFace, goldModeStart, goldModeEnd, crown;
    // others
    public AudioClip hit, pop, coin;
    public GameManager manager;

    private void Start() {
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        source = GetComponent<AudioSource>();    
    }

    public void PlaySound(AudioClip sound) {
        if (manager.sound) // for debug
            source.PlayOneShot(sound);
    }

    public void PlaySoundAfter(AudioClip sound, float delay) {
        if (manager.sound) // for debug
            StartCoroutine(_PlaySoundAfter(sound, delay));
    }

    IEnumerator _PlaySoundAfter(AudioClip sound, float delay) {

        yield return new WaitForSeconds(delay);
        if (manager.sound) // for debug
            source.PlayOneShot(sound);
    }
}