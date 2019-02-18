using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class PlayerDashboard : MonoBehaviour
{
	public Player player;
	public Clock clock;
	public Text wealth;
	public Text monthlyRevenue;
	public Text progress;

	void Update() {
		wealth.text = "Wealth: $" + player.Wealth.ToDisplay();
		monthlyRevenue.text = "Monthly Revenue: $" + player.MonthlyRevenue.ToDisplay();
		// progress.text = (Mathf.Round(clock.Progress*100)).ToString() + "%";
		progress.text = clock.CurrentMonth;
	}

	public void NextMonth() {
		Events.instance.Raise(new NewMonthEvent());
		// Events.instance.Raise(new DeselectBuildingEvent()); // Glitch - this doesn't actually deselect the building, just raises the event for those listening
	}
}
