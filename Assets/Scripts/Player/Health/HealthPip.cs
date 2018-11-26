using UnityEngine;

[System.Serializable]
public class HealthPip {
    public int healthAmount;
    public float regenTime;

    public bool Full { get { return healthAmount == maxHealth; } }
    public float RegenPercent { get { return regenCount / regenTime; } }

    private int maxHealth;
    private float regenCount;

    public void Init()
    {
        maxHealth = healthAmount;
    }

    public void OnUpdate()
    {
        if (healthAmount >= maxHealth || healthAmount == 0)
            return;

        if(regenCount < regenTime)
        {
            regenCount += Time.deltaTime;
            return;
        }

        regenCount = 0f;

        healthAmount++;
    }

    public void TakeDamage(int amount)
    {
        if (healthAmount == 0)
            return;

        healthAmount -= amount;
        regenCount = 0f;

        if (healthAmount < 0)
            healthAmount = 0;
    }

    public void HealDamage(int amount)
    {
        if (healthAmount >= maxHealth)
            return;

        healthAmount += amount;
        regenCount = 0f;

        if (healthAmount > maxHealth)
            healthAmount = maxHealth;
    }
}
