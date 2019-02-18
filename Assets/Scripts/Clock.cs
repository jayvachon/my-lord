using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Clock : MonoBehaviour
{
	public float Progress {
		get { return progress / monthTime; }
	}

	public string CurrentMonth {
		get { return months[monthIndex]; }
	}

	string[] months = new string[] {
		"January", "February", "March", "April", 
		"May", "June", "July", "August", 
		"September", "October", "November", "December"
	};

	const float monthTime = 3f;
	float progress = 0f;
	int monthIndex = 0;

	void Update() {
		if (progress < monthTime) {
			progress += Time.deltaTime;
		} else {

			if (monthIndex < 11) {
				monthIndex ++;
			} else {
				monthIndex = 0;
			}

			progress = 0f;
			Events.instance.Raise(new NewMonthEvent());
		}
	}
}
