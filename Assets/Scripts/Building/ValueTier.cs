using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueTier
{
    public int level, value, rent, renovate, rooms;

    public ValueTier(int _level, int _value, int _rent, int _renovate, int _rooms) {
    	level = _level;
    	value = _value;
    	rent = _rent;
    	renovate = _renovate;
    	rooms = _rooms;
    }
}
