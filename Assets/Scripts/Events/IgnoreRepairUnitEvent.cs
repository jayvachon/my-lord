using UnityEngine;
using System.Collections;

namespace EventSystem {

	public class IgnoreRepairUnitEvent : GameEvent {

		public readonly Unit Unit;

		public IgnoreRepairUnitEvent (Unit unit) {
			Unit = unit;
		}
	}
}