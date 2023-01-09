using UnityEngine;
using System.Collections;

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

