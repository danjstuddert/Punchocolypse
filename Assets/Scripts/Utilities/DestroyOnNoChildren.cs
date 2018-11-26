using UnityEngine;

public class DestroyOnNoChildren : MonoBehaviour {
    [SerializeField] private float checkTime;
    [SerializeField] private bool checkFromStart;

    private float checkCount;

    private void Start()
    {
        if (checkFromStart)
            StartChecking();
    }

    public void StartChecking()
    {
        this.InvokeRepeating(CheckChildren, 0, checkTime);
    }

    private void CheckChildren()
    {
        if (transform.childCount > 0)
            return;

        Destroy(gameObject);
    }
}
