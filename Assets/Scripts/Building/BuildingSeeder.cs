using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

[RequireComponent(typeof(GameObjectPool))]
public class BuildingSeeder : MonoBehaviour {

	void Start() {

		Vector2 inheritedBuilding = new Vector2(
			Random.Range(0, 9),
			Random.Range(0, 4)
		);

		for (int i = 0; i < 9; i ++) {
			for (int j = 0; j < 4; j ++) {

				float val = Distribution.Random(0, 2000000)
					.RoundToInterval(500000)
					.Min(500000);
				
				/*if (val <= 500000) {
					val = val.CeilToInterval(250000);
				} else if (val <= 2500000) {
					val = val.CeilToInterval(500000);
				} else if (val <= 9000000) {
					val = val.CeilToInterval(1000000);
				} else {
					val = val.CeilToInterval(10000000);
				}*/
				
				int quality = 0;
				if (val >= 1000000) { quality ++; }
				if (val >= 2000000) { quality ++; }
				if (val >= 3000000) { quality ++; }
				if (val >= 5000000) { quality ++; }
				if (val >= 7500000) { quality ++; }
				if (val >= 10000000) { quality ++; }
				if (val >= 15000000) { quality ++; }
				if (val >= 20000000) { quality ++; }

				int perRoomRent = 300;
				if (quality == 1) { perRoomRent = 400; }
				if (quality == 2) { perRoomRent = 600; }
				if (quality == 3) { perRoomRent = 800; }
				if (quality == 4) { perRoomRent = 1000; }
				if (quality == 5) { perRoomRent = 1200; }
				if (quality == 6) { perRoomRent = 1600; }
				if (quality == 7) { perRoomRent = 2400; }
				if (quality == 8) { perRoomRent = 3200; }

				// Player starts the game owning one low-value building
				bool isInheritedBuilding = (i == inheritedBuilding.x && j == inheritedBuilding.y);
				if (isInheritedBuilding) {
					val = 500000;
					quality = 0;
					perRoomRent = 300;
				}

				Building building = GameObjectPool.Instantiate("Buildings", new Vector3(
					-4 + i,
					1,
					-4 + j * 2.75f
				)).GetComponent<Building>();
				building.Init((int)val, quality, perRoomRent);

				if (isInheritedBuilding) {
					StartCoroutine(InheritBuilding(building));
				}
			}
		}
	}

	IEnumerator InheritBuilding(Building building) {

		// Wait until the end of the frame to ensure all listeners have been added
		yield return new WaitForEndOfFrame();
		Events.instance.Raise(new BuyBuildingEvent(building));
	}
}
