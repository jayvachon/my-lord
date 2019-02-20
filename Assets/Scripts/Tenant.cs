using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenant
{
    public enum TenantState {
        Unhoused,
        Applicant,
        Housed
    }

    // First and last name
    public string Name { get; private set; }

    // What tenant is capable of paying
    public int MaxRent { get; private set; }

    // What they actually pay
    public int Rent { get; private set; }

    // Minimum acceptable apartment quality
    public int MinQuality { get; private set; }

    // If something in their apartment needs attention
    public bool NeedRepair { get; set; }

    // Message to landloard - maybe should be an array
    public string Message { get; private set; }

    // Whether or not they can be evicted
    public bool Evictable { get; private set; }

    public TenantState State { get; private set; }

    string[] firstNames = new string[] {
    	"Rosa", "Karl", "Vladimir", "Linda", "Don", "Sheryl", "Stanley", "Fran", "Mohammed", "Vivian"
    };
    string[] lastNames = new string[] {
    	"Lee", "McCarthy", "Jones", "Luxembourg", "Nesmith", "Archer", "Cardoso", "Russell", "Wright"
    };

    public Tenant(int maxRent, int minQuality, TenantState state) {
    	Name = string.Format("{0} {1}", firstNames.RandomItem(), lastNames.RandomItem());
    	Rent = 0;
    	MaxRent = maxRent;
        MinQuality = minQuality;
    	NeedRepair = false;
    	Message = "";
    	Evictable = false;
        State = state;
    }

    public void UpdateRent(int newRent) {
    	Rent = Mathf.Min(MaxRent, newRent);
    	if (newRent > MaxRent) {
    		Message = string.Format("I can't afford this rent! I'm paying ${0}", MaxRent);
    		Evictable = true;
    	} else {
    		Message = "";
    		Evictable = false;
    	}
    }

    public void UpdateState(TenantState newState) {
        State = newState;
    }

    public void Print() {
        Debug.Log(string.Format("Name: {0}\nMaxRent: {1}\nMinQuality: {2}",
            Name, MaxRent, MinQuality));
    }
}
