using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject roundWindow;
	public GameObject endWindow;
	bool finalRound = false;

	public static GameManager Instance;

	void Awake()
	{
		Instance = this;
	}

	void OnDisable()
	{
		if (finalRound == false) {
			UIManager.Instance.SetNewWindow (roundWindow);
		}
		if (finalRound == true) {
			UIManager.Instance.SetNewWindow (endWindow);
		}
	}

	public void SetFinalRound (bool theweeknd)
	{
		finalRound = theweeknd;
	}

	public void ResetGame ()
	{
		SceneManager.LoadScene ("main");
	}

}
