using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Dialoglogic: MonoBehaviour
{
    public DialogueManager Manager;
    public int currentLine;
    public Con_DialogData con_DialogData;
    // public Text DialogText;
    // public Text CharName;
    public TextMeshProUGUI DialogText;
    public TextMeshProUGUI CharName;
    private bool canNext = true;
    public AudioSource m_SounAudio;
    // public Con_DialogData newDialog;
    public GameObject AutoStart;
    public GameObject AutoEnd;
    private bool autoPlay = false;
    private bool autoPlayNext = true;
    // private bool firstDialogueCompleted = false;
    


    public AudioClip next;

    private void Awake()
    {
        m_SounAudio = this.gameObject.AddComponent<AudioSource>();
    }

    public void DialogInit(Con_DialogData DialogData, int start)
    {
        con_DialogData = DialogData;
        currentLine = start;
        canNext = true;
        autoPlayNext = true;
        AutoPlayEnd();
        playDialog();
    }

    void Update()
    {
        if (con_DialogData == null)
        {
            return;
        }

        if (autoPlay)
        {
            if (currentLine < con_DialogData.dialoguedatas.Length && autoPlayNext)
            {
                playDialog();
            }else if (currentLine >= con_DialogData.dialoguedatas.Length && autoPlayNext)
            {
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            if (currentLine < con_DialogData.dialoguedatas.Length && Input.GetKeyDown(KeyCode.G) && canNext)
                playDialog();
            else if(currentLine >= con_DialogData.dialoguedatas.Length && Input.GetKeyDown(KeyCode.G) && canNext)
            {
                this.gameObject.SetActive(false);
            }
        }
    }

    public void playDialog()
    {
        m_SounAudio.PlayOneShot(next);
        DialogText.text = "";
        StopCoroutine("ShowDialog");
        // StartCoroutine(ShowDialog(con_DialogData.dialoguedatas[currentLine].Dialogue));
        canNext = false;
        autoPlayNext = false;
        
        //播放语音
        if(con_DialogData.dialoguedatas[currentLine].DialogueVoice != null)
            m_SounAudio.PlayOneShot(con_DialogData.dialoguedatas[currentLine].DialogueVoice);

        //改变名字
        CharName.text = con_DialogData.dialoguedatas[currentLine].charName;
        CharName.color = con_DialogData.dialoguedatas[currentLine].NameColor;

        // StartCoroutine("ShowDialog");
        StartCoroutine(ShowDialog(con_DialogData.dialoguedatas[currentLine].Dialogue));

        if (con_DialogData.dialoguedatas[currentLine].Event != "")
            Manager.DoEvent(con_DialogData.dialoguedatas[currentLine].Event);

        if (!autoPlay)
            Invoke("CanNext", 1.5F);
    }
    
    IEnumerator ShowDialog(string message)
    {
        foreach (char c in con_DialogData.dialoguedatas[currentLine].Dialogue)
        {
            DialogText.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        if (autoPlay)
        {
            yield return new WaitForSeconds(1f);
            autoPlayNext = true;
            CanNext();
        }
    }

    void CanNext()
    {
        if (con_DialogData.dialoguedatas[currentLine].end)
        {
            this.gameObject.SetActive(false);
            // 添加以下代码以在对话结束时更新按钮的激活状态
            AutoStart.SetActive(false);
            AutoEnd.SetActive(false);

            // // 如果第一次对话结束，触发新的对话
            // Manager.TriggerNextDialogue();
             Manager.firstDialogueCompleted = true;
        }

        if (con_DialogData.dialoguedatas[currentLine].diaselect.Length == 0)
        {
            canNext = true;
            currentLine++;

        }
        else
        {
            autoPlayNext = false;
            canNext = false;
            Manager.dialogReplyInit(con_DialogData, currentLine);
            
        }
    }

    public void AutoPlayStart()
    {
        autoPlay = true;
        AutoStart.SetActive(false);
        AutoEnd.SetActive(true);
        autoPlayNext = true;
    }

    public void AutoPlayEnd()
    {
        autoPlay = false;
        AutoStart.SetActive(true);
        AutoEnd.SetActive(false);
        canNext = true;
    }

}
