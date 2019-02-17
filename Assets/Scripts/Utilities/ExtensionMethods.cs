using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods
{
    public static string ToDisplay(this int value) {
    	return value.ToString("##,#");
    }
}
