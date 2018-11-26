using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(Throwable))]
public class Attachable : MonoBehaviour {
    protected bool attached;
    protected bool attachedToFace;

    public virtual void OnAttached()
    {
        attached = true;
    }

    public virtual void OnDetached()
    {
        attached = false;
    }

    public virtual void OnAttachedToFace()
    {
        attachedToFace = true;
    }

    public virtual void OnDetachedFromFace()
    {
        attachedToFace = false;
    }
}
