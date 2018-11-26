using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneAfterTime : MonoBehaviour {
    [SerializeField] private string levelToLoad;
    [SerializeField] private float time;

    private void Start()
    {
        this.Invoke(LoadLevel, time);
    }

    private void LoadLevel()
    {
        if (string.IsNullOrEmpty(levelToLoad))
        {
            Debug.LogError("Tried to load level but no level specified");
            return;
        }

        GameController.Instance.LoadLevel(levelToLoad);
    }
}
