using UnityEngine;
using System.Collections;

namespace EventSystem {

	public class RepairBuildingEvent : GameEvent {

		public readonly Building Building;

		public RepairBuildingEvent (Building building) {
			Building = building;
		}
	}
}