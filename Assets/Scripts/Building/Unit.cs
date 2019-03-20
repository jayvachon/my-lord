using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit: MB
{
    public int Rent { get; private set; }

    void Awake() {
    	Rent = 1600;
    }
}
