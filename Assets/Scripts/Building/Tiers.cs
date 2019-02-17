using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tiers
{
	public static readonly ValueTier[] Tier = new ValueTier[] {
		new ValueTier(0, 500000, 1500, 10000, 9),
		new ValueTier(1, 1000000, 3000, 25000, 9),
		new ValueTier(2, 2000000, 3600, 75000, 9),
		new ValueTier(3, 5000000, 4500, 150000, 9),
		new ValueTier(4, 10000000, 6000, 500000, 9)
	};

	public static int Max {
		get { return Tier.Length - 1; }
	}
}
