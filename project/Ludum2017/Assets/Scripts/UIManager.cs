using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour 
{
	public static UIManager Instance;

	void Awake()
	{
		Instance = this;
	}

	public GameObject currentWindow;

	public void SetNewWindow(GameObject newWindow)
	{
		if (currentWindow != null) 
		{
			currentWindow.SetActive (false);
		}

		currentWindow = newWindow;
		currentWindow.SetActive (true);
	}
}
