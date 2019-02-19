using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class BuildingDashboard : SelectBuildingListener, IRefreshable
{
	public Image background;
    public Text value;
    public Text tenants;
    public GameObject buttons;
    public Button buyButton;
    public Button renovateButton;
    public Text renovateText;
    public Button sellButton;
    public Text sellText;
    public Button repairButton;
    public Text repairText;
    public Player player;

    void Start() {
    	Disable();
    }

    public void Refresh() {
    	if (SelectedBuilding != null) {
	    	UpdateDisplayText();
	    	UpdateButtonStates();
    	}
    }

    public void BuyBuilding() {
    	if (player.Wealth >= SelectedBuilding.Tier.value) {
    		Events.instance.Raise(new BuyBuildingEvent(SelectedBuilding));
    		UpdateButtonStates();
    	}
    }

    public void RenovateBuilding() {
		if (player.Wealth >= SelectedBuilding.Tier.renovate) {
			Events.instance.Raise(new RenovateBuildingEvent(SelectedBuilding));
			UpdateButtonStates();
		}
	}

	public void RepairBuilding() {
		if (player.Wealth >= SelectedBuilding.RepairCost) {
			Events.instance.Raise(new RepairBuildingEvent(SelectedBuilding));
			UpdateButtonStates();
		}
	}

	public void SellBuilding() {
		Events.instance.Raise(new SellBuildingEvent(SelectedBuilding));
		UpdateButtonStates();
	}

    void Enable() {
    	background.enabled = true;
    	value.enabled = true;
    	tenants.enabled = true;
    	// UpdateButtonStates();
    }

    void Disable() {
    	background.enabled = false;
    	value.enabled = false;
    	tenants.enabled = false;
    	UpdateButtonStates(false);
    }

    void UpdateButtonStates(bool active=true) {
		
		buttons.SetActive(active);
		if (!active) return;

		buyButton.gameObject.SetActive(false);
		renovateButton.gameObject.SetActive(false);
		sellButton.gameObject.SetActive(false);
		repairButton.gameObject.SetActive(false);

		switch(SelectedBuilding.State) {
			case Building.BuildingState.ForSale:
	    		buyButton.gameObject.SetActive(true);
		    	buyButton.interactable = player.Wealth >= SelectedBuilding.Tier.value;
		    	break;
		    case Building.BuildingState.Owned:

			    // Repair
			    if (SelectedBuilding.NeedsRepairs) {
			    	ActivateRepairButton();
			    }

			    // Sell (cannot sell without fixing your shit)
			    else {
				    sellButton.gameObject.SetActive(true);
				    sellText.text = "Sell $" + SelectedBuilding.Tier.value.ToDisplay();
			    }
		    	break;
		    case Building.BuildingState.Unlivable:
			    if (SelectedBuilding.Tier.level < Tiers.Max) {
		    		ActivateRenovateButton();
			    }
		    	ActivateRepairButton();
		    	break;
		    case Building.BuildingState.Renovating:
		    	buttons.SetActive(false);
	    		break;
			default:
				buttons.SetActive(false);
				break;		
		}
	}

	void ActivateRepairButton() {
    	repairButton.gameObject.SetActive(true);
	    repairButton.interactable = 
			player.Wealth >= SelectedBuilding.RepairCost;
    	repairText.text = "Fix $" + SelectedBuilding.RepairCost.ToDisplay();
	}

	void ActivateRenovateButton() {
		renovateButton.gameObject.SetActive(true);
    	renovateButton.interactable = 
			player.Wealth >= SelectedBuilding.Tier.renovate;
		renovateText.text = "Renovate $" + SelectedBuilding.Tier.renovate.ToDisplay();
	}

	void UpdateDisplayText() {
		value.text = "Building Value: $" + SelectedBuilding.Tier.value.ToDisplay();
		tenants.text = string.Format("Tenants: {0}/{1} @ ${2}/room",
			SelectedBuilding.Tenants.Count,
			SelectedBuilding.Tier.rooms,
			SelectedBuilding.Tier.baseRent);
	}

    protected override void AddListeners() {
    	base.AddListeners();
    	// Events.instance.AddListener<UpgradeBuildingEvent>(OnUpgradeBuildingEvent);
    	Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
	}

	protected override void OnSelect() {
		Refresh();
		Enable();
	}

	protected override void OnDeselect() {
		Disable();
	}

	void OnNewMonthEvent(NewMonthEvent e) {
		Refresh();
	}

    /*void OnUpgradeBuildingEvent(UpgradeBuildingEvent e) {
		if (e.Building == SelectedBuilding) {
			UpdateDisplayText();
			UpdateButtonStates();
		}
	}*/
}
