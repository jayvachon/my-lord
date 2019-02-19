using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
	public Transform cylinder;

	void OnEnable() {
		cylinder.position = Vector3.zero;
		StartCoroutine(Ejaculate());
	}

	IEnumerator Ejaculate() {
		float e = 0f;
		while(e < 3f) {
			e += Time.deltaTime;
			cylinder.Translate(new Vector3(0, 0, -1) * 5 * Time.deltaTime);
			yield return null;
		}
		GameObjectPool.Destroy("Coins", transform);
	}
}
