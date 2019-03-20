using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class ClockDisplay : MBUI
{
	public Clock clock;
	public Text text;
	public Text button;

	enum ButtonState {
		Start,
		Pause,
		Resume,
	}

	ButtonState buttonState;

	void Start() {
		buttonState = ButtonState.Start;
		button.text = "Start Year";
	}

	public void HandleButtonPress() {
		Events.instance.Raise(new BeginYearEvent());
	}

	void Update() {
		text.text = string.Format("{0} {1}", clock.CurrentMonth, clock.CurrentYear);
	}
}
