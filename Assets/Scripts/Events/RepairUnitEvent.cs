using UnityEngine;
using System.Collections;

namespace EventSystem {

	public class RepairUnitEvent : GameEvent {

		public readonly Unit Unit;

		public RepairUnitEvent (Unit unit) {
			Unit = unit;
		}
	}
}