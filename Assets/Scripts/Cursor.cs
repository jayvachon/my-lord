using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Cursor : SelectBuildingListener {

	bool mouseDown = false;
	bool blockClick = false;

	Transform hover;
	bool hovering = false;

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
		RaycastHit hit;
		if (GetMouseOver(out hit)) {
			hover = hit.transform;
			hovering = true;
		} else {
			hovering = false;
		}
		Debug.Log (hovering);
	}

	void MouseDown() {
		RaycastHit hit;
		if (GetMouseOver(out hit)) {
			Events.instance.Raise(new ClickEvent(hit.transform));
		}
	}

	bool GetMouseOver(out RaycastHit hit) {
		
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		bool didHit = Physics.Raycast(ray, out hit, Mathf.Infinity);

		// If the mouse is over the UI, don't raycast
		if (Input.mousePosition.y <= 220) {
			return false;
		}
		if (blockClick && Input.mousePosition.x >= 500) {
			return false;
		}
		return didHit;
	}

	protected override void OnSelect() {
		blockClick = true;
	}

	protected override void OnDeselect() {
		blockClick = false;
	}
}
