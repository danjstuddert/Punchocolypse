using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour {
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private List<Resetable> resetables;

    private CheckPointController controller;
    private LayerMask playerLayer;

    private bool hasInitialised;
    private bool hasTriggered;

    public void Init(CheckPointController controller, LayerMask playerLayer)
    {
        if (GetComponent<BoxCollider>().isTrigger == false)
            GetComponent<BoxCollider>().isTrigger = true;

        if (respawnPoint == null)
            respawnPoint = transform;

        this.controller = controller;
        this.playerLayer = playerLayer;

        hasInitialised = true;
    }

    public void ReloadToPoint(Player player)
    {
        // Need to make sure everything is reset to how it was
        for (int i = 0; i < resetables.Count; i++)
        {
            resetables[i].Reset();
        }

        // Move the player
        player.trackingOriginTransform.position = respawnPoint.position;
        player.trackingOriginTransform.rotation = respawnPoint.rotation;
        player.hmdTransforms[0].rotation = respawnPoint.rotation;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || hasInitialised == false)
            return;

        if (playerLayer == (playerLayer | (1 << other.gameObject.layer)))
        {
            hasTriggered = true;
            controller.UpdateCurrentCheckPoint(this);
        }
    }
}
