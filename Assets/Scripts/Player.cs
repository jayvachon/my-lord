using System.Linq;
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
    	get { 
            int revenue = buildings.Sum(b => b.TotalRent);
            int expenses = 0;
            Loan loan = LoanManager.GetLoan();
            if (loan != null && !loan.Completed) {
                expenses = loan.MonthlyPayment;
            }
            return revenue - expenses;
        }
    }

    List<Building> buildings = new List<Building>();

    void Update() {
        if (Input.GetKeyDown(KeyCode.L)) {
            Loan loan = LoanManager.NewLoan();
            wealth += loan.Amount;
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            LoanManager.GetLoan().Print();
        }
    }

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
        wealth -= e.Building.RenovationCost;
    }

    void OnNewMonthEvent(NewMonthEvent e) {
		foreach(Building b in buildings) {
			wealth += b.State == BuildingState.Owned ? b.TotalRent : 0;
		}
        Loan loan = LoanManager.GetLoan();
        if (loan != null && !loan.Completed) {
            wealth -= loan.MonthlyPayment;
        }
	}

    void OnSellBuildingEvent(SellBuildingEvent e) {
        wealth += e.Building.Value;
    }

    void OnRepairBuildingEvent(RepairBuildingEvent e) {
        
    }
}
