using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FireProgressScript : MonoBehaviour
{
    private Image fireLevel;
    private float currentAmt;
    private float maxAmt = 100f;
    private bool leftKeyPressed;
    private bool rightKeyPressed;
    public GameObject fire;
    private GameObject camManager;
    public GameObject progressbar;
    public TextMeshProUGUI instruction;
    bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        leftKeyPressed = false;
        rightKeyPressed = false; ;
        currentAmt = 0f;
        fireLevel = GetComponent<Image>();
        camManager = GameObject.Find("CameraManager");
    }

    // Update is called once per frame
    void Update()
    {
        // Get consecutive left and right key press to increase fire level
        if (Input.GetKey(KeyCode.LeftArrow) && !leftKeyPressed)
        {
            leftKeyPressed = true;
            rightKeyPressed = false;
            FireLevelIncrease();
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !rightKeyPressed)
        {
            rightKeyPressed = true;
            leftKeyPressed = false;
            FireLevelIncrease();
        }

        // Decrease fire level over time
        if (currentAmt >= 100)
        {
            // fire start animation
            fire.SetActive(true);
            StartCoroutine(Wait());

        }
        else if (currentAmt >= 70)
        {
            fireLevel.color = new Color(1f, 0f, 0f);
            currentAmt = currentAmt - (float)25.0 * Time.deltaTime;
            if(first == true) {
                instruction.text = "Elon: Can't believe how hard it is without a lighter!!";
                StartCoroutine(Hidetext());
            }
        }
        else if (currentAmt >= 40)
        {
            fireLevel.color = new Color(1f, 0.32f, 0f);
            currentAmt = currentAmt - (float)15.0 * Time.deltaTime;
        }
        else if (currentAmt > 0)
        {
            fireLevel.color = new Color(1f, 0.55f, 0f);
            currentAmt = currentAmt - (float)10.0 * Time.deltaTime;
            instruction.text = "";
        }
        else if (currentAmt == 0)
        {
            instruction.text = "Hit right and left arrow buttons to apply friction";
        }

        // Update fill amount
        fireLevel.fillAmount = currentAmt / maxAmt;
    }
    IEnumerator Wait()
    {
        yield return new WaitForSecondsRealtime(5);
        camManager.SendMessage("SwitchToFireStarterCam", false);
        progressbar.SetActive(false);
    }

    IEnumerator Hidetext()
    {
        yield return new WaitForSecondsRealtime(2);
        instruction.text = "";
        first = false;
    }

    void FireLevelIncrease()
    {
        currentAmt = currentAmt + 5;
    }
}
