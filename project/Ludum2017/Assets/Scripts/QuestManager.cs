using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour {

    private static QuestManager _instance;
    public static QuestManager Instance { get { return _instance; } }

    public List<Quest> questDict;
    public List<Quest> _soloQuests;
    public List<Quest> _groupQuests;
    public Quest currentSoloQuest;
    public Quest currentGroupQuest;

    // Use this for initialization
    void Awake () {

        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        //separate quests by type
        _soloQuests = questDict.FindAll(qFind => qFind.playerType == Quest.QuestType.SOLO).ToList();
        _groupQuests = questDict.FindAll(qFind => qFind.playerType == Quest.QuestType.GROUP).ToList();

        int randSolo = Random.Range(0, _soloQuests.Count);
        currentSoloQuest = _soloQuests[randSolo]; //set initial solo quest
        int randGroup = Random.Range(0, _groupQuests.Count);
        currentGroupQuest = _groupQuests[0]; //set initial group quest
    }

    public void NextSoloQuest()
    {
        int rand = Random.Range(0, _soloQuests.Count-1);
        currentSoloQuest = _soloQuests[rand]; //set new solo quest
    }

    public void NextGroupQuest()
    {
        int rand = Random.Range(0, _groupQuests.Count-1);
        currentGroupQuest = _groupQuests[rand]; //set new group quest
    }


}
