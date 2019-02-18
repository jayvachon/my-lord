using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class TenantList : SelectBuildingListener, IRefreshable
{
	public Transform content;
	List<TenantDetails> details = new List<TenantDetails>();

	public void Refresh() {
		foreach(TenantDetails d in details) {
			d.Refresh();
		}
	}

	void CreateTenantDetails(Tenant tenant) {
		Transform newDetailsTransform = GameObjectPool
			.Instantiate("TenantDetails", Vector3.zero);
		newDetailsTransform.SetParent(content);
		TenantDetails newDetails = newDetailsTransform.GetComponent<TenantDetails>();
		newDetails.Init(tenant);
		details.Add(newDetails);
	}

	protected override void OnSelect() {
    	foreach(Tenant tenant in SelectedBuilding.Tenants) {
    		CreateTenantDetails(tenant);
    	}
    }

    protected override void OnDeselect() {
    	foreach(TenantDetails d in details) {
    		GameObjectPool.Destroy("TenantDetails", d.transform);
    	}
    	details.Clear();
    }
}
