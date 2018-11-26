using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class GrabEvent : MonoBehaviourExtended {
    [SerializeField] private LayerMask grabLayer;
    [SerializeField] private UnityEvent onGrab;

    [Header("Delays")]
    [SerializeField] private float handOutDelay;
    [SerializeField] private bool singleGrabOnly;
    [SerializeField] private float grabResetDelay;

    private Hand handIn;
    private bool hasGrabbed;

    private void Update()
    {
        if (IsRendering() == false)
            return;

        CheckHand();
    }

    private void CheckHand()
    {
        if (handIn == null || hasGrabbed)
            return;

        if (handIn.GetStandardInteractionButtonDown())
            HandleGrab();
    }

    private void HandleGrab()
    {
        onGrab.Invoke();

        hasGrabbed = true;

        if (singleGrabOnly)
            return;

        this.Invoke(ResetGrab, grabResetDelay);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (grabLayer == (grabLayer | (1 << other.gameObject.layer)) && other.GetComponent<Hand>())
        {
            handIn = other.GetComponent<Hand>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (handIn == null)
            return;

        if (grabLayer == (grabLayer | (1 << other.gameObject.layer)) && other.gameObject == handIn.gameObject)
        {
            this.Invoke(HandOut, handOutDelay);
        }
    }

    private void ResetGrab()
    {
        hasGrabbed = false;
    }

    private void HandOut()
    {
        handIn = null;
    }
}
