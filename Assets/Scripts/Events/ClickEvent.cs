using UnityEngine;
using System.Collections;

namespace EventSystem {

	public class ClickEvent : GameEvent {

		// public readonly RaycastHit hit;
		public readonly Transform transform;

		public ClickEvent (Transform _transform) {
			// hit = _hit;
			// transform = hit.transform;
			transform = _transform;
			Debug.Log (transform);
		}
	}
}