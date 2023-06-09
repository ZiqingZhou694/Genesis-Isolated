using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera PlayerCam;
    public Camera firestartercam;
    public GameObject firestarter;
    public GameObject player;
    public float distance = 2.0f;
    public Terrain terrain;
    public GameObject firebar;
    public Camera WaterCam;
    // Start is called before the first frame update
    void Start()
    {

    }

    //after finish with user input, spawn the firestarter in front of playercam
    public void SwitchToFireStarterCam(bool i) {
        if(i == true)
        {
            PlayerCam.gameObject.SetActive(false);
            firestartercam.gameObject.SetActive(true);
            firebar.SetActive(true);
        }
        if(i == false)
        {
            PlayerCam.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.Locked;
            firestartercam.gameObject.SetActive(false);
            MoveObjectInfrontOfPlayer(firestarter);
        }
    }

    public void SwitchToWaterCam(bool i)
    {
        if (i == true)
        {
            PlayerCam.gameObject.SetActive(false);
            WaterCam.gameObject.SetActive(true);
        }
        if (i == false)
        {
            PlayerCam.gameObject.SetActive(true);
            WaterCam.gameObject.SetActive(false);
        }
    }

    public void MoveObjectInfrontOfPlayer(GameObject obj)
    {
        Vector3 newPosition = player.transform.position + player.transform.forward * distance;
        obj.transform.position = newPosition;
        //Debug.Log(newPosition);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
