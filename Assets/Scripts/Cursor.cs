using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Cursor : MonoBehaviour {

	// float maxDistance = 5000f;

	void Update() {

		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity, Color.yellow);

		if (Input.GetMouseButtonDown(0)) {

			RaycastHit[] hits = Physics.RaycastAll(ray);
			for (int i = 0; i < hits.Length; i ++) {
				Debug.Log (hits);
			}
			// RaycastHit hit;
			// if (Physics.Raycast(ray, out hit, maxDistance)) {
				// Events.instance.Raise(new ClickEvent(hit.transform));
			// }
		}
	}
}
