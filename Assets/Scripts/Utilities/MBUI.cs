using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBUI : MonoBehaviour
{
 	void Awake() {
 		AddListeners();
 	}
 	
 	protected virtual void AddListeners() {}   
}
