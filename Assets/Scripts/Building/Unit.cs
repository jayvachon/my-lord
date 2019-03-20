using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Unit: MB
{
    public int Rent { get; private set; }
    public bool RepairNeeded { get; private set; }

    void Awake() {
    	Rent = 1600;
    	RepairNeeded = false;
    }

    protected override void AddListeners() {
    	Events.instance.AddListener<EndMonthEvent>(OnEndMonthEvent);
    	Events.instance.AddListener<RepairUnitEvent>(OnRepairUnitEvent);
    	Events.instance.AddListener<IgnoreRepairUnitEvent>(OnIgnoreRepairUnitEvent);
    }

    void OnEndMonthEvent(EndMonthEvent e) {
    	RepairNeeded = Random.Range(0, 24) == 0;
    }

    void OnRepairUnitEvent(RepairUnitEvent e) {
    	RepairNeeded = false;
    }

    void OnIgnoreRepairUnitEvent(IgnoreRepairUnitEvent e) {
    	RepairNeeded = false;
    }
}
