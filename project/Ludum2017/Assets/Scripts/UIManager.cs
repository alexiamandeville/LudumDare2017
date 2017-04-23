using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour 
{
	public static UIManager Instance;
    public Text soloObj;
    public Text[] playerAssets;

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

    void Update()
    {
        for (int i = 0; i < playerAssets.Length; i++)
        {
            playerAssets[i].text = NetworkedPlayer.myHexes[i]; //update UI for player assets
        }

        soloObj.text = QuestManager.Instance.currentSoloQuest.description.ToString();
    }
}
