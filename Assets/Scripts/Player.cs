using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Player : MonoBehaviour
{
    int wealth = 3000000;
    public int Wealth {
    	get { return wealth; }
    }

    public int MonthlyRevenue {
    	get {
    		return buildings.Sum(b => b.State == Building.BuildingState.Renovating ? 0 : b.Tier.rent);
    	}
    }

    List<Building> buildings = new List<Building>();

    void Awake() {
    	Events.instance.AddListener<BuyBuildingEvent>(OnBuyBuildingEvent);
        Events.instance.AddListener<RenovateBuildingEvent>(OnRenovateBuildingEvent);
    	Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
    }

    void OnBuyBuildingEvent(BuyBuildingEvent e) {
    	wealth -= e.Building.Tier.value;
    	buildings.Add(e.Building);
    }

    void OnRenovateBuildingEvent(RenovateBuildingEvent e) {
        wealth -= e.Building.Tier.renovate;
    }

    void OnNewMonthEvent(NewMonthEvent e) {
		foreach(Building b in buildings) {
			wealth += b.Tier.rent;
		}
	}
}
