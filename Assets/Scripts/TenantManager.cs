using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TenantManager
{
	static List<Tenant> tenants = new List<Tenant>();

	static TenantManager() {
		
		for(int i = 0; i < 1000; i ++) {
			int maxRent = (int)Distribution.Random(0, 1600)
				.RoundToInterval(100)
				.Min(0);
			
			int minQuality = 0;
			if (maxRent > 600) { minQuality ++; }
			if (maxRent > 800) { minQuality ++; }
			if (maxRent > 900) { minQuality ++; }
			if (maxRent > 1000) { minQuality ++; }
			if (maxRent > 1200) { minQuality ++; }
			if (maxRent > 1600) { minQuality ++; }
			if (maxRent > 2400) { minQuality ++; }
			if (maxRent > 3000) { minQuality ++; }

			Tenant t = new Tenant(maxRent, minQuality, Tenant.TenantState.Unhoused);
			tenants.Add(t);
		}
	}

	public static bool TryHouseTenant(int rent, int quality, out Tenant tenant) {
		if (TryGetRandomTenantAboveMaxRentThreshold(rent, quality, out tenant)) {
			tenant.UpdateRent(rent);
			tenant.UpdateState(Tenant.TenantState.Housed);
		}
		return tenant != null;
	}

	public static bool TryGetApplicant(int rent, int quality, out Tenant tenant) {
		if (TryGetRandomTenantAboveMaxRentThreshold(rent, quality, out tenant)) {
			tenant.UpdateRent(rent);
			tenant.UpdateState(Tenant.TenantState.Applicant);
		}
		return tenant != null;
	}

	static bool TryGetRandomTenantAboveMaxRentThreshold(int rent, int quality, out Tenant qualifiedTenant) {
		List<Tenant> qualifiedTenants = tenants.FindAll(t => 
			t.MaxRent >= rent 
			&& t.MinQuality <= quality
			&& t.State == Tenant.TenantState.Unhoused);
		qualifiedTenant = qualifiedTenants.RandomItem();
		return qualifiedTenants.Count > 0;
	}

	public static void UnhouseTenant(Tenant tenant) {
		tenant.UpdateState(Tenant.TenantState.Unhoused);
	}

	public static void RejectApplicant(Tenant applicant) {
		applicant.UpdateRent(0);
		applicant.UpdateState(Tenant.TenantState.Unhoused);
	}

	public static void AcceptApplicant(Tenant applicant) {
		applicant.UpdateState(Tenant.TenantState.Housed);
	}

	public static void PrintUnhoused() {
		List<Tenant> unhousedTenants = tenants.FindAll(t => t.State == Tenant.TenantState.Unhoused);
		Debug.Log (string.Format("{0} unhoused", unhousedTenants.Count));
		foreach(Tenant t in unhousedTenants) {
			t.Print();
		}
	}
}
