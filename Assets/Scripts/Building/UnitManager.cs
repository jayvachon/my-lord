using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public Unit[] units;
    public int Rent {
    	get { return units.Sum(u => u.Rent); }
    }
    
    public bool RepairNeeded {
    	get {
    		for (int i = 0; i < units.Length; i ++) {
    			if (units[i].RepairNeeded)
    				return true;
    		}
    		return false;
    	}
    }
}
