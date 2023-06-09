using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DialogStatus
{
    Com = 0,//普通
    Quest,//任务
    store,
}

[CreateAssetMenu(fileName = "Configuration", menuName = "DialogSystem/DialogConf")]
public class Con_DialogData : ScriptableObject
{
    public int NPCID;
    public string DialogName;
    public DialogStatus DialogStatus;
    public DialogData[] dialoguedatas;
    
    // 新增属性 QuestCompleted
    public bool QuestCompleted = false;

    // 新增属性 triggerConditionMet
    // public bool triggerConditionMet = false;
    
    [Serializable]
    public struct DialogData
    {
        public string charName;
        public Color NameColor;
        [TextArea(1,5)]
        public string Dialogue;
        public AudioClip DialogueVoice;
        public selectDialog[] diaselect;
        public string Event;
        public bool end;
    }
    [Serializable]
    public struct selectDialog{
        public int next;
        public string selectText;
    }
}
