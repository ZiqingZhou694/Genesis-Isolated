using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueItem : MonoBehaviour
{
    Con_DialogData Dialog;

    DialogueManager Manager;

    Button Button;

    public Image state;

    public Sprite[] Imagestate;

    public TextMeshProUGUI nameText;

    private int StartIndex = 0;

    public GameObject SelectImage;

    

    public void Initbuttom(Con_DialogData dialog, DialogueManager manager)
    {
        this.Manager = manager;
        this.Dialog = dialog;
        this.Button = this.GetComponent<Button>();
        StartIndex = 0;
        Button.onClick.AddListener(dialogStart);
        stateInit(Dialog.DialogStatus);
    }

    public void InitSelect(DialogueManager manager, Con_DialogData dialog, int Start, int select)
    {
        this.Manager = manager;
        this.Dialog = dialog;
        this.Button = this.GetComponent<Button>();
        StartIndex = dialog.dialoguedatas[Start].diaselect[select].next;
        
 
        Button.onClick.AddListener(dialogStart);

        // 添加以下代码以更新按钮文本
        if (nameText != null)
        {
            nameText.text = dialog.dialoguedatas[Start].diaselect[select].selectText;
        }
    }

    public void dialogStart()
    {
        Manager.dialogPlay(Dialog,StartIndex);
    }

    // void stateInit(DialogStatus status)
    // {
    //     state.sprite = Imagestate[(int)status];
    //     nameText.text = Dialog.DialogName;
    // }
    void stateInit(DialogStatus status)
    {
        if ((int)status >= 0 && (int)status < Imagestate.Length)
        {
            state.sprite = Imagestate[(int)status];
            nameText.text = Dialog.DialogName;
        }
        else
        {
            Debug.LogError($"Invalid status value: {status}. Check the DialogStatus enum and the Imagestate array.");
        }
    }


}
