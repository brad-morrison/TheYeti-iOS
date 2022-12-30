using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{
	public float delay;
	public AudioSource audio;
	public AudioClip clip;

	// Use this for initialization
	void Start()
	{
        Application.targetFrameRate = 600; // run at 60fps
        StartCoroutine(LoadMenu(delay));
	}

	public IEnumerator LoadMenu(float delay)
	{
		yield return new WaitForSeconds(delay);
        Application.LoadLevel("Menu");
    }

	public void PlaySound()
	{
		audio.PlayOneShot(clip);
	}
}

