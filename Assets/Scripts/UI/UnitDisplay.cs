using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class UnitDisplay : MB
{
    public Unit unit;
    public Text rent;
    public GameObject repairGroup;

    void Awake() {
    	repairGroup.SetActive(false);
    }

    void Update() {
    	rent.text = string.Format("Rent: ${0}", unit.Rent);
    }

    public void Ignore() {
    	Events.instance.Raise(new IgnoreRepairUnitEvent(unit));
    	repairGroup.SetActive(false);
    }

    public void Repair() {
    	Events.instance.Raise(new RepairUnitEvent(unit));
    	repairGroup.SetActive(false);
    }

    protected override void AddListeners() {
    	Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
    }

    void OnNewMonthEvent(NewMonthEvent e) {
    	if (unit.RepairNeeded) {
    		repairGroup.SetActive(true);
    	}
    }
}
