using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Cursor : MonoBehaviour {

	bool mouseDown = false;

	void LateUpdate() {
		if (Input.GetMouseButton(0)) {
			if (!mouseDown) {
				MouseDown();
			}
			mouseDown = true;
		}
		if (!Input.GetMouseButton(0)) {
			mouseDown = false;
		}
	}

	void MouseDown() {
		GetMouseOver();
	}

	void GetMouseOver() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, Mathf.Infinity)) {
			Events.instance.Raise(new ClickEvent(hit.transform));
		}
	}
}
