using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Unit: MB
{
    public int Rent { get; set; }
    public int Quality { get; set; }
    public bool RepairNeeded { get; private set; }
    public Tenant2 Tenant { get; private set; }

    void Awake() {
    	Tenant = new Tenant2();
    	Quality = 0;
    	Rent = 1600;
    	RepairNeeded = false;
    }

    public void Repair() {
    	RepairNeeded = false;
    }

    public void IgnoreRepair() {
    	RepairNeeded = false;
    }

    protected override void AddListeners() {
    	Events.instance.AddListener<EndMonthEvent>(OnEndMonthEvent);
    }

    void OnEndMonthEvent(EndMonthEvent e) {
    	RepairNeeded = Random.Range(0, 24) == 0;
    }
}
