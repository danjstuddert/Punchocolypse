using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ImpactInfo
{
    public PhysicMaterial impactmaterial;
    public List<AudioClip> impactSounds;
    public GameObject impactParticle;
}

public class ImpactController : Singleton<ImpactController>
{
    [SerializeField] private List<ImpactInfo> impacts;

    private bool recentlyImpacted;

    /// <summary>
    /// Pass shared mat from arrow
    /// </summary>
    /// <param name="physMat"></param>
    /// <param name="hitPoint"></param>
    /// <param name="velocity"></param>
    /// 

    public void PlayImpact(PhysicMaterial physMat, Vector3 hitPoint, Vector3 velocity)
    {
        if (recentlyImpacted || physMat == null)
            return;

        recentlyImpacted = true;
        this.Invoke(ResetImpacted, 0.25f);

        for (int i = 0; i < impacts.Count; i++)
        {
            if (physMat.name.Contains(impacts[i].impactmaterial.name))
            {
                //Spawn particle
                ParticleController.Instance.SpawnParticle(impacts[i].impactParticle, hitPoint, Quaternion.Euler(-velocity));

                //Play random sound from impactSounds
                AudioSource.PlayClipAtPoint(impacts[i].impactSounds[Random.Range(0, impacts[i].impactSounds.Count)], hitPoint);
            }
        }
    }
		

    private void ResetImpacted()
    {
        recentlyImpacted = false;
    }
}
