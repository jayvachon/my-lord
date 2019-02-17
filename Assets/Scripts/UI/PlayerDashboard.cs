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
		wealth.text = "Wealth: $" + player.Wealth.ToString();
		monthlyRevenue.text = "Monthly Revenue: $" + player.MonthlyRevenue.ToString();
		// progress.text = (Mathf.Round(clock.Progress*100)).ToString() + "%";
	}

	public void NextMonth() {
		Events.instance.Raise(new NewMonthEvent());
	}
}
