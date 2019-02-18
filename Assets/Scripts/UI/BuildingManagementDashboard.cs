using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class BuildingManagementDashboard : SelectBuildingListener, IRefreshable
{
    public GameObject dashboard;
    public InputField rent; 
    public TenantList tenantList;

    void Start() {
    	Disable();
    }

    void Enable() {
    	dashboard.gameObject.SetActive(true);
    }

    void Disable() {
    	dashboard.gameObject.SetActive(false);
    }

    public void Refresh() {
        if (SelectedBuilding.State != Building.BuildingState.NotForSale && SelectedBuilding.State != Building.BuildingState.ForSale) {
            rent.gameObject.SetActive(true);
            tenantList.gameObject.SetActive(true);
            rent.text = SelectedBuilding.Tier.rent.ToString();
            tenantList.Refresh();
        } else {
            rent.gameObject.SetActive(false);
            tenantList.gameObject.SetActive(false);
        }
    }

    public void AcceptRentUpdate() {
        SelectedBuilding.UpdateRent(int.Parse(rent.text));
        Refresh();
    }

    public void RejectRentUpdate() {
        Refresh();
    }

    protected override void OnSelect() {
        Refresh();
    	Enable();
    }

    protected override void OnDeselect() {
        Disable();
    }
}
