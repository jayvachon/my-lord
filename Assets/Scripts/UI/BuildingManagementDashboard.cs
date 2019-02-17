using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class BuildingManagementDashboard : MB
{
    public GameObject dashboard;

    void Start() {
    	Disable();
    }

    void Enable() {
    	dashboard.gameObject.SetActive(true);
    }

    void Disable() {
    	dashboard.gameObject.SetActive(false);
    }

    protected override void AddListeners() {
    	Events.instance.AddListener<SelectBuildingEvent>(OnSelectBuildingEvent);
    	Events.instance.AddListener<DeselectBuildingEvent>(OnDeselectBuildingEvent);
    }

    void OnSelectBuildingEvent(SelectBuildingEvent e) {
    	Enable();
    }

    void OnDeselectBuildingEvent(DeselectBuildingEvent e) {
    	Disable();
    }
}
