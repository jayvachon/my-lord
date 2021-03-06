﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class BuildingManagementDashboard : SelectBuildingListener, IRefreshable, IEnableable
{
    public Player player;

    public GameObject dashboard;
    public GameObject buttonBlocker;
    public Text label;

    public GameObject buyButton;
    public GameObject renovateButton;
    public GameObject sellButton;
    public Text renovateText;

    public RectTransform rent;
    public InputField rentInput;
    public TenantList tenantList;

    public RectTransform title;
    public RectTransform tabs;
    public ApplicantList applicantList;

    void Start() {
        buttonBlocker.gameObject.SetActive(false);
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
        buyButton.gameObject.SetActive(false);
        renovateButton.gameObject.SetActive(false);
        sellButton.gameObject.SetActive(false);
    	rent.gameObject.SetActive(false);
        tenantList.gameObject.SetActive(false);
        title.gameObject.SetActive(false);
        tabs.gameObject.SetActive(false);
        applicantList.gameObject.SetActive(false);
    }

    public void Refresh() {
        Refresh(false);
    }

    public void Refresh(bool showApplicantList=false) {

        if (!SelectedBuilding) {
            Disable();
            return;
        }

        Enable();

        label.text = string.Format("Manage Building | {0}", SelectedBuilding.Value.ToDisplay());

        if (SelectedBuilding.State == BuildingState.Owned
            || SelectedBuilding.State == BuildingState.Renovating) {
            
            buyButton.gameObject.SetActive(false);
            sellButton.gameObject.SetActive(true);

            if (SelectedBuilding.HasRenovated) {
                renovateButton.gameObject.SetActive(false);
            } else {
                renovateButton.gameObject.SetActive(true);
                renovateText.text = string.Format("Renovate ${0}",
                    SelectedBuilding.RenovationCost.ToDisplay());
                renovateButton.GetComponent<Button>()
                    .interactable = SelectedBuilding.CanRenovate;
            }

            rentInput.text = SelectedBuilding.PerRoomRent.ToString();
            tenantList.Refresh();
            applicantList.Refresh();

            if (showApplicantList) {
                tenantList.gameObject.SetActive(false);
                applicantList.gameObject.SetActive(true);
            } else {
                tenantList.gameObject.SetActive(true);
                applicantList.gameObject.SetActive(false);
            }
        }

        if (SelectedBuilding.State == BuildingState.Renovating) {
            sellButton.gameObject.SetActive(false);
            renovateButton.gameObject.SetActive(true);
            renovateText.text = "Renovating";
            renovateButton.GetComponent<Button>().interactable = false;
            rent.gameObject.SetActive(false);
            tabs.gameObject.SetActive(false);
            tenantList.gameObject.SetActive(false);
            applicantList.gameObject.SetActive(false);
        }

        if (SelectedBuilding.State == BuildingState.NotForSale
            || SelectedBuilding.State == BuildingState.ForSale) {

            sellButton.gameObject.SetActive(false);
            renovateButton.gameObject.SetActive(false);
            tabs.gameObject.SetActive(false);
            rent.gameObject.SetActive(false);
            tenantList.gameObject.SetActive(false);
            applicantList.gameObject.SetActive(false);
        }

        if (SelectedBuilding.State == BuildingState.ForSale) {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponent<Button>()
                .interactable = player.Wealth >= SelectedBuilding.Value;
        }
    }

    public void BuyBuilding() {
        if (player.Wealth >= SelectedBuilding.Value) {
            Events.instance.Raise(new BuyBuildingEvent(SelectedBuilding));
            Refresh();
        }
    }

    public void RenovateBuilding() {
        if (player.Wealth >= SelectedBuilding.RenovationCost) {
            Events.instance.Raise(new RenovateBuildingEvent(SelectedBuilding));
            Refresh();
        }
    }

    public void SellBuilding() {
        Events.instance.Raise(new SellBuildingEvent(SelectedBuilding));
        Refresh();
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

    protected override void AddListeners() {
        base.AddListeners();
        Events.instance.AddListener<RenovateBuildingEvent>(OnRenovateBuildingEvent);
        Events.instance.AddListener<BeginYearEvent>(OnBeginYearEvent);
        Events.instance.AddListener<EndYearEvent>(OnEndYearEvent);
    }

    void OnRenovateBuildingEvent(RenovateBuildingEvent e) {
        if (e.Building == SelectedBuilding) {
            Refresh();
        }
    }

    void OnBeginYearEvent(BeginYearEvent e) {
        buttonBlocker.gameObject.SetActive(true);
    }

    void OnEndYearEvent(EndYearEvent e) {
        buttonBlocker.gameObject.SetActive(false);
    }
}
