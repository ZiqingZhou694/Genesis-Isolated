using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DotLiquid.Tags;

public class DialogueManager : MonoBehaviour
{
    public List<Con_DialogData> allDialoglist = new List<Con_DialogData>();//所以对话列表

    public Transform dialogueButtons; // 对话按钮
    public Object ButtonsItem; //对话按钮预制体

    public Transform dialogueReply; //回复按钮
    public Object ReplyItem;

    public Transform dialogueBox; // 对话框
    public Dialoglogic dialoglogic; //对话逻辑处理类

    public ButtonManager buttonManager;
    public bool firstDialogueCompleted = false;
    private int currentDialogIndex = 0;

    public void dialogbuttonInit(int npcID)
    {
        dialogueButtons.gameObject.SetActive(true);
        dialogueReply.gameObject.SetActive(false);
        clearButton(dialogueButtons);
        // for(int i=0; i < allDialoglist.Count; i++){
        //     if(allDialoglist[i].NPCID == npcID){
        //         GameObject selectButton = (GameObject)Instantiate(ButtonsItem, dialogueButtons);
        //         selectButton.GetComponent<DialogueItem>().Initbuttom(allDialoglist[i], this);
        //     }
        // }
    

        // public void dialogbuttonInit(int npcID, int dialogIndex = 0)
        // if (dialogIndex < allDialoglist.Count && allDialoglist[dialogIndex].NPCID == npcID)
        // {
        //     GameObject selectButton = (GameObject)Instantiate(ButtonsItem, dialogueButtons);
        //     selectButton.GetComponent<DialogueItem>().Initbuttom(allDialoglist[dialogIndex], this);
        // }
        //dialogueManager.dialogbuttonInit(npcID, 0); // 显示第一个对话
        //dialogueManager.dialogbuttonInit(npcID, 1); // 显示第二个对话
        //dialogueManager.dialogbuttonInit(npcID, 2); // 显示第三个对话

        //   public void dialogbuttonInit(int npcID, bool showNextDialog = false)
        // if (showNextDialog)
        // {
        //     // 选择要显示的对话元素
        //     int dialogIndex = firstDialogueCompleted ? 1 : 0;

        //     if (allDialoglist[dialogIndex].NPCID == npcID)
        //     {
        //         GameObject selectButton = (GameObject)Instantiate(ButtonsItem, dialogueButtons);
        //         selectButton.GetComponent<DialogueItem>().Initbuttom(allDialoglist[dialogIndex], this);
        //     }
        // }
        // else
        // {
        //     for (int i = 0; i < allDialoglist.Count; i++)
        //     {
        //         if (allDialoglist[i].NPCID == npcID)
        //         {
        //             GameObject selectButton = (GameObject)Instantiate(ButtonsItem, dialogueButtons);
        //             selectButton.GetComponent<DialogueItem>().Initbuttom(allDialoglist[i], this);
        //         }
        //     }
        // }
        //dialogueManager.dialogbuttonInit(npcID); // 显示所有对话列表
        //dialogueManager.dialogbuttonInit(npcID, true); // 显示第一个或第二个对话列表元素


        // while (currentDialogIndex < allDialoglist.Count && allDialoglist[currentDialogIndex].NPCID != npcID)
        // {
        //     currentDialogIndex++;
        // }
        //
        // if (currentDialogIndex < allDialoglist.Count)
        // {
        //     GameObject selectButton = (GameObject)Instantiate(ButtonsItem, dialogueButtons);
        //     selectButton.GetComponent<DialogueItem>().Initbuttom(allDialoglist[currentDialogIndex], this);
        //       // 如果当前对话不是最后一个，则递增索引
        // if (currentDialogIndex < allDialoglist.Count - 1)
        // {
        //     currentDialogIndex++;
        // }
        // }
        
        
        while (currentDialogIndex < allDialoglist.Count && allDialoglist[currentDialogIndex].NPCID != npcID)
        {
            currentDialogIndex++;
        }
        
        if (currentDialogIndex < allDialoglist.Count)
        {
            GameObject selectButton = (GameObject)Instantiate(ButtonsItem, dialogueButtons);
            selectButton.GetComponent<DialogueItem>().Initbuttom(allDialoglist[currentDialogIndex], this);
            if (allDialoglist[currentDialogIndex].DialogStatus == DialogStatus.Quest)
            {
                if (allDialoglist[currentDialogIndex].QuestCompleted)
                {
                    currentDialogIndex++;
                }
                // return;
                 if(!allDialoglist[currentDialogIndex].QuestCompleted)
                    return;
            }
            // 如果当前对话不是最后一个，则递增索引
            if (currentDialogIndex < allDialoglist.Count - 1)
            {
                currentDialogIndex++;
            }
        }
        
        Invoke("InitItem", 0.02F);
    }

    void InitItem(){
        buttonManager.InitItem();
    }

    public void DialogButtonOver(){
        dialogueButtons.gameObject.SetActive(false);
        dialogueReply.gameObject.SetActive(false);
        dialogueBox.gameObject.SetActive(false);
    
    }

    public void dialogReplyInit(Con_DialogData con_DialogData, int currentLine)
    {
        dialogueReply.gameObject.SetActive(true);
        clearButton(dialogueReply);
        dialogueButtons.gameObject.SetActive(false);
        for (int i = 0; i < con_DialogData.dialoguedatas[currentLine].diaselect.Length; i++)
        {
            GameObject selectButton = (GameObject)Instantiate(ReplyItem, dialogueReply);
            selectButton.GetComponent<DialogueItem>().InitSelect(this, con_DialogData, currentLine, i);
        }
    }

    void clearButton(Transform transform)
    {
        for (int i =0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void dialogPlay(Con_DialogData Dialog, int start)
    {
        dialogueBox.gameObject.SetActive(true);
        dialogueReply.gameObject.SetActive(false);
        dialogueButtons.gameObject.SetActive(false);

        dialoglogic.DialogInit(Dialog, start);
    }

    public void FinshDialog()
    {
        dialogueBox.gameObject.SetActive(false);
    }

    public void DoEvent(string EventName)
    {
        Invoke(EventName, 0.01F);
    }

    public void TriggerNextDialogue()
    {
        if (firstDialogueCompleted)
        {
            return;
        }

        firstDialogueCompleted = true;

        // 在这里触发新的对话
        // 假设新的对话是allDialoglist中的第二个元素，从索引0开始：
        Con_DialogData newDialogue = allDialoglist[1];
        int start = 0;
        dialogPlay(newDialogue, start);
    }

}
