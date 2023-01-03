using UnityEngine;
using System.Collections;

public class DestroyGameObject : MonoBehaviour
{
	public void DestroySelf() {
		Destroy(gameObject);
	}
}

