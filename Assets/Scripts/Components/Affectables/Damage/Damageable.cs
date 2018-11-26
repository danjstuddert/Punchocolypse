using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class Damageable : Affectable
{
	[SerializeField] private DeathEffects deathEffect;

    [SerializeField] private float startingHealth;
    [Tooltip("Forces the damageable to take no damage for a time after taking damage. If set to 0 will ignore")]
    [SerializeField] private float invulnerabilityDuration;

    [Header("On Damage")]
	private float damageFlashTime = .2f;

    [Header("On Impact")]
    [SerializeField] protected ObjectWeight breakableWeight;
    [Range(0f, 10f), SerializeField] protected float minBreakVelocity;

 
    [SerializeField] private bool deparentOnDeath;

    [Header("Events")]
    [SerializeField] private UnityEvent onTakeDamage;
    [SerializeField] private UnityEvent onDeath;

    public float CurrentHealth { get; private set; }

    private float maxHealth;

    private bool hasColour;
    
	[SerializeField] private Color damageFlashColour = Color.white;
	private float flashDelayTime = .2f;

    private Vector3 previousCollisionLocation;
    private Vector3 previousImpactVelocity;

    private bool recentlyImpacted;
    private Color originalColour;
    private bool invulnerable;

    private GameObject deadParticle;

    private void OnEnable()
    {
        CurrentHealth = maxHealth = startingHealth;
    }

    protected override void Start()
    {
        base.Start();

        myRenderer = GetComponent<Renderer>();
        if (myRenderer == null)
        {
            myRenderer = GetComponentInChildren<Renderer>();
        }

        if (myRenderer != null)
        {
            hasColour = myRenderer.material.HasProperty("_Color");

            if (hasColour)
            {
                originalColour = myRenderer.material.color;
            }
        }
    }

    public override void Damage(float amount, Vector3 impactPosition, Vector3 impactVelocity, bool fromExplosion = false, bool instaKill = false)
    {
        if (instaKill)
            AdjustHealth(-startingHealth);

        if (invulnerable || ExplosionCheck(fromExplosion)) return;

        previousCollisionLocation = impactPosition;
        previousImpactVelocity = impactVelocity;

        StartCoroutine(Impact(damageFlashColour, impactPosition, -impactVelocity));

        AdjustHealth(-amount);
        onTakeDamage.Invoke();

        if (invulnerabilityDuration > 0)
        {
            invulnerable = true;
            this.Invoke(RemoveImmunity, invulnerabilityDuration);
        }
    }

    public void DamageEvent(float amount)
    {
        Damage(amount, transform.position, Vector3.zero);
    }

    public override void Heal(float amount)
    {
        if (maxHealth != 0)
        {
            AdjustHealth(amount);
        }
    }

    public void AddListener(UnityAction action)
    {
        onDeath.AddListener(action);
    }

    public override void Impacted(ObjectWeight weight, Vector3 impactPoint, Vector3 impactVelocity) {
        if (breakableWeight == ObjectWeight.None)
            return;

        if (weight >= breakableWeight && (minBreakVelocity == 0 || impactVelocity.magnitude >= minBreakVelocity))
			StartCoroutine(RemoveObject());
	}

    private void AdjustHealth(float amount)
    {
        if (maxHealth == 0 || CurrentHealth <= 0) return;

        CurrentHealth += amount;

        if (CurrentHealth > maxHealth)
        {
            CurrentHealth = maxHealth;
        }
        else if (CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        onDeath.Invoke();
        StartCoroutine(RemoveObject());
    }

    private void RemoveImmunity()
    {
        invulnerable = false;
    }

	public IEnumerator RemoveObject()
	{
		if (deathEffect == null)
			Debug.LogError (gameObject.name + "has no death effect found");

		if (deathEffect != null && deathEffect.deathObjects != null && deathEffect.deathObjects.Count > 0)
		{
			SimplePool.Spawn(deathEffect.deathObjects[Random.Range(0, deathEffect.deathObjects.Count)], transform.position, transform.rotation);

			//Play random sound from impactSounds
			AudioSource.PlayClipAtPoint(deathEffect.deathSounds[Random.Range(0, deathEffect.deathSounds.Count)], transform.position);

            if (deathEffect.deathParticle)
                ParticleController.Instance.SpawnParticle(deathEffect.deathParticle, transform.position, transform.rotation);

			if (deathEffect.deathObjectForceMultiplier > 0)
			{
				Collider[] hitColliders = Physics.OverlapSphere(previousCollisionLocation, 1f, deathEffect.forceLayer);

				foreach (Collider col in hitColliders)
				{
					if (col.GetComponent<Rigidbody>())
						col.GetComponent<Rigidbody>().AddForceAtPosition(previousImpactVelocity * deathEffect.deathObjectForceMultiplier,
							previousCollisionLocation);
				}
			}
		}

		yield return new WaitForEndOfFrame();
		SimplePool.Despawn(gameObject, deparentOnDeath);
	}

    private IEnumerator Impact(Color c, Vector3 impactPoint, Vector3 impactDirection)
    {
        if (recentlyImpacted == false)
        {
            recentlyImpacted = true;


            this.Invoke(ResetImpacted, flashDelayTime);

            if (myRenderer != null && damageFlashTime > 0 && hasColour)
            {
                myRenderer.material.color = c;
                float t = 0f;
                while (t <= damageFlashTime)
                {
                    t += Time.deltaTime;
                    yield return null;
                }
                myRenderer.material.color = originalColour;
            }
        }
    }

    private void ResetImpacted()
    {
        recentlyImpacted = false;
    }
}
