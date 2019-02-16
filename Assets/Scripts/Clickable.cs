using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public abstract class Clickable : MonoBehaviour, IClickable {

	void Awake() {
		Events.instance.AddListener<ClickEvent>(OnClickEvent);
	}

	void OnMouseOver() {
		/*if (Input.GetMouseButtonDown(0)) {
			Events.instance.Raise(new ClickEvent(transform));
		}*/
	}

	void OnClickEvent(ClickEvent e) {
		if (e.transform == transform) {
			ClickThis();
		} else {
			ClickOther();
		}
	}

	public virtual void ClickThis() {}
	public virtual void ClickOther() {}
}
