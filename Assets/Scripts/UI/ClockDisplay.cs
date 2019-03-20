using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class ClockDisplay : MBUI
{
	public UnitManager units;
	public Clock clock;
	public Text text;
	public Text buttonText;
	public Button button;

	enum ButtonState {
		Start,
		Running,
		Paused,
		Resume,
	}

	ButtonState buttonState;

	void Awake() {
		SetButtonState(ButtonState.Start);
	}

	public void HandleButtonPress() {
		if (buttonState == ButtonState.Start) {
			Events.instance.Raise(new BeginYearEvent());
			SetButtonState(ButtonState.Running);
		}
		if (buttonState == ButtonState.Resume) {
			clock.Resume();
			SetButtonState(ButtonState.Running);
		}
	}

	void SetButtonState(ButtonState state) {
		buttonState = state;
		switch(state) {
			case ButtonState.Start:
				buttonText.text = "Start Year";
				button.interactable = true;
				break;
			case ButtonState.Running:
				buttonText.text = "Running";
				button.interactable = false;
				break;
			case ButtonState.Paused:
				buttonText.text = "Paused";
				button.interactable = false;
				break;
			case ButtonState.Resume:
				buttonText.text = "Resume";
				button.interactable = true;
				break;
		}
	}

	void Update() {
		text.text = string.Format("{0} {1}", clock.CurrentMonth, clock.CurrentYear);
	}

	protected override void AddListeners() {
		Events.instance.AddListener<EndYearEvent>(OnEndYearEvent);
		Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
		Events.instance.AddListener<RepairUnitEvent>(OnRepairUnitEvent);
		Events.instance.AddListener<IgnoreRepairUnitEvent>(OnIgnoreRepairUnitEvent);
	}

	void OnEndYearEvent(EndYearEvent e) {
		SetButtonState(ButtonState.Start);
	}

	void OnNewMonthEvent(NewMonthEvent e) {
		if (units.RepairNeeded) {
			SetButtonState(ButtonState.Paused);
		}
	}

	void OnRepairUnitEvent(RepairUnitEvent e) {
		if (!units.RepairNeeded) {
			SetButtonState(ButtonState.Resume);
		}
	}

	void OnIgnoreRepairUnitEvent(IgnoreRepairUnitEvent e) {
		if (!units.RepairNeeded) {
			SetButtonState(ButtonState.Resume);
		}
	}
}
