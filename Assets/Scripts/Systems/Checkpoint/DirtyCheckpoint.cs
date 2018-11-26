using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyCheckpoint : MonoBehaviour {
    [SerializeField] private int checkpointId;

    public int Id { get { return checkpointId; } }
}
