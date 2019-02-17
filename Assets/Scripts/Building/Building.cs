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

	public Material notForSaleMaterial;
	public Material forSaleMaterial;
	public Material ownedMaterial;
	public Material renovatingMaterial;
	public Material selectedMaterial;
	public TextMesh information;
	public MeshRenderer buildingMesh;
	public GameObject firstWarning;
	public GameObject secondWarning;
	public GameObject thirdWarning;

	public bool Selected { get; private set; }
	public BuildingState State { get; private set; }
	public ValueTier Tier { get; private set; }
	
	public List<Tenant> Tenants {
		get {
			return tenants;
		}
	}

	public int RepairCost {
		get {
			return repairsNeeded * 500;
		}
	}

	List<Tenant> tenants = new List<Tenant>();
	int repairsNeeded = 0;
	int renovationTimer = 3; // Time in months
	Material unselectedMaterial;

	public void Init(ValueTier tier) {
		
		Tier = tier;
		SetState(BuildingState.NotForSale);
		UpdateDisplayValue();

		for (int i = 0; i < Tier.rooms; i ++) {
			tenants.Add(new Tenant());
		}

		RemoveRepairs();
	}

	void UpdateDisplayValue() {
		information.text = (Tier.value*1.0f/1000000*1.0f).ToString();
	}

	void AddRepair() {
		if (repairsNeeded < 3) {
			repairsNeeded ++;
		} else {
			RemoveRepairs();
		}

		switch(repairsNeeded) {
			case 1:
				firstWarning.gameObject.SetActive(true);
				secondWarning.gameObject.SetActive(false);
				thirdWarning.gameObject.SetActive(false);
				break;
			case 2:
				firstWarning.gameObject.SetActive(false);
				secondWarning.gameObject.SetActive(true);
				thirdWarning.gameObject.SetActive(false);
				break;
			case 3:
				firstWarning.gameObject.SetActive(false);
				secondWarning.gameObject.SetActive(false);
				thirdWarning.gameObject.SetActive(true);
				break;
		}
		
	}

	void RemoveRepairs() {
		repairsNeeded = 0;
		firstWarning.gameObject.SetActive(false);
		secondWarning.gameObject.SetActive(false);
		thirdWarning.gameObject.SetActive(false);
	}

	#region Clickable
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
		buildingMesh.material = selectedMaterial;
	}

	void Deselect() {
		Events.instance.Raise(new DeselectBuildingEvent());
		Selected = false;
		buildingMesh.material = unselectedMaterial;
	}
	#endregion

	#region Events
	protected override void AddListeners() {
		base.AddListeners();
		Events.instance.AddListener<BuyBuildingEvent>(OnBuyBuildingEvent);
		Events.instance.AddListener<RenovateBuildingEvent>(OnRenovateBuildingEvent);
		Events.instance.AddListener<NewMonthEvent>(OnNewMonthEvent);
		Events.instance.AddListener<SellBuildingEvent>(OnSellBuildingEvent);
		Events.instance.AddListener<RepairBuildingEvent>(OnRepairBuildingEvent);
	}

	void SetState(BuildingState state) {
		State = state;
		switch(State) {
			case BuildingState.NotForSale:
				unselectedMaterial = notForSaleMaterial;
				break;
			case BuildingState.ForSale:
				unselectedMaterial = forSaleMaterial;
				break;
			case BuildingState.Owned:
				unselectedMaterial = ownedMaterial;
				break;
			case BuildingState.Renovating:
				unselectedMaterial = renovatingMaterial;
				break;
		}
		if (!Selected) {
			buildingMesh.material = unselectedMaterial;
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

 	void OnRepairBuildingEvent(RepairBuildingEvent e) {
 		if (e.Building == this) {
 			RemoveRepairs();
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
	 		case BuildingState.Owned:
	 			bool needRepairs = Random.Range(0, 20) == 0;
	 			if (needRepairs) {
	 				AddRepair();
	 			}
	 			break;
	 		default: break;
 		}
 	}
 	#endregion
 }
