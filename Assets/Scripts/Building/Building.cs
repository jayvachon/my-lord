using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public class Building : Clickable {

	public enum BuildingState {
		NotForSale,
		ForSale,
		Owned,
		Renovating
	}

	public Material unselectedMaterial;
	public Material selectedMaterial;
	public TextMesh information;
	public BuildingStateIndicator ownedIndicator;
	public BuildingStateIndicator forSaleIndicator;
	public BuildingStateIndicator renovatingIndicator;

	public bool Selected { get; private set; }
	public BuildingState State { get; private set; }
	public ValueTier Tier { get; private set; }

	int renovationTimer = 3; // Time in months

	public void Init(ValueTier tier) {
		Tier = tier;
		SetState(BuildingState.NotForSale);
		UpdateDisplayValue();
	}

	void UpdateDisplayValue() {
		information.text = (Tier.value*1.0f/1000000*1.0f).ToString();
	}

	public override void ClickThis() {
		Toggle();
	}

	public override void ClickOther() {
		if (Selected) {
			Deselect();
		}
	}

	void Toggle() {
		if (Selected) {
			Deselect();
		} else {
			Select();
		}
	}

	void Select() {

		// To avoid race condition between de/selecting, always select last
		StartCoroutine(CoSelect());
	}
	IEnumerator CoSelect() {
		yield return new WaitForEndOfFrame();
		Events.instance.Raise(new SelectBuildingEvent(this));
		Selected = true;
		mesh.material = selectedMaterial;
	}

	void Deselect() {
		Events.instance.Raise(new DeselectBuildingEvent());
		Selected = false;
		mesh.material = unselectedMaterial;
	}

	protected override void AddListeners() {
		base.AddListeners();
		Events.instance.AddListener<BuyBuildingEvent>(OnBuyBuildingEvent);
		Events.instance.AddListener<RenovateBuildingEvent>(OnRenovateBuildingEvent);
		Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
		Events.instance.AddListener<SellBuildingEvent>(OnSellBuildingEvent);
	}

	void SetState(BuildingState state) {
		State = state;
		switch(state) {
			case BuildingState.NotForSale: 
				renovatingIndicator.gameObject.SetActive(false);
				ownedIndicator.gameObject.SetActive(false);
				forSaleIndicator.gameObject.SetActive(false);
				break;
			case BuildingState.ForSale: 
				renovatingIndicator.gameObject.SetActive(false);
				ownedIndicator.gameObject.SetActive(false);
				forSaleIndicator.gameObject.SetActive(true);
				break;
			case BuildingState.Owned:
				renovatingIndicator.gameObject.SetActive(false);
				ownedIndicator.gameObject.SetActive(true);
				forSaleIndicator.gameObject.SetActive(false);
				break;
			case BuildingState.Renovating:
				renovatingIndicator.gameObject.SetActive(true);
				ownedIndicator.gameObject.SetActive(false);
				forSaleIndicator.gameObject.SetActive(false);
				break;
		}
	}
 	
 	void OnBuyBuildingEvent(BuyBuildingEvent e) {
 		if (e.Building == this) {
 			SetState(BuildingState.Owned);
 		}
 	}

 	void OnRenovateBuildingEvent(RenovateBuildingEvent e) {
 		if (e.Building == this) {
 			SetState(BuildingState.Renovating);
 		}
 	}

 	void OnSellBuildingEvent(SellBuildingEvent e) {
 		if (e.Building == this) {
 			SetState(BuildingState.NotForSale);
 		}
 	}

 	void OnNewMonthEvent(NewMonthEvent e) {
 		
 		bool change;

 		switch(State) {
	 		case BuildingState.NotForSale:
	 			change = Random.Range(0, 40) == 0;
	 			if (change) {
	 				SetState(BuildingState.ForSale);
	 			}
	 			break;
	 		case BuildingState.ForSale:
	 			change = Random.Range(0, 3) == 0;
	 			if (change) {
	 				SetState(BuildingState.NotForSale);
	 			}
	 			break;
	 		case BuildingState.Renovating:
	 			if (renovationTimer > 0) {
	 				renovationTimer --;
	 			} else {
	 				renovationTimer = 3;
	 				Tier = Tiers.Tier[Tier.level + 1];
	 				UpdateDisplayValue();
	 				SetState(BuildingState.Owned);
	 				Events.instance.Raise(new UpgradeBuildingEvent(this));
	 			}
	 			break;
	 		default: break;
 		}
 	}
 }
