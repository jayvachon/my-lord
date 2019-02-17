using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

// Real-time clock. Might deprecate this if I go with turn-based.

public class Clock : MonoBehaviour
{
	public float Progress {
		get { return progress / month; }
	}

	const float month = 10f;
	float progress = 0f;

	void Update() {
		if (progress < month) {
			progress += Time.deltaTime;
		} else {
			progress = 0f;
			// Events.instance.Raise(new NewMonthEvent());
		}
	}
}
