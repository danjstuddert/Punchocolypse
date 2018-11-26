using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayMushroom : Overlay
{
    [SerializeField] private GameObject distortionLayer;
    [SerializeField] private GameObject colourLayer;

    private Renderer distortionRenderer;
    private Renderer colourRenderer;
    private float distortionOpacity;
    private Color colourOpacity;

    public override void Init(float fadeInTime, float displayTime)
    {
        distortionRenderer = distortionLayer.GetComponent<Renderer>();
        colourRenderer = colourLayer.GetComponent<Renderer>();

        distortionOpacity = distortionRenderer.material.GetFloat("_Opacity");
        colourOpacity = colourRenderer.material.GetColor("_Color");

        base.Init(fadeInTime, displayTime);
    }

    protected override void Display()
    {
        StartCoroutine(FadeScreen());
    }

    private IEnumerator FadeScreen()
    {
        float t = 0f;

        Color colourRendererEnd = colourOpacity;
        Color colourRendererStart = colourOpacity;
        colourRendererStart.a = 0f;

        distortionRenderer.material.SetFloat("_Opacity", 0);
        colourRenderer.material.SetColor("_Color", colourRendererStart);


        while (t < fadeInTime)
        {
            t += Time.deltaTime;

            distortionRenderer.material.SetFloat("_Opacity", Mathf.Lerp(0, distortionOpacity, t / fadeInTime));
            colourRenderer.material.SetColor("_Color", Color.Lerp(colourRendererStart, colourRendererEnd, t / fadeInTime));

            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        t = 0f;

        while (t < fadeInTime)
        {
            t += Time.deltaTime;

            distortionRenderer.material.SetFloat("_Opacity", Mathf.Lerp(distortionOpacity, 0, t / fadeInTime));
            colourRenderer.material.SetColor("_Color", Color.Lerp(colourRendererEnd, colourRendererStart, t / fadeInTime));
            yield return null;
        }
    }
}
