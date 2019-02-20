﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Player : MB
{
    int wealth = 1000000;
    public int Wealth {
    	get { return wealth; }
    }

    public int MonthlyRevenue {
    	get { return buildings.Sum(b => b.TotalRent); }
    }

    List<Building> buildings = new List<Building>();

    protected override void AddListeners() {
    	Events.instance.AddListener<BuyBuildingEvent>(OnBuyBuildingEvent);
        Events.instance.AddListener<RenovateBuildingEvent>(OnRenovateBuildingEvent);
    	Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
        Events.instance.AddListener<SellBuildingEvent>(OnSellBuildingEvent);
        Events.instance.AddListener<RepairBuildingEvent>(OnRepairBuildingEvent);
    }

    void OnBuyBuildingEvent(BuyBuildingEvent e) {
    	wealth -= e.Building.Value;
    	buildings.Add(e.Building);
    }

    void OnRenovateBuildingEvent(RenovateBuildingEvent e) {
        wealth -= e.Building.Tier.renovate;
    }

    void OnNewMonthEvent(NewMonthEvent e) {
		foreach(Building b in buildings) {
			wealth += b.State == Building.BuildingState.Owned ? b.TotalRent : 0;
		}
	}

    void OnSellBuildingEvent(SellBuildingEvent e) {
        wealth += e.Building.Value;
    }

    void OnRepairBuildingEvent(RepairBuildingEvent e) {
        wealth -= e.Building.RepairCost;
    }
}
