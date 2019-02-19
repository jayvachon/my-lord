using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class PersonList : SelectBuildingListener, IRefreshable
{
    public Text tab;
	public Transform content;
	List<TenantDetails> details = new List<TenantDetails>();

	protected virtual string TabText {
		get { return ""; }
	}

	public void Refresh() {
		
		tab.text = TabText;

		foreach(TenantDetails d in details) {
			d.Refresh();
		}
	}

	protected void CreateTenantDetails(Tenant tenant) {
		Transform newDetailsTransform = GameObjectPool
			.Instantiate("TenantDetails", Vector3.zero);
		newDetailsTransform.SetParent(content);
		TenantDetails newDetails = newDetailsTransform.GetComponent<TenantDetails>();
		newDetails.Init(SelectedBuilding, tenant, this);
		details.Add(newDetails);
	}

    protected override void OnDeselect() {
    	foreach(TenantDetails d in details) {
    		GameObjectPool.Destroy("TenantDetails", d.transform);
    	}
    	details.Clear();
    }

    protected override void OnNewMonth() {
    	Refresh();
    }
}
