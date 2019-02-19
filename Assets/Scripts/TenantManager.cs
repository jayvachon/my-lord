using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TenantManager
{
	static Stack<Tenant> tenants = new Stack<Tenant>();

	public static Tenant Create(int rent, Tenant.TenantState state) {
		return new Tenant(rent, state);
	}

	public static void Destroy(Tenant tenant) {
		tenants.Push(tenant);
	}
}
