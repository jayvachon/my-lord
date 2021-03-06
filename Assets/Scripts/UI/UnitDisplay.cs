﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class UnitDisplay : MB
{
    public Unit unit;
    public Text rentAmount;
    public GameObject repairGroup;
    public GameObject rentEditGroup;
    public InputField rentInput;

    void Awake() {
    	repairGroup.SetActive(false);
    	rentEditGroup.SetActive(false);
    }

    void Start() {
    	rentAmount.text = string.Format("${0}", unit.Rent.ToDisplay());
    }

    public void Ignore() {
    	unit.IgnoreRepair();
    	Events.instance.Raise(new IgnoreRepairUnitEvent(unit));
    	repairGroup.SetActive(false);
    }

    public void Repair() {
    	unit.Repair();
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
    	rentEditGroup.SetActive(true);
    	rentAmount.gameObject.SetActive(false);
    }

    void OnBeginYearEvent(BeginYearEvent e) {
    	rentEditGroup.SetActive(false);
    	rentAmount.gameObject.SetActive(true);
    	rentAmount.text = string.Format("${0}", unit.Rent.ToDisplay());
    }
}
