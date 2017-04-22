﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest {

    public enum QuestProgress {IN_PROGRESS, DONE}
    public enum QuestHexType { RIVER, ROAD, ANIMAL, MOUNTAIN, BUILDING }
    public enum QuestType { SOLO, GROUP}

    public string title;
    public int id;
    public QuestHexType hexType;
    public QuestType playerType;
    public QuestProgress progress;
    public string description;
    public int pointReward;

}



