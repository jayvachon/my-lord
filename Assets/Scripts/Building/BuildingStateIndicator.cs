using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingStateIndicator : MonoBehaviour
{
	void Update() {
		transform.Rotate(Vector3.up * 20 * Time.deltaTime);
	}    
}
