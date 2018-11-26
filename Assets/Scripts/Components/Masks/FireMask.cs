using UnityEngine;
using Valve.VR.InteractionSystem;

public class FireMask : Attachable
{
    [SerializeField] private float arrowCheckTime;

    private ArrowHand arrowHand;

    private float checkCount;

    private void Update()
    {
        CheckArrow();
    }

    public override void OnAttachedToFace()
    {
        base.OnAttachedToFace();

        //Find the bow
        if (arrowHand == null)
            arrowHand = transform.root.GetComponentInChildren<ArrowHand>();
    }

    public override void OnDetachedFromFace()
    {
        base.OnDetachedFromFace();
        checkCount = 0f;
    }

    private void CheckArrow()
    {
        if (attachedToFace == false || arrowHand == null)
            return;

        if(checkCount < arrowCheckTime)
        {
            checkCount += Time.deltaTime;
            return;
        }

        checkCount = 0f;

        if (arrowHand.CurrentArrow && arrowHand.CurrentArrow.GetComponentInChildren<FireSource>().IsBurning == false)
            arrowHand.CurrentArrow.GetComponentInChildren<FireSource>().FireExposure(FireStrength.Huge);
    }
}

