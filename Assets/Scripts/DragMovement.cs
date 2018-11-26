using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DragMovement : MonoBehaviour
{
    [SerializeField] private float minDragDistance;
    [SerializeField] private float movementAmount = 2f;

    private Hand currentHand;

    private Transform playArea;
    private Vector3 lastGrabPosition;
    private Vector3 currentGrabPosition;
    private Vector3 dragDirection;

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
       if(currentHand != null && currentHand.controller != null && currentHand.controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        return true;

        return false;

    }

    private void UpdateMovement()
    {
        //if hand has not moved greater than min drag distance, return
        if(m_transform.position.DirectionTo(lastGrabPosition).sqrMagnitude < minDragDistance) return;

        currentGrabPosition = m_transform.position;

        dragDirection = currentGrabPosition.DirectionTo(lastGrabPosition, true);
        dragDirection = dragDirection.Flat();

        playArea.transform.position += dragDirection * (movementAmount * Time.deltaTime);
        lastGrabPosition = transform.position;
    }

}

