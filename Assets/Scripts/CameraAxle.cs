using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAxle : MonoBehaviour {

	float speed = 60f;

	void Update () {
		if (Input.GetKey("left")) {
			Rotate(Vector3.up);
		}	
		if (Input.GetKey("right")) {
			Rotate(Vector3.down);
		}
	}

	void Rotate(Vector3 direction) {
		transform.RotateAround(Vector3.zero, direction, speed * Time.deltaTime);
	}
}
