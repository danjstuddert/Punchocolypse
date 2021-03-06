﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.XR;

[RequireComponent(typeof(Player))]
public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private bool canRotate;
    [Range(0.1f, 1f), SerializeField] private float rotationSensitivity;
    [SerializeField] private float rotationAmount;
    [Tooltip("A time to delay after teleporting to prevent turning instantly after teleporting")]
    [SerializeField] private float afterTeleportDelay;

    public bool CanRotate { get { return canRotate; } }
    public float RotationSensitivity { get { return rotationSensitivity; } }

    private Player player;
    private List<Hand> hands;

    private bool hasRotated;
    private bool teleportHeld;
    private float teleportCount;
    private VRDevice VRDevice;

    private void Start()
    {
        player = GetComponent<Player>();

        VRDevice = GetVRDevice.GetDevice(XRDevice.model);

        hands = new List<Hand>();
        hands.AddRange(player.hands);
    }

    private void Update()
    {
        CheckHandInput();
    }

    private void CheckHandInput()
    {
        if (canRotate == false)
            return;

        if (Teleport.instance && Teleport.instance.TeleportButtonHeld)
        {
            if (teleportHeld == false)
            {
                hasRotated = true;
                teleportHeld = true;
            }

            return;
        }

        if (hasRotated || teleportHeld)
        {
            if (VRDevice == VRDevice.Occulus && ( hands[0].controller.GetAxis().x == 0
            && hands[0].otherHand.controller.GetAxis().x == 0))
            {
                ResetRotate();
            }
            return;
        }

        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].controller == null)
                continue;

            if (VRDevice == VRDevice.Vive && hands[i].GetStandardInteractionButtonDown() && hands[i].controller.GetAxis().x >= rotationSensitivity || 
                VRDevice == VRDevice.Occulus && hands[i].controller.GetAxis().x >= rotationSensitivity)
                Rotate(rotationAmount);
            else if (VRDevice == VRDevice.Vive && hands[i].GetStandardInteractionButtonDown() && hands[i].controller.GetAxis().x <= -rotationSensitivity || 
                VRDevice == VRDevice.Occulus && hands[i].controller.GetAxis().x <= -rotationSensitivity)
                Rotate(-rotationAmount);
        }
    }

    private void Rotate(float direction)
    {
        Vector3 angle = player.trackingOriginTransform.eulerAngles;
        angle.y += direction;
        angle.y = ClampAngle(angle.y);

        player.trackingOriginTransform.rotation = Quaternion.Euler(angle);
        hasRotated = true;
    }

    private void ResetRotate()
    {
        teleportHeld = false;
        hasRotated = false;
    }

    private static float ClampAngle(float angle)
    {
        if (angle > 360)
            angle -= 360;
        else if (angle < 0)
            angle += 360;

        return angle;
    }
}
