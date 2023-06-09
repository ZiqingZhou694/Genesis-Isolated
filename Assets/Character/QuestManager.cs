using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public Con_DialogData nextDialog;

    public void Start()
    {
        nextDialog.QuestCompleted = false;
    }
    public void CompleteQuest()
    {
        nextDialog.QuestCompleted = true;
    }
}
