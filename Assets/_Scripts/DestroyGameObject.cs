using UnityEngine;
using System.Collections;

// Utility class to destroy any object
public class DestroyGameObject : MonoBehaviour
{
	public void DestroySelf() {
		Destroy(gameObject);
	}

	public void DestroySelfAfter(float time)
	{
		StartCoroutine(_DestroySelfAfter(time));
	}

    public IEnumerator _DestroySelfAfter(float time)
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}

