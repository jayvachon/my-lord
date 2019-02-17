using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ValueTier
{
    public int level, value, rent, renovate;

    public ValueTier(int _level, int _value, int _rent, int _renovate) {
    	level = _level;
    	value = _value;
    	rent = _rent;
    	renovate = _renovate;
    }
}
