using UnityEngine;
using System.Collections;

namespace EventSystem {

	public class SellBuildingEvent : GameEvent {

		public readonly Building Building;

		public SellBuildingEvent (Building building) {
			Building = building;
		}
	}
}