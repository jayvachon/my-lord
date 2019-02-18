using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tiers
{
	public static readonly ValueTier[] Tier = new ValueTier[] {
		new ValueTier(0, 500000, 400, 100000, 9),
		new ValueTier(1, 1000000, 600, 250000, 9),
		new ValueTier(2, 2000000, 900, 750000, 9),
		new ValueTier(3, 5000000, 1600, 1500000, 9),
		new ValueTier(4, 10000000, 2400, 5000000, 9)
	};

	public static int Max {
		get { return Tier.Length - 1; }
	}
}
