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
    public GameObject newYearGroup;
    public InputField rentInput;

    void Awake() {
    	repairGroup.SetActive(false);
    	newYearGroup.SetActive(false);
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

    public void IncreaseRent() {
    	rentInput.text = (int.Parse(rentInput.text) + 100).ToString();
    	unit.Rent = int.Parse(rentInput.text);
    }

    public void ReduceRent() {
    	rentInput.text = (int.Parse(rentInput.text) - 100).ToString();
    	unit.Rent = int.Parse(rentInput.text);
    }

    protected override void AddListeners() {
    	Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
    	Events.instance.AddListener<EndYearEvent>(OnEndYearEvent);
    	Events.instance.AddListener<BeginYearEvent>(OnBeginYearEvent);
    }

    void OnNewMonthEvent(NewMonthEvent e) {
    	if (unit.RepairNeeded) {
    		repairGroup.SetActive(true);
    	}
    }

    void OnEndYearEvent(EndYearEvent e) {
    	newYearGroup.SetActive(true);
    }

    void OnBeginYearEvent(BeginYearEvent e) {
    	newYearGroup.SetActive(false);
    }
}
