using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventSystem;

public enum BuildingState {
	NotForSale,
	ForSale,
	Owned,
	Renovating,
	Unlivable
}

public class Building : Clickable {

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
	public GameObject unlivableIndicator;

	public bool Selected { get; private set; }
	public BuildingState State { get; private set; }
	public ValueTier Tier { get; private set; }
	
	public List<Tenant> Tenants {
		get { return tenants; }
	}
	public bool HasTenants {
		get { return tenants.Count > 0; }
	}
	public List<Tenant> Applicants {
		get { return applicants; }
	}

	public int RepairCost {
		get { return repairsNeeded * 500; }
	}
	public bool NeedsRepairs {
		get { return repairsNeeded > 0; }
	}

	public int TotalRent {
		get { return tenants.Sum(t => t.Rent); }
	}

	public bool CanRenovate {
		get { return tenants.Count == 0; }
	}

	public int Value { get; private set; }
	public int Quality { get; private set; }
	public int PerRoomRent { get; private set; }
	public int Rooms { get; private set; }
	public int RenovationCost { get; private set; }

	List<Tenant> tenants = new List<Tenant>();
	List<Tenant> applicants = new List<Tenant>();
	int repairsNeeded = 0;
	int renovationTimer = 6; // Time in months
	Material unselectedMaterial;

	public void Init(int value, int quality, int perRoomRent) {
		Value = value;
		Quality = quality;
		PerRoomRent = perRoomRent;
		Rooms = 4;
		RenovationCost = 100000;
		// Tier = tier;
		// PerRoomRent = Tier.baseRent;
		SetState(BuildingState.NotForSale);
		UpdateDisplayValue();
		FillTenants();
		RemoveRepairs();
	}

	public void UpdateRent(int newRent) {
		PerRoomRent = newRent;
		foreach(Tenant t in tenants) {
			t.UpdateRent(newRent);
		}
	}

	public void EvictTenant(Tenant tenant) {
		tenants.Remove(tenant);
		TenantManager.UnhouseTenant(tenant);
	}

	public void AcceptApplicant(Tenant applicant) {
		TenantManager.AcceptApplicant(applicant);
		applicants.Remove(applicant);
		tenants.Add(applicant);
		if (tenants.Count == Rooms) {
			RejectAllApplicants();
		}
	}

	public void RejectApplicant(Tenant applicant) {
		applicants.Remove(applicant);
		TenantManager.RejectApplicant(applicant);
	}

	void RejectAllApplicants() {
		foreach(Tenant applicant in applicants) {
			TenantManager.RejectApplicant(applicant);
		}
		applicants.Clear();
	}

	void FillTenants() {
		for (int i = 0; i < Rooms; i ++) {
			Tenant newTenant;
			if (TenantManager.TryHouseTenant(PerRoomRent, Quality, out newTenant)) {
				tenants.Add(newTenant);
			}
		}
	}

	void UpdateDisplayValue() {
		information.text = (Value / 1000000f).ToString();
	}

	void AddRepair() {
		if (repairsNeeded < 3) {
			repairsNeeded ++;
		} else {
			repairsNeeded ++;
			RemoveTenants();
			SetState(BuildingState.Unlivable);
		}

		DisplayRepairs(repairsNeeded);
	}

	void DisplayRepairs(int severity) {
		switch(severity) {
			case 0:
				firstWarning.gameObject.SetActive(false);
				secondWarning.gameObject.SetActive(false);
				thirdWarning.gameObject.SetActive(false);
				unlivableIndicator.gameObject.SetActive(false);
				break;
			case 1:
				firstWarning.gameObject.SetActive(true);
				secondWarning.gameObject.SetActive(false);
				thirdWarning.gameObject.SetActive(false);
				unlivableIndicator.gameObject.SetActive(false);
				break;
			case 2:
				firstWarning.gameObject.SetActive(false);
				secondWarning.gameObject.SetActive(true);
				thirdWarning.gameObject.SetActive(false);
				unlivableIndicator.gameObject.SetActive(false);
				break;
			case 3:
				firstWarning.gameObject.SetActive(false);
				secondWarning.gameObject.SetActive(false);
				thirdWarning.gameObject.SetActive(true);
				unlivableIndicator.gameObject.SetActive(false);
				break;
			case 4:
				firstWarning.gameObject.SetActive(false);
				secondWarning.gameObject.SetActive(false);
				thirdWarning.gameObject.SetActive(false);
				unlivableIndicator.gameObject.SetActive(true);
				break;
		}
	}

	void RemoveRepairs() {
		repairsNeeded = 0;
		firstWarning.gameObject.SetActive(false);
		secondWarning.gameObject.SetActive(false);
		thirdWarning.gameObject.SetActive(false);
		unlivableIndicator.gameObject.SetActive(false);
	}

	void RemoveTenants() {
		tenants.Clear();
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
			case BuildingState.Unlivable:
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
 			RemoveRepairs();
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
	 			change = Random.Range(0, 72) == 0;
	 			if (change) {
	 				if (Selected) Deselect();
	 				SetState(BuildingState.ForSale);
	 			}
	 			break;
	 		case BuildingState.ForSale:
	 			change = Random.Range(0, 6) == 0;
	 			if (change) {
	 				if (Selected) Deselect();
	 				SetState(BuildingState.NotForSale);
	 			}
	 			break;
	 		case BuildingState.Renovating:
	 			if (renovationTimer > 0) {
	 				renovationTimer --;
	 			} else {
	 				renovationTimer = 6;
	 				Tier = Tiers.Tier[Tier.level + 1];
	 				UpdateDisplayValue();
	 				FillTenants();
	 				SetState(BuildingState.Owned);
	 				Events.instance.Raise(new UpgradeBuildingEvent(this));
	 			}
	 			break;
	 		case BuildingState.Owned:

	 			bool needRepairs = Random.Range(0, 24) == 0;
	 			if (needRepairs && tenants.Count > 0) {
	 				int tenantIndex = Random.Range(0, tenants.Count);
	 				tenants[tenantIndex].NeedRepair = true;
	 			}

	 			int repairsNeeded = tenants.Sum(t => t.NeedRepair ? 1 : 0);
	 			DisplayRepairs(Mathf.Min(1, repairsNeeded));
	 			
	 			if (tenants.Count < Rooms) {
 					Tenant newTenant;
 					if (TenantManager.TryGetApplicant(PerRoomRent, Quality, out newTenant)) {
 						applicants.Add(newTenant);
 					}
	 			}

	 			if (TotalRent > 0) {
	 				// Tried to do a simple effect. failed. 
	 				/*Transform c = GameObjectPool.Instantiate("Coins", transform.position);
	 				c.SetParent(transform);
	 				c.position = Vector3.zero;*/
	 			}
	 			break;
	 		default: break;
 		}
 	}
 	#endregion
 }
