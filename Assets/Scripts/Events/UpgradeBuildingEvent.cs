using UnityEngine;
using System.Collections;

namespace EventSystem {

	public class UpgradeBuildingEvent : GameEvent {

		public readonly Building Building;

		public UpgradeBuildingEvent (Building building) {
			Building = building;
		}
	}
}