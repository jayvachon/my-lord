using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class TenantList : PersonList
{
	protected override string TabText {
		get {
			return string.Format("Tenants ({0}/{1})",
				SelectedBuilding.Tenants.Count,
				SelectedBuilding.Rooms);
		}
	}

	protected override List<Tenant> Tenants {
		get {
			return SelectedBuilding.Tenants;
		}
	}
}
