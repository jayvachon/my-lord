using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public abstract class SelectBuildingListener : MB
{
	protected Building SelectedBuilding { get; private set; }

    protected override void AddListeners() {
    	Events.instance.AddListener<SelectBuildingEvent>(OnSelectBuildingEvent);
    	Events.instance.AddListener<DeselectBuildingEvent>(OnDeselectBuildingEvent);
        Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
    }

    protected void OnSelectBuildingEvent(SelectBuildingEvent e) {
    	SelectedBuilding = e.Building;
    	OnSelect();
    }

    protected void OnDeselectBuildingEvent(DeselectBuildingEvent e) {
    	SelectedBuilding = null;
    	OnDeselect();
    }

    protected void OnNewMonthEvent(NewMonthEvent e) {
        OnNewMonth();
    }

    protected abstract void OnSelect();
    protected abstract void OnDeselect();
    protected virtual void OnNewMonth() {}
}
