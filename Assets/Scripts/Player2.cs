using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Player2 : MB
{
	public Unit[] units;

	public int Wealth { get; private set; }

	void Awake() {
		Wealth = 0;
	}

    protected override void AddListeners() {
    	Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
    }

    void OnNewMonthEvent(NewMonthEvent e) {
    	for (int i = 0; i < units.Length; i ++) {
    		Wealth += units[i].Rent;
    	}
    }
}
