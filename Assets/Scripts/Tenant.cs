using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenant
{
    public string Name { get; private set; }

    // What tenant is capable of paying
    public int MaxRent { get; private set; }

    // What they actually pay
    public int Rent { get; private set; }

    public bool NeedRepair { get; set; }
    public string Message { get; private set; }
    public bool Evictable { get; private set; }

    string[] firstNames = new string[] {
    	"Rosa", "Karl", "Vladimir", "Linda", "Don", "Sheryl", "Stanley", "Fran", "Mohammed"
    };
    string[] lastNames = new string[] {
    	"Lee", "McCarthy", "Jones", "Luxembourg", "Nesmith", "Archer", "Cardoso", "Russell", "Wright"
    };

    public Tenant(int rent) {
    	Name = string.Format("{0} {1}", firstNames.RandomItem(), lastNames.RandomItem());
    	Rent = rent;
    	MaxRent = GetMaxRent();
    	NeedRepair = false;
    	Message = "";
    	Evictable = false;
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

    int GetMaxRent() {
    	int[] maxRents = new int[] {
			400, 600, 900, 1600, 2400
		};
		return maxRents.RandomItem();
    }
}
