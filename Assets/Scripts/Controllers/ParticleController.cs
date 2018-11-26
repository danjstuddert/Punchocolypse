using System.Collections.Generic;
using UnityEngine;

public class ParticleController : Singleton<ParticleController>
{
    [SerializeField] private Transform particleParent;

    private List<GameObject> currentParticles;

    private void Start()
    {
        if (particleParent == null)
            particleParent = transform;

        currentParticles = new List<GameObject>();
    }

    private void Update()
    {
        CheckParticles();
    }

    public GameObject SpawnParticle(GameObject prefab, Vector3 position, Quaternion rotation, bool turnsOff = false)
    {
        GameObject particle = SimplePool.Spawn(prefab, position, rotation);
        particle.transform.SetParent(particleParent);

        if (particle.GetComponent<ParticleSystem>())
        {
            particle.GetComponent<ParticleSystem>().Clear();
            particle.GetComponent<ParticleSystem>().Play();
        }
        foreach (Transform child in particle.transform)
        {
            if (child.GetComponent<ParticleSystem>())
            {
                child.GetComponent<ParticleSystem>().Clear();
                child.GetComponent<ParticleSystem>().Play();
            }
        }

        if(turnsOff)
            currentParticles.Add(particle);

        return particle;
    }

    private void CheckParticles()
    {
        if (currentParticles == null || currentParticles.Count <= 0)
            return;

        for (int i = 0; i < currentParticles.Count; i++)
        {
            if (currentParticles[i] == null)
                continue;

            ParticleSystem[] particles = GetComponentsInChildren<ParticleSystem>();

            bool particlesPlaying = false;
            for (int p = 0; p < particles.Length; p++)
            {
                if (particles[p].isPlaying)
                {
                    particlesPlaying = true;
                    break;
                }
            }

            if (particlesPlaying)
                continue;

            RemoveParticle(currentParticles[i]);
        }
    }

    private void RemoveParticle(GameObject particle)
    {
        currentParticles.Remove(particle);
        SimplePool.Despawn(particle);
    }
}
