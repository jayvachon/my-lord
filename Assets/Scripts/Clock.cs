using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Clock : MB
{
	public float Progress {
		get { return progress / monthTime; }
	}

	public string CurrentMonth {
		get { return months[monthIndex]; }
	}

	public string CurrentYear {
		get { return year.ToString(); }
	}

	string[] months = new string[] {
		"January", "February", "March", "April", 
		"May", "June", "July", "August", 
		"September", "October", "November", "December"
	};

	const float monthTime = 1f;
	float progress = 0f;
	int monthIndex = 0;
	int year = 2020;

	void Start() {
		Game.State = GameState.YearStart;
	}

	void Update() {

		if (Game.State == GameState.YearStart) {
			return;
		}

		if (progress < monthTime) {
			progress += Time.deltaTime;
		} else {

			if (monthIndex < 11) {
				monthIndex ++;
				Events.instance.Raise(new NewMonthEvent());
			} else {
				year ++;
				monthIndex = 0;
				Game.State = GameState.YearStart;
				Events.instance.Raise(new EndYearEvent());
			}

			progress = 0f;
			
		}
	}

	protected override void AddListeners() {
		Events.instance.AddListener<BeginYearEvent>(OnBeginYearEvent);
	}

	void OnBeginYearEvent(BeginYearEvent e) {
		Events.instance.Raise(new NewMonthEvent());
		Game.State = GameState.InYear;
	}
}
