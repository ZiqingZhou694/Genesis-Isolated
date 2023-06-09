using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public CharacterController characterController;
    public float speed;
    private Vector3 camRotation;
    private Transform cam;
    private Vector3 moveDirection;
    Animator anim;


    [Range(-45, -15)]
    public int minAngle = -30;
    [Range(30, 80)]
    public int maxAngle = 45;
    [Range(0, 500)]
    public int sensitivity = 20;

    private void Awake()
    {
        cam = Camera.main.transform;
        //play = player.transform;
    }

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        Move();
    }
    void Update()
    {
        Rotate();

    }

    private void Rotate()
    {
        transform.Rotate(Vector3.up * sensitivity * Time.deltaTime * Input.GetAxis("Mouse X"));

        camRotation.x -= Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        camRotation.x = Mathf.Clamp(camRotation.x, minAngle, maxAngle);

        cam.localEulerAngles = camRotation;
    }

    private void Move()
    {
        Vector3 InputPlayer = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        };
        //float horizontalMove = Input.GetAxis("Horizontal");
        //float verticalMove = Input.GetAxis("Vertical");
        if (InputPlayer != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }
        if (characterController.isGrounded)
        {
            moveDirection = new Vector3(InputPlayer.x, 0, InputPlayer.z);
            moveDirection = transform.TransformDirection(moveDirection);
        }

        moveDirection.y -= 9.181f * Time.deltaTime;
        characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}
