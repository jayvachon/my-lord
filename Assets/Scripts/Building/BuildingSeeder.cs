using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

[RequireComponent(typeof(GameObjectPool))]
public class BuildingSeeder : MonoBehaviour {

	void Start() {

		const float perlinScale = 0.75f;

		Vector2 inheritedBuilding = new Vector2(
			Random.Range(0, 9),
			Random.Range(0, 4)
		);

		for (int i = 0; i < 9; i ++) {
			for (int j = 0; j < 4; j ++) {

				float val = Mathf.PerlinNoise(i * perlinScale, j * perlinScale);
				val = val.Map(0, 1, 0, Tiers.Max+1);
				val = Mathf.Floor(val);

				// Player starts the game owning one low-value building
				bool isInheritedBuilding = (i == inheritedBuilding.x && j == inheritedBuilding.y);
				ValueTier tier = isInheritedBuilding ? SeedValue(0) : SeedValue((int)val);

				Building building = GameObjectPool.Instantiate("Buildings", new Vector3(
					-4 + i,
					1,
					-4 + j * 2.75f
				)).GetComponent<Building>();
				building.Init(tier);

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

	ValueTier SeedValue(int index=-1) {
		return index == -1 ? Tiers.Tier[Random.Range(0, 5)] : Tiers.Tier[index];
	}
}
