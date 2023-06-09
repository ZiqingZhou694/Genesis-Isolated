using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MenuManager : MonoBehaviour
{
    public GameObject startmenu;
    public GameObject pausemenu;

    public GameObject introVideoCanvas;
    public GameObject introVideoClip;
    // Start is called before the first frame update
    void Start()
    {
        startmenu.SetActive(true);
        pausemenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void OnClickBeginButton()
    {
        startmenu.SetActive(false);
        introVideoCanvas.SetActive(true);
        introVideoClip.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnClickResumeButton()
    {
        pausemenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }

    public void OnClickExitButton()
    {
        pausemenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausemenu.SetActive(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            introVideoCanvas.SetActive(false);
            introVideoClip.SetActive(false);
        }
    }
}
