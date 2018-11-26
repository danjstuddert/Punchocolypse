using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CheckPointController : MonoBehaviour {
    [SerializeField] private string checkpointTag;
    [SerializeField] private LayerMask playerLayer;

    private Player player;

    private Checkpoint[] checkPoints;
    private Checkpoint currentCheckPoint;

    private void Awake()
    {
        player = Player.Instance;

        GameObject[] points = GameObject.FindGameObjectsWithTag(checkpointTag);
        checkPoints = new Checkpoint[points.Length];

        for (int i = 0; i < points.Length; i++)
        {
            checkPoints[i] = points[i].GetComponent<Checkpoint>();
            checkPoints[i].Init(this, playerLayer);
        }
    }

    public void PlayerDead()
    {
        // Player hasn't reached a checkpoint yet, just reload the level
        if (currentCheckPoint == null)
            GameController.Instance.ReloadLevel();
        else
            StartCoroutine(Reset());
    }

    public void UpdateCurrentCheckPoint(Checkpoint currentCheckPoint)
    {
        this.currentCheckPoint = currentCheckPoint;
    }

    private IEnumerator Reset()
    {
        currentCheckPoint.ReloadToPoint(player);

        yield return new WaitForEndOfFrame();

        player.GetComponentInChildren<DamageablePlayer>().Reset();
    }
}
