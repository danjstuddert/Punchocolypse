using UnityEngine;

public class Overlay : MonoBehaviour {
    protected float fadeInTime;
    protected float displayTime;

    public virtual void Init(float fadeInTime, float displayTime)
    {
        this.fadeInTime = fadeInTime;
        this.displayTime = displayTime;

        Display();
    }

    protected virtual void Display()
    {

    }
}
