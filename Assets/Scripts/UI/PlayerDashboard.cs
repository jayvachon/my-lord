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
	public Button turnButton;

	void Update() {
		wealth.text = "Wealth: $" + player.Wealth.ToDisplay();
		monthlyRevenue.text = "Monthly Revenue: $" + player.MonthlyRevenue.ToDisplay();
		// progress.text = (Mathf.Round(clock.Progress*100)).ToString() + "%";
		progress.text = clock.CurrentMonth;

		turnButton.interactable = Game.State == GameState.YearStart;
	}

	public void BeginYear() {
		// clock.BeginYear();
		Events.instance.Raise(new BeginYearEvent());
	}

	/*public void NextMonth() {
		Events.instance.Raise(new NewMonthEvent());
	}*/
}
