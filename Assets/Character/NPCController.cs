using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour
{
    public int npcID;
    public DialogueManager dialogueManager;
    // // Start is called before the first frame update
    // void Start()
    // {

    // }

    // // Update is called once per frame
    // void Update()
    // {

    // }


    private void OnTriggerEnter(Collider other)
    {
        // dialogueManager.dialogbuttonInit(npcID);
        Debug.Log("我是NPC");
        dialogueManager.dialogbuttonInit(npcID);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnTriggerExit(Collider other)
    {
        dialogueManager.DialogButtonOver();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
