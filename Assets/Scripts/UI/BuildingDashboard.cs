using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class BuildingDashboard : MB
{
	public Image background;
    public Text value;
    public GameObject buttons;
    public Button buyButton;
    public Button renovateButton;
    public Text renovateText;
    public Player player;

    Building selectedBuilding;

    void Start() {
    	Disable();
    }

    public void BuyBuilding() {
    	if (player.Wealth >= selectedBuilding.Tier.value) {
    		Events.instance.Raise(new BuyBuildingEvent(selectedBuilding));
    		UpdateButtonStates();
    	}
    }

    public void RenovateBuilding() {
		if (player.Wealth >= selectedBuilding.Tier.renovate) {
			Events.instance.Raise(new RenovateBuildingEvent(selectedBuilding));
			UpdateButtonStates();
		}
	}

    void Enable() {
    	background.enabled = true;
    	value.enabled = true;
    	UpdateButtonStates();
    }

    void Disable() {
    	background.enabled = false;
    	value.enabled = false;
    	UpdateButtonStates(false);
    }

    void UpdateButtonStates(bool active=true) {
		buttons.SetActive(active);
		if (active) {
			switch(selectedBuilding.State) {
				case Building.BuildingState.ForSale:
		    		buyButton.gameObject.SetActive(true);
		    		renovateButton.gameObject.SetActive(false);
			    	buyButton.interactable = player.Wealth >= selectedBuilding.Tier.value;
			    	break;
			    case Building.BuildingState.Owned:
				    buyButton.gameObject.SetActive(false);
				    if (selectedBuilding.Tier.level < Tiers.Max) {
			    		renovateButton.gameObject.SetActive(true);
				    	renovateButton.interactable = 
			    			player.Wealth >= selectedBuilding.Tier.renovate;
			    		renovateText.text = "Renovate $" + selectedBuilding.Tier.renovate;
				    } else {
				    	renovateButton.gameObject.SetActive(false);
				    }
			    	break;
				default:
					buttons.SetActive(false);
					break;		
			}
		}
	}

	void UpdateDisplayText() {
		value.text = "$" + selectedBuilding.Tier.value.ToString();
	}

    protected override void AddListeners() {
    	Events.instance.AddListener<SelectBuildingEvent>(OnSelectBuildingEvent);
    	Events.instance.AddListener<DeselectBuildingEvent>(OnDeselectBuildingEvent);
    	Events.instance.AddListener<UpgradeBuildingEvent>(OnUpgradeBuildingEvent);
	}

    void OnSelectBuildingEvent(SelectBuildingEvent e) {
		selectedBuilding = e.Building; 
    	UpdateDisplayText();
		Enable();
    }

    void OnDeselectBuildingEvent(DeselectBuildingEvent e) {
    	selectedBuilding = null;
    	Disable();
    }

    void OnUpgradeBuildingEvent(UpgradeBuildingEvent e) {
		if (e.Building == selectedBuilding) {
			UpdateDisplayText();
			UpdateButtonStates();
		}
	}
}
