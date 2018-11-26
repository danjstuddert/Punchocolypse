using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPipUI : MonoBehaviour {
    [SerializeField] private GameObject healthPip;

    private List<GameObject> healthPips;

    public void Init(int healthPerPip)
    {
        healthPips = new List<GameObject>();

        for (int i = 0; i < healthPerPip; i++)
        {
            GameObject go = SimplePool.Spawn(healthPip, transform.position, transform.rotation);
            go.transform.SetParent(transform);
            healthPips.Add(go);
        }
    }

    public void HealthLoss(int amountLost)
    {
        for (int i = healthPips.Count - 1; i >= 0; i--)
        {
            if (healthPips[i].activeInHierarchy == false)
                continue;

            if (amountLost <= 0)
                break;
        }
    }

    public void HealthGain(int amountGain)
    {

    }

    public void HealthRegen(float amountRegenerated)
    {

    }
}
