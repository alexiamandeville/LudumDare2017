using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public List<Quest> questDict;
    private List<Quest> _soloQuests;
    private List<Quest> _groupQuests;
    public static Quest currentSoloQuest;
    public static Quest currentGroupQuest;
     
	// Use this for initialization
	void Awake () {

        //separate quests by type
        List<Quest> _soloQuests = questDict.FindAll(qFind => qFind.playerType == Quest.QuestType.SOLO).ToList();
        List<Quest> _groupQuests = questDict.FindAll(qFind => qFind.playerType == Quest.QuestType.GROUP).ToList();

        int rand = Random.Range(0, questDict.Count);
        currentSoloQuest = questDict[rand]; //set initial solo quest
        currentGroupQuest = questDict[rand]; //set initial group quest
    }
	
	// Update is called once per frame
	void Update () {

        //group quest
        if (currentGroupQuest.progress.ToString() == "COMPLETE") //update current quest once player has completed one
        {
            NetworkedPlayer.LocalPlayerInstance.GetPhotonView().RPC("CompleteGroupQuest", PhotonTargets.All, currentGroupQuest.pointReward, currentGroupQuest.id); //call network update player
            int rand = Random.Range(0, questDict.Count);
            currentGroupQuest = questDict[rand]; //set updated quest

        }

        //solo quest
        if (currentSoloQuest.progress.ToString() == "COMPLETE") //update current quest once player has completed one
        {
            NetworkedPlayer.LocalPlayerInstance.GetPhotonView().RPC("CompleteSoloQuest", PhotonTargets.All, currentSoloQuest.pointReward, currentSoloQuest.id); //call network update player
            int rand = Random.Range(0, questDict.Count);
            currentSoloQuest = questDict[rand]; //set updated quest

        }
	}

}
