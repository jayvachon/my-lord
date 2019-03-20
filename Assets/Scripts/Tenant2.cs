using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tenant2
{
    public readonly string Name;
    public readonly int MaxRent;
    public readonly int MinQuality;

    public Tenant2() {
    	Name = Names.One();
    	MaxRent = 1800;
    	MinQuality = 0;
    }
}
