using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundStart : MonoBehaviour {

	public GameObject inGameWindow;
	public int maxRounds;

	int roundNumber = 1;
	public Text title;

	void OnEnable()
	{
		title.text = ("Round " + roundNumber.ToString ());
	}

	void OnDisable()
	{
		if (roundNumber <= maxRounds) {
			UIManager.Instance.SetNewWindow (inGameWindow);
			roundNumber++;
		} 
		if (roundNumber > maxRounds) 
		{
			GameManager.Instance.SetFinalRound (true);
		}

	}
		
}
