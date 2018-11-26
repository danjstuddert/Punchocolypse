using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(VelocityEstimator))]
public class Damager : MonoBehaviourExtended
{
    [SerializeField] protected float damage;
    [SerializeField] protected LayerMask damagingMask;
    [SerializeField] protected bool instaKill;
    [SerializeField] protected float minDamageVelocity;
    [SerializeField] protected float stabForceMultiplier;

    private VelocityEstimator velocityEstimator;
    protected override void Start()
    {
        base.Start();
        velocityEstimator = GetComponent<VelocityEstimator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (velocityEstimator == null|| Mathf.Abs(velocityEstimator.GetVelocityEstimate().z) < minDamageVelocity)
            return;

        if (damagingMask == (damagingMask | (1 << other.gameObject.layer)))
        {
            Affectable a = other.transform.GetComponentInChildren<Affectable>();

            if (a)
                a.Damage(damage, transform.position, transform.position, false, instaKill);

            Rigidbody rBody = other.GetComponentInChildren<Rigidbody>();

            if (rBody)
                rBody.AddForce(velocityEstimator.GetVelocityEstimate() * stabForceMultiplier);
        }
    }
}
