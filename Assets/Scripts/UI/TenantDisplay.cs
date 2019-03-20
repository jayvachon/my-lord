using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EventSystem;

public class TenantDisplay : MB
{
	public Unit unit;
	public Text tenantName;
    public GameObject evictButton;
    public Text feedback;

    void Start() {
    	evictButton.SetActive(false);
    	feedback.gameObject.SetActive(false);
    	tenantName.text = unit.Tenant.Name;
    }

    void Update() {
    	if (unit.Rent > unit.Tenant.MaxRent) {
    		feedback.gameObject.SetActive(true);
    	} else {
    		feedback.gameObject.SetActive(false);
    	}
    }

    public void Evict() {

    }

    protected override void AddListeners() {
    	Events.instance.AddListener<EndYearEvent>(OnEndYearEvent);
    	Events.instance.AddListener<BeginYearEvent>(OnBeginYearEvent);
    }

    void OnEndYearEvent(EndYearEvent e) {
    	evictButton.SetActive(true);
    }

    void OnBeginYearEvent(BeginYearEvent e) {
    	evictButton.SetActive(false);
    }
}
