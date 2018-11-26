using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class DamageablePlayer : Affectable
{
    [Header("Health Settings")]
    [SerializeField] private int startingHealth;
    [Tooltip("Delay before health starts to regen")]
    [SerializeField] private float healthRegenDelay;
    [Tooltip("How long, in seconds, it takes to regenerate a point of health")]
    [SerializeField] private float healthRegenTime;
    [SerializeField] private int lowHealthThreshold;
    [SerializeField] private AudioSource lowHealthSound;
    [SerializeField] private SoundPlayOneshot damageSound;
    [SerializeField] private SoundPlayOneshot deadSound;
    [SerializeField] private SoundPlayOneshot healSound;

    [Header("Overlay")]
    [SerializeField] private GameObject faceSphere;
    [SerializeField] private string dissolveShaderHandle;
    [SerializeField] private float dissolveInTime;
    [SerializeField] private AnimationCurve dissolveInCurve;
    [SerializeField] private float dissolveOutWait;
    [SerializeField] private float dissolveOutTime;
    [SerializeField] private AnimationCurve dissolveOutCurve;
    [Range(0f, 1f), SerializeField] private float minDissolveAmount;
    [Range(0f, 1f), SerializeField] private float maxDissolveAmount;
    [Range(0f, 1f), SerializeField] private float minDissolvePulseAmount;
    [SerializeField] private float pulseTime;

    [Header("UI")]
    [SerializeField] private HealthUI healthUI;

    [Header("Events")]
    [SerializeField] private UnityEvent onHit;
    [SerializeField] private UnityEvent onDeath;

    [Header("Debug Options")]
    [SerializeField] private bool invunerable;
    [SerializeField] private bool neverInstaKill;
    [SerializeField] private bool debugTakeDamage;
    [SerializeField] private bool debugHealDamage;

    private int currentHealth;
    private int maxHealth;

    private bool beenKilled;
    private float delayCount;
    private float regenCount;

    private bool isScaling;

    private Renderer faceSphereRenderer;
    private float faceSpherePercent;
    private float hitDamagePercent;

    protected override void Start()
    {
        base.Start();

        maxHealth = currentHealth = startingHealth;
        AdjustFaceSphere();

        hitDamagePercent = 1f / (maxHealth - 1f);

        if (faceSphere)
            faceSphereRenderer = faceSphere.GetComponent<Renderer>();
    }

    private void Update()
    {
        Regen();
        UpdateFaceSphere();

        if (debugTakeDamage)
        {
            debugTakeDamage = false;
            Damage(1, transform.position, Vector3.zero);
        }

        if (debugHealDamage)
        {
            debugHealDamage = false;
            Heal(1);
        }
    }

    public void Reset()
    {
        beenKilled = false;
        currentHealth = maxHealth;
        regenCount = delayCount = 0f;

        AdjustFaceSphere();
    }

    public override void Damage(float amount, Vector3 impactPosition, Vector3 impactVelocity, bool fromExplosion = false, bool instaKill = false)
    {
        if (beenKilled || invunerable)
            return;

        Debug.Log("Ouch!");

        if (damageSound)
            damageSound.Play();

        AdjustHealth(-amount);

        onHit.Invoke();
    }

    public override void Heal(float amount)
    {
        if (beenKilled || invunerable)
            return;

        if (healSound)
            healSound.Play();

        AdjustHealth(amount);
    }

    private void AdjustHealth(float amount)
    {
        currentHealth += (int)amount;
        AdjustFaceSphere();

        StopAllCoroutines();
        StartCoroutine(FaceSphereFade());
        regenCount = 0f;

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;

        if (currentHealth <= 0 && beenKilled == false)
        {
            beenKilled = true;
            faceSpherePercent = 0f;

            if (deadSound)
                deadSound.Play();

            onDeath.Invoke();
        }
    }

    private void Regen()
    {
        if (beenKilled || isScaling)
            return;

        if (currentHealth == maxHealth)
        {
            if (delayCount > 0)
                delayCount = 0f;

            if (regenCount > 0)
                regenCount = 0f;

            return;
        }

        if (delayCount < healthRegenDelay)
        {
            delayCount += Time.deltaTime;
            return;
        }

        if(regenCount < healthRegenTime)
        {
            regenCount += Time.deltaTime;
            return;
        }

        regenCount = 0f;
        currentHealth++;

        AdjustFaceSphere();

        if (currentHealth > maxHealth)
            currentHealth = maxHealth;
    }

    private void UpdateFaceSphere()
    {
        if (isScaling || faceSphere == null)
            return;

        if (faceSpherePercent == 0 && faceSphere.activeInHierarchy)
            faceSphere.SetActive(false);
        else if (faceSpherePercent > 0 && faceSphere.activeInHierarchy == false)
            faceSphere.SetActive(true);

        float spherePercent = Mathf.Lerp(faceSpherePercent, faceSpherePercent - hitDamagePercent, regenCount/ healthRegenTime);

        if(faceSphereRenderer.material.GetFloat(dissolveShaderHandle) != spherePercent)
            faceSphereRenderer.material.SetFloat(dissolveShaderHandle, spherePercent);
    }

    private void AdjustFaceSphere()
    {
        // maxHealth - 1 here so it is at 100% and the player has 1 health left
        faceSpherePercent = 1 - ((currentHealth - 1f) / (maxHealth - 1f));
    }

    private IEnumerator FaceSphereFade()
    {
        if (faceSphere == null)
            yield break;

        if (faceSphere.activeInHierarchy == false)
            faceSphere.SetActive(true);

        float t = 0f;
        isScaling = true;

        float startValue = faceSphereRenderer.material.GetFloat(dissolveShaderHandle);

        while (t <= dissolveInTime)
        {
            t += Time.deltaTime;

            faceSphereRenderer.material.SetFloat(dissolveShaderHandle,
                Mathf.LerpUnclamped(startValue, faceSpherePercent, dissolveInCurve.Evaluate(t / dissolveInTime)));

            yield return null;
        }

        faceSphereRenderer.material.SetFloat(dissolveShaderHandle, faceSpherePercent);

        yield return new WaitForSeconds(dissolveOutWait);

        isScaling = false;
    }
}
