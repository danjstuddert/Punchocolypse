using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [Header("Level Loading")]
    [SerializeField] private string deadLevel;
    [SerializeField] private float levelLoadDelay;

    [Header("Fade Out")]
    [SerializeField] private GameObject blackout;
    [SerializeField] private float fadeInTime;
    [SerializeField] private float fadeDelayTime;

    private Renderer rend;

    public string SceneName { get { return SceneManager.GetActiveScene().name; } }

    private void Start()
    {
        if(blackout != null)
            rend = blackout.GetComponent<Renderer>();
    }

    public void EndGame()
    {
        SteamVR_LoadLevel.Begin(deadLevel);
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(BeginLoadLevel(levelName));
    }

    public void ReloadLevel()
    {
        StartCoroutine(BeginLoadLevel(SceneManager.GetActiveScene().name));
    }

    public void Quit()
    {
        Application.Quit();
    }

    private IEnumerator BeginLoadLevel(string levelName)
    {
        blackout.SetActive(true);

        float t = 0f;

        while (t < fadeInTime)
        {
            t += Time.deltaTime;

            rend.material.SetColor("_Color",
                new Color(0, 0, 0, Mathf.Lerp(0, 1, t / fadeInTime)));

            yield return null;
        }

        yield return new WaitForSeconds(fadeDelayTime);

        SteamVR_LoadLevel.Begin(levelName);
    }
}
