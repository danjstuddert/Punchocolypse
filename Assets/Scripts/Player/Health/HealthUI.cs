using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class HealthUI : MonoBehaviour {
    [SerializeField] private GameObject healthPipDisplay;
    [SerializeField] private Transform healthPipParent;

    private List<HealthPipUI> healthPips;

    public void Init(int healthPipNumber, int healthPerPip)
    {
        healthPips = new List<HealthPipUI>();

        for (int i = 0; i < healthPipNumber; i++)
        {
            GameObject pip = SimplePool.Spawn(healthPipDisplay, transform.position, healthPipParent.rotation);
            pip.transform.SetParent(healthPipParent);
            healthPips.Add(pip.GetComponent<HealthPipUI>());
            healthPips[i].Init(healthPerPip);
        }
    }

    public void HealthLoss(int pipNumber, int amountLost)
    {
        healthPips[pipNumber].HealthLoss(amountLost);
    }

    public void HealthGain(int pipNumber, int amountGain)
    {
        healthPips[pipNumber].HealthGain(amountGain);
    }

    public void HealthRegen(int pipNumber, float amountRegenerated)
    {
        healthPips[pipNumber].HealthRegen(amountRegenerated);
    }
}
