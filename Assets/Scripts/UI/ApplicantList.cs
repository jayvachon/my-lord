﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ApplicantList : PersonList
{
	protected override string TabText {
		get {
			return string.Format("Applicants ({0})",
				SelectedBuilding.Applicants.Count);
		}
	}

	protected override List<Tenant> Tenants {
		get {
			return SelectedBuilding.Applicants;
		}
	}
}
