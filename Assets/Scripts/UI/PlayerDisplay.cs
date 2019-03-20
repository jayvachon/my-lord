using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : MonoBehaviour
{
	public Player2 player;
    public Text wealth;

    void Update() {
    	wealth.text = string.Format("Wealth: ${0}", player.Wealth.ToDisplay());
    }
}
