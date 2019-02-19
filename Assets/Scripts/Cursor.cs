using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Cursor : SelectBuildingListener {

	bool mouseDown = false;
	bool blockClick = false;

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

		// If the mouse is over the UI, don't raycast
		if (Input.mousePosition.y <= 220) {
			return;
		}
		if (blockClick && Input.mousePosition.x >= 500) {
			return;
		}
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
			Events.instance.Raise(new ClickEvent(hit.transform));
		}
	}

	protected override void OnSelect() {
		blockClick = true;
	}

	protected override void OnDeselect() {
		blockClick = false;
	}
}
