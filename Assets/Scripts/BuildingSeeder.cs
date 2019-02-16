using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GameObjectPool))]
public class BuildingSeeder : MonoBehaviour {

	void Awake() {
		for (int i = 0; i < 9; i ++) {
			for (int j = 0; j < 4; j ++) {
				GameObjectPool.Instantiate("Buildings", new Vector3(
					-4 + i,
					1,
					-4 + j * 2.75f
				));
			}
		}
	}
}
