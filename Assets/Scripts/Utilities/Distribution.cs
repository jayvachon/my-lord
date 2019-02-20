using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Distribution
{
	public static float Random(float minValue=-1, float maxValue=1) {

		float mean = (minValue + maxValue) / 2f;
		float sigma = (maxValue - mean) / 3f;

        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        float standard = u * fac;
        return standard * sigma + mean;
    }
}
