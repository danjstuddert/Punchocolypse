using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DirtyCheckpointController : Singleton<DirtyCheckpointController>
{
    private List<DirtyCheckpoint> points;
    private int currentCheckpoint;

    private void Awake()
    {
        DirtyCheckpointController[] dirtyCheckpointControllers = FindObjectsOfType<DirtyCheckpointController>();

        for (int i = 0; i < dirtyCheckpointControllers.Length; i++)
        {
            if (dirtyCheckpointControllers[i] != this)
            {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(this);
    }

    public void UpdateCheckPoint(int checkPoint)
    {
        currentCheckpoint = checkPoint;
    }

    public void Reset()
    {
        currentCheckpoint = 0;
    }

    public void RelocatePlayer(Player player)
    {
        points = new List<DirtyCheckpoint>();

        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Checkpoint"))
        {
            if (go.GetComponent<DirtyCheckpoint>() == null)
                continue;

            points.Add(go.GetComponent<DirtyCheckpoint>());
        }

        if (points != null && points.Count > 0)
        {
            player.trackingOriginTransform.position = points.Find(p => p.Id == currentCheckpoint).transform.position;
            player.trackingOriginTransform.rotation = points.Find(p => p.Id == currentCheckpoint).transform.rotation;
            player.hmdTransforms[0].rotation = points.Find(p => p.Id == currentCheckpoint).transform.rotation;
        }
    }
}