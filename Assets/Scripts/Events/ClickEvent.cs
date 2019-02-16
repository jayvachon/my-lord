using UnityEngine;
using System.Collections;

namespace EventSystem {

	public class ClickEvent : GameEvent {

		public readonly Transform transform;

		public ClickEvent (Transform _transform) {
			transform = _transform;
		}
	}
}