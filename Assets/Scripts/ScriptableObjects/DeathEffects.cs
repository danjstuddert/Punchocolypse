using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "DeathEffects", fileName = "New Damage Info")]
public class DeathEffects : ScriptableObject {

	public List<GameObject> deathObjects;

    public LayerMask forceLayer;

    public float deathObjectForceMultiplier;

	public GameObject deathParticle;

	public List<AudioClip> deathSounds;

    public void PlayDeathEffects()
    {
        //Play death sound
    }



}
