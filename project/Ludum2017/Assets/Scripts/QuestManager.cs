using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    public List<Quest> questDict;
    private static List<Quest> _soloQuests = new List<Quest>();
    private static List<Quest> _groupQuests = new List<Quest>();
    public static Quest currentSoloQuest;
    public static Quest currentGroupQuest;

    // Use this for initialization
    void Awake () {

        //separate quests by type
        List<Quest> _soloQuests = questDict.FindAll(qFind => qFind.playerType == Quest.QuestType.SOLO).ToList();
        List<Quest> _groupQuests = questDict.FindAll(qFind => qFind.playerType == Quest.QuestType.GROUP).ToList();

        int randSolo = Random.Range(0, _soloQuests.Count);
        currentSoloQuest = _soloQuests[randSolo]; //set initial solo quest
        int randGroup = Random.Range(0, _soloQuests.Count);
        currentGroupQuest = _groupQuests[randGroup]; //set initial group quest
    }

    public static void NextSoloQuest()
    {
        print(_soloQuests.Count);
        int rand = Random.Range(0, _soloQuests.Count-1);
        currentSoloQuest = _soloQuests[rand]; //set initial solo quest
    }

    public static void NextGroupQuest()
    {
        int rand = Random.Range(0, _groupQuests.Count-1);
        currentGroupQuest = _groupQuests[rand]; //set initial group quest
    }


}
