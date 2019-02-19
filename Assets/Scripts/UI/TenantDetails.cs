using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TenantDetails : MB, IRefreshable
{
	public Text tenantName;
	public Text rent;
	public Text message;
	public Button repairButton;
	public Text repairText;
	public Button evictButton;
	public Text evictText;

    Building building;
	Tenant tenant;
    PersonList list;

    public void Init(Building _building, Tenant _tenant, PersonList _list) {
        building = _building;
    	tenant = _tenant;
        list = _list;
    	Refresh();
    }

    public void Repair() {
		tenant.NeedRepair = false;
		Refresh();
    }

    public void Evict() {
        building.EvictTenant(tenant);
        gameObject.SetActive(false);
        list.Refresh();
    }

    public void Refresh() {
    	tenantName.text = tenant.Name;
    	rent.text = "$" + tenant.Rent;
    	message.text = tenant.Message;

    	repairButton.gameObject.SetActive(false);
    	evictButton.gameObject.SetActive(false);

    	if (tenant.NeedRepair) {
    		repairButton.gameObject.SetActive(true);
    		repairText.text = "Fix $500";
    	}

        if (tenant.Evictable) {
            evictButton.gameObject.SetActive(true);
            evictText.text = "Evict";
        }
    }
}
