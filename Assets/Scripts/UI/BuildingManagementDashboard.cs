using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class BuildingManagementDashboard : SelectBuildingListener, IRefreshable, IEnableable
{
    public GameObject dashboard;
    public RectTransform rent;
    public InputField rentInput;
    public TenantList tenantList;

    public RectTransform title;
    public RectTransform tabs;
    public RectTransform applicantList;

    void Start() {
    	Disable();
    }

    public void Enable() {
        rent.gameObject.SetActive(true);
        tenantList.gameObject.SetActive(true);
        title.gameObject.SetActive(true);
        tabs.gameObject.SetActive(true);
        applicantList.gameObject.SetActive(true);
    }

    public void Disable() {
    	rent.gameObject.SetActive(false);
        tenantList.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        tabs.gameObject.SetActive(false);
        applicantList.gameObject.SetActive(false);
    }

    public void Refresh() {

        if (!SelectedBuilding) {
            Disable();
            return;
        }

        Enable();

        if (SelectedBuilding.State != Building.BuildingState.NotForSale 
            && SelectedBuilding.State != Building.BuildingState.ForSale) {
            
            rentInput.text = SelectedBuilding.PerRoomRent.ToString();
            tenantList.Refresh();
        } else {
            tabs.gameObject.SetActive(false);
            rent.gameObject.SetActive(false);
            tenantList.gameObject.SetActive(false);
            applicantList.gameObject.SetActive(false);
        }
    }

    public void AdjustUp() {
        rentInput.text = (int.Parse(rentInput.text) + 100).ToString();
        SelectedBuilding.UpdateRent(int.Parse(rentInput.text));
        Refresh();
    }

    public void AdjustDown() {
        rentInput.text = (int.Parse(rentInput.text) - 100).ToString();
        SelectedBuilding.UpdateRent(int.Parse(rentInput.text));
        Refresh();
    }

    public void AcceptRentUpdate() {
        SelectedBuilding.UpdateRent(int.Parse(rentInput.text));
        Refresh();
    }

    public void RejectRentUpdate() { Refresh(); }
    protected override void OnSelect() { Refresh(); }
    protected override void OnDeselect() { Refresh(); }
}
