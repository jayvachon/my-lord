using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static string ToDisplay(this int value) {
    	return value.ToString("##,#");
    }

    public static float Map (this float x, float x1, float x2, float y1,  float y2) {
		var m = (y2 - y1) / (x2 - x1);
		var c = y1 - m * x1;
		return m * x + c;
    }
}
