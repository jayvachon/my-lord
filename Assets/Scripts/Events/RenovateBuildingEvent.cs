using UnityEngine;
using System.Collections;

namespace EventSystem {

	public class RenovateBuildingEvent : GameEvent {

		public readonly Building Building;

		public RenovateBuildingEvent (Building building) {
			Building = building;
		}
	}
}