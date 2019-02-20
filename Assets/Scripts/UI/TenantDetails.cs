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
    public Button acceptButton;
    public Button rejectButton;

    IRefreshable buildingManagementDashboard;
    Building building;
	Tenant tenant;

    public void Init(IRefreshable _buildingManagementDashboard,
        Building _building, Tenant _tenant) {

        buildingManagementDashboard = _buildingManagementDashboard;
        building = _building;
    	tenant = _tenant;
    	Refresh();
    }

    public void Repair() {
		tenant.NeedRepair = false;
		Refresh();
    }

    public void Evict() {
        building.EvictTenant(tenant);
        gameObject.SetActive(false);
        buildingManagementDashboard.Refresh();
    }

    public void Accept() {
        building.AcceptApplicant(tenant);
        buildingManagementDashboard.Refresh();
    }

    public void Reject() {
        building.RejectApplicant(tenant);
        buildingManagementDashboard.Refresh();
    }

    public void Refresh() {
    	tenantName.text = tenant.Name;
    	rent.text = "$" + tenant.Rent;
    	message.text = tenant.Message;

    	repairButton.gameObject.SetActive(false);
    	evictButton.gameObject.SetActive(false);
        acceptButton.gameObject.SetActive(false);
        rejectButton.gameObject.SetActive(false);

    	if (tenant.NeedRepair) {
    		repairButton.gameObject.SetActive(true);
    		repairText.text = "Fix $500";
    	}

        if (tenant.Evictable) {
            evictButton.gameObject.SetActive(true);
            evictText.text = "Evict";
        }

        if (tenant.State == Tenant.TenantState.Applicant) {
            acceptButton.gameObject.SetActive(true);
            rejectButton.gameObject.SetActive(true);
        }
    }
}
