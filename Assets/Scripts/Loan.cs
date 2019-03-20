using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Loan
{
    public int Amount { get; private set; }
    public float InterestRate { get; private set; }
    public int Duration { get; private set; } // in years
    public int TotalPaid { get; private set; }
    public int TotalOwed {
    	get { return Mathf.FloorToInt(Amount * InterestRate) + Amount; }
    }
    public int MonthlyPayment {
    	get { return TotalOwed / (Duration * 12); }
    }
    public int Owed {
    	get { return TotalOwed - TotalPaid; }
    }
    public bool Completed {
    	get { return TotalOwed <= 0; }
    }

    public Loan(int amount, float interestRate, int duration) {
    	Amount = amount;
    	InterestRate = interestRate;
    	Duration = duration;
    	TotalPaid = 0;
    	Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
    	Print();
    }

    void OnNewMonthEvent(NewMonthEvent e) {
    	TotalPaid += MonthlyPayment;
    	if (Completed) {
    		Events.instance.RemoveListener<NewMonthEvent>(OnNewMonthEvent);
    	}
    }

    public void Print() {
    	Debug.Log (string.Format("Amount: {0}\nInterestRate: {1}\nDuration: {2}\nTotalPaid: {3}\nTotalOwed: {4}\nMonthlyPayment: {5}",
    		Amount,
    		InterestRate,
    		Duration,
    		TotalPaid,
    		TotalOwed,
    		MonthlyPayment));
    }
}
