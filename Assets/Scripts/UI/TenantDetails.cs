using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TenantDetails : MB, IRefreshable
{
	public Text tenantName;
	public Text rent;
	public Text message;

	Tenant tenant;

    public void Init(Tenant _tenant) {
    	tenant = _tenant;
    	Refresh();
    }

    public void Refresh() {
    	tenantName.text = tenant.Name;
    	rent.text = "$" + tenant.Rent;
    	message.text = tenant.Message;
    }
}
