using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PersonList : SelectBuildingListener, IRefreshable
{
	public BuildingManagementDashboard buildingManagementDashboard;
    public Text tab;
	public Transform content;
	List<TenantDetails> details = new List<TenantDetails>();

	protected abstract List<Tenant> Tenants { get; }
	protected abstract string TabText { get; }

	public void Refresh() {

		tab.text = TabText;

		foreach(TenantDetails d in details) {
			GameObjectPool.Destroy("TenantDetails", d.transform);
		}
		details.Clear();

		foreach(Tenant t in Tenants.OrderBy(n => n.Name).ToList()) {
			CreateTenantDetails(t);
		}
	}

	protected TenantDetails CreateTenantDetails(Tenant tenant) {
		Transform newDetailsTransform = GameObjectPool
			.Instantiate("TenantDetails", Vector3.zero);
		newDetailsTransform.SetParent(content);
		newDetailsTransform.SetAsLastSibling();
		TenantDetails newDetails = newDetailsTransform.GetComponent<TenantDetails>();
		newDetails.Init(buildingManagementDashboard, SelectedBuilding, tenant);
		details.Add(newDetails);
		return newDetails;
	}

    protected override void OnNewMonth() {
    	if (SelectedBuilding != null)
	    	Refresh();
    }
}
