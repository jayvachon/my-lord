using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitDisplay : MonoBehaviour
{
    public Unit unit;
    public Text rent;

    void Update() {
    	rent.text = string.Format("Rent: {0}", unit.Rent);
    }
}
