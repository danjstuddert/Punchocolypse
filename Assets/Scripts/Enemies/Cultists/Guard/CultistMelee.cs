using BehaviorDesigner.Runtime;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class CultistMelee : MonoBehaviourExtended
{
    [SerializeField] private float damage;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float range;

    private CultistGuard guard;
    private Affectable target;

    private Transform targetTransform;

    protected override void Start()
    {
        base.Start();
        target = Player.Instance.trackingOriginTransform.GetComponentInChildren<Affectable>();
    }

    public void Attack()
    {
        if (target == null)
            return;

        if ((target.transform.position - transform.position).sqrMagnitude <= range * range)
        {
            target.Damage(damage, transform.position, transform.position);
        }
    }
}
