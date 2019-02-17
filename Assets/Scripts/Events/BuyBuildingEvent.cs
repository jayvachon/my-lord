using UnityEngine;
using System.Collections;

namespace EventSystem {

	public class BuyBuildingEvent : GameEvent {

		public readonly Building Building;

		public BuyBuildingEvent (Building building) {
			Building = building;
		}
	}
}