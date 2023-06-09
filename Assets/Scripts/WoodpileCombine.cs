using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodpileCombine : MonoBehaviour
{
    public QuestGiver quest;
    public PlayerLogic logic;
    private float fireEXP = 200;
    public GameObject questMan;
    public GameObject root;

    private void Start()
    {
        quest = GameObject.FindGameObjectWithTag("questGiver").GetComponent<QuestGiver>();
        logic = FindObjectOfType<PlayerLogic>();
        questMan = GameObject.Find("MakeFireQuestManager");
        root = GameObject.Find("Root");
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object is the one we want to bind with
        if (collision.gameObject.tag == "firestarter")
        {
            quest.fireComplete = true;
            logic.DecreaseTechDepLevel(fireEXP);
            Object.Destroy(collision.gameObject);
            this.transform.Find("Fire").gameObject.SetActive(true);
            questMan.GetComponentInChildren<QuestManager>().CompleteQuest();
            root.GetComponent<DialogueManager>().dialogbuttonInit(0);
        }
    }
}
