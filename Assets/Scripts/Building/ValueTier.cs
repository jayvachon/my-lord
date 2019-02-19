using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ValueTier
{
    public int level, value, baseRent, renovate, rooms;

    public ValueTier(int _level, int _value, int _baseRent, int _renovate, int _rooms) {
    	level = _level;
    	value = _value;
    	baseRent = _baseRent;
    	renovate = _renovate;
    	rooms = _rooms;
    }
}
