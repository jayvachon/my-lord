using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Building : Clickable {

	public Material unselectedMaterial;
	public Material selectedMaterial;

	MeshRenderer _mesh = null;
	MeshRenderer mesh {
		get {
			if (_mesh == null) {
				_mesh = GetComponent<MeshRenderer>();
			}
			return _mesh;
		}
	}

	bool selected = false;

	public override void ClickThis() {
		Toggle();
	}

	public override void ClickOther() {
		Deselect();
	}

	void Toggle() {
		if (selected) {
			Deselect();
		} else {
			Select();
		}
	}

	void Deselect() {
		selected = false;
		mesh.material = unselectedMaterial;
	}

	void Select() {
		selected = true;
		mesh.material = selectedMaterial;
	}
}
