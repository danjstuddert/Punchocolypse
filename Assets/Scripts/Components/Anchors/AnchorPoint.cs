using UnityEngine;

public enum Axis { X, Y, Z }
public class AnchorPoint : Hoverable
{
    [SerializeField] private Material anchoredMaterial;
    [SerializeField] private GameObject anchorHoverObject;
    [Tooltip("Turn this object on when anchored to the bow")]
    [SerializeField] protected GameObject anchoredObject;
    [SerializeField] private bool startsLocked;

    [Header("Particles")]
    [SerializeField] private GameObject anchoredParticle;
    [SerializeField] private GameObject anchorBrokenParticle;
    [SerializeField] private Vector3 particleSpawnAdjustment;
    [SerializeField] private bool debugParticleSpawn;

    public bool IsLocked { get { return isLocked; } }

    protected bool isLocked;

    private BowAnchor bowAnchor;
    private Renderer myRenderer;
    private Material originalMaterial;

    private Vector3 hitPosition;

    protected virtual void Start()
    {
        if (anchoredMaterial)
        {
            myRenderer = GetComponent<Renderer>();

            if (myRenderer == null)
                myRenderer = GetComponentInChildren<Renderer>();

            if (myRenderer != null)
                originalMaterial = myRenderer.material;
        }

        if (startsLocked)
            isLocked = true;

        if (anchoredObject && anchoredObject.activeInHierarchy)
            anchoredObject.SetActive(false);

        if (anchorHoverObject && anchorHoverObject.activeInHierarchy)
            anchorHoverObject.SetActive(false);

        hitPosition = transform.position;
    }

    public virtual void Anchored(Vector3 anchorPoint)
    {
        if (isLocked)
            return;

        hitPosition = anchorPoint - transform.position;
        float distance = hitPosition.magnitude;
        hitPosition = transform.position + (hitPosition / distance);

        if (bowAnchor == null)
            bowAnchor = FindObjectOfType<BowAnchor>();

        if (myRenderer)
            myRenderer.material = anchoredMaterial;

        if (anchoredObject)
           anchoredObject.SetActive(true);

        bowAnchor.AnchorObject(this);

        if (anchorHoverObject && anchorHoverObject.activeInHierarchy)
            anchorHoverObject.SetActive(false);

        if (anchoredParticle)
            ParticleController.Instance.SpawnParticle(anchoredParticle, anchorPoint + particleSpawnAdjustment, transform.rotation);
    }

    public virtual void Unlock()
    {
        if (isLocked)
            isLocked = false;
    }

    public virtual void MoveObject(Vector3 velocity, Vector3 followingPoint) {}

    public virtual void OnAnchorExit()
    {
        if (myRenderer)
            myRenderer.material = originalMaterial;

        if (anchorHoverObject && anchorHoverObject.activeInHierarchy)
            anchorHoverObject.SetActive(false);
    }

    public override void OnBowHover()
    {
        if (anchoredMaterial && isLocked == false)
            myRenderer.material = anchoredMaterial;

        if (anchorHoverObject && anchorHoverObject.activeInHierarchy == false)
            anchorHoverObject.SetActive(true);
    }

    public override void OnBowHoverExit()
    {
        if (anchoredMaterial && isLocked == false)
            myRenderer.material = originalMaterial;

        if (anchorHoverObject && anchorHoverObject.activeInHierarchy)
            anchorHoverObject.SetActive(false);
    }

    protected virtual void BreakAnchor(Vector3 anchorPoint)
    {
        bowAnchor.BreakAnchor();
        OnBowHoverExit();
        OnAnchorExit();

        if (anchorBrokenParticle)
        {
            hitPosition = anchorPoint - transform.position;
            float distance = hitPosition.magnitude;
            hitPosition = transform.position + (hitPosition / distance);

            ParticleController.Instance.SpawnParticle(anchorBrokenParticle, hitPosition + particleSpawnAdjustment, transform.rotation);
        }

        if (anchoredObject)
            anchoredObject.SetActive(false);
    }

#if UNITY_EDITOR
    protected virtual void OnDrawGizmosSelected()
    {
        if (debugParticleSpawn)
        {
            Gizmos.DrawSphere(transform.position + particleSpawnAdjustment, 0.15f);
        }
    }
#endif
}
