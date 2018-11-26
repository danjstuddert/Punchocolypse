using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DirtyPlayerCheckpoint : MonoBehaviour {
    [SerializeField] private float fadeWaitTime;
    [SerializeField] private float fadeTime;
    [SerializeField] private GameObject blackout;

    private Renderer rend;

    private void Start()
    {
        if (blackout.activeInHierarchy == false)
            blackout.SetActive(true);

        if(DirtyCheckpointController.Instance)
            DirtyCheckpointController.Instance.RelocatePlayer(Player.Instance);

        rend = blackout.GetComponent<Renderer>();

        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(fadeWaitTime);

        float t = 0f;

        while(t < fadeTime)
        {
            t += Time.deltaTime;

            rend.material.SetColor("_Color",
                new Color(0, 0, 0, Mathf.Lerp(1, 0, t / fadeTime)));

            yield return null;
        }

        blackout.SetActive(false);
    }
}
