using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveSmoothTime;
    public float GravityStrength;
    public float jump_strength;
    public float walk_speed;
    public float run_speed;
    Animator anim;
    private CharacterController Controller;
    private Vector3 CurrentMoveVelocity;
    private Vector3 MoveDampVelocity;

    private Vector3 CurrentForceVelocity;

    void Start()
    {
        Controller = GetComponent<CharacterController>();
        anim = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 InputPlayer = new Vector3
        {
            x = Input.GetAxisRaw("Horizontal"),
            y = 0f,
            z = Input.GetAxisRaw("Vertical")
        };

        if (InputPlayer != Vector3.zero)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (InputPlayer.magnitude > 1f)
        {
            InputPlayer.Normalize();
        }

        Vector3 MoveVector = transform.TransformDirection(InputPlayer);
        float currentspeed = Input.GetKey(KeyCode.LeftShift) ? run_speed : walk_speed;

        CurrentMoveVelocity = Vector3.SmoothDamp(CurrentMoveVelocity, MoveVector * currentspeed, ref MoveDampVelocity, MoveSmoothTime);

        Controller.Move(CurrentMoveVelocity * Time.deltaTime);

        Ray groundCheckRay = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(groundCheckRay, 1.1f))
        {
            CurrentForceVelocity.y = -2f;
            if (Input.GetKey(KeyCode.Space))
            {
                CurrentForceVelocity.y = jump_strength;
            }
        }
        else
        {
            CurrentForceVelocity.y -= GravityStrength * Time.deltaTime;
        }

        Controller.Move(CurrentForceVelocity * Time.deltaTime);
    }

}
