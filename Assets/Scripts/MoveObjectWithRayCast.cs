using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectWithRayCast : MonoBehaviour
{
    public float distanceFromCamera = 3.0f; // distance from camera
    private Camera mainCamera;
    private bool isDragging = false;
    private GameObject objectToDrag;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.gameObject.tag == "firestarter") {
                    objectToDrag = hitInfo.collider.gameObject;
                    isDragging = true;
                }
                if(hitInfo.collider.gameObject.tag == "bottle")
                {
                    objectToDrag = hitInfo.collider.gameObject;
                    isDragging = true;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging && objectToDrag != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                objectToDrag.transform.position = mainCamera.transform.position + ray.direction * distanceFromCamera;
            }
        }
    }
}
