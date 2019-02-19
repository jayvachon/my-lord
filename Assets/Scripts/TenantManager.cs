using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TenantManager
{
	static Stack<Tenant> tenants = new Stack<Tenant>();

	public static Tenant Create(int rent) {
		return new Tenant(rent);
	}

	public static void Destroy(Tenant tenant) {
		tenants.Push(tenant);
	}
}
