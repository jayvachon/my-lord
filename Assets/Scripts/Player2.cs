using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Player2 : MB
{
	public UnitManager units;

	public int Wealth { get; private set; }

	void Awake() {
		Wealth = 0;
	}

    protected override void AddListeners() {
    	Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
        Events.instance.AddListener<RepairUnitEvent>(OnRepairUnitEvent);
    }

    void OnNewMonthEvent(NewMonthEvent e) {
        Wealth += units.Rent;
    }

    void OnRepairUnitEvent(RepairUnitEvent e) {
        Wealth -= 500;
    }
}
