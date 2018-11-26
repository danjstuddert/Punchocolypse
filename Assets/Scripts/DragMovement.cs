using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DragMovement : MonoBehaviour
{
    private Hand currentHand;

    private Transform playArea;
    private Vector3 lastGrabPosition;
    private Vector3 currentGrabPosition;
    private Vector3 dragDirection;

    public float movementAmount = 2f;
    private Transform m_transform;
    private void Awake()
    {
        currentHand = GetComponent<Hand>();
        m_transform = transform;
        playArea = Player.Instance.gameObject.transform;
    }

    private void Update()
    {
        if (CheckForInput())
        {
            UpdateMovement();
        }
    }

    private bool CheckForInput()
    {
       if( currentHand.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        return true;

        return false;

    }

    private void UpdateMovement()
    {
        //if hand has not moved, return
        if(m_transform.position == lastGrabPosition) return;

        currentGrabPosition = m_transform.position;

        dragDirection = currentGrabPosition.DirectionTo(transform.position, true);
        playArea.Translate(dragDirection * movementAmount * Time.deltaTime);

        lastGrabPosition = transform.position;

    }

}

