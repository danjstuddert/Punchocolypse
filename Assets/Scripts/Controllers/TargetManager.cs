using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetManager : Singleton<TargetManager>
{

    public List<Target> currentTargets;
    public List<Transform> targetSpawnPoints;

    public GameObject targetPrefab;
    public GameObject spawnParticle;

    public AudioClip deathSound;
    public AudioClip hitSound;
    

    public int maxCombo;
    public float comboPitchMaximum = 2;

    private Target targetToGetSoundFrom;
    private float comboPitch = 1f;

    /// <summary>
    /// Overall score the player has gained (maybe reset when targets respawn)
    /// </summary>
    public int score;

    public float comboTimeChance = 3.0f;

    private float timer;

    /// <summary>
    /// The amount of time before the targets will respawn (if any of them have been destroyed
    /// </summary>
    public float respawnDelay = 120f;

    private int currentCombo;

    private void Start()
    {
        SpawnTargets();

        timer = 0;

        //InvokeRepeating("ResetTargets", respawnDelay, respawnDelay);
    }

    public void HitTarget(Target target/*AudioSource targetAudioSource, Text scoreText, Animator anim, GameObject particle, float health*/)
    {
        targetToGetSoundFrom = target;

        AudioSource aSource = target.GetComponent<AudioSource>();

        //reset timer if timer is already happening
        if (timer > 0)
        {
            timer = 0;
        }

        currentCombo++;
        if (currentCombo > maxCombo)
            currentCombo = maxCombo;

        comboPitch += 0.2f;
        if (comboPitch > comboPitchMaximum)
            comboPitch = comboPitchMaximum;

        //gain 1 score and combo multiplier
        score += 1 * currentCombo;

        //update score text
        target.scoreText.text = score + "";

        //pitch change
        aSource.pitch = comboPitch;

        aSource.clip = hitSound;

        aSource.Play();

        //show particle effect
        Instantiate(target.targetHitParticle, target.transform);

        //show floating text with score amount
        target.scoreAnim.SetTrigger("play");

        target.health--;

        if (target.health <= 0)
        {
            aSource.clip = deathSound;

            aSource.Play();

            //Despawn target Gameobject (not just child) may need to change target heirarchy
            Destroy(target.transform.parent.gameObject, 0.5f);
        }


    }
    public void ResetTargets()
    {

        //reset combo and score and target health
        score = 0;
        currentCombo = 0;
        //t.health = defaultHealth;


        //check for any missing targets, if missing, respawn

        for (int i = 0; i < currentTargets.Count; i++)
        {
            if (!currentTargets[i].isActiveAndEnabled)
            {

                SimplePool.Spawn(targetPrefab, currentTargets[i].transform.parent.transform.position, Quaternion.identity);

                // set up init to reset health, audio, anim etc
                //t.Init ();

            }

        }
    }

    public void SpawnTargets()
    {

        for (int i = 0; i < targetSpawnPoints.Count; i++)
        {

            GameObject newTarget = Instantiate(targetPrefab, targetSpawnPoints[i].position, Quaternion.identity);
            newTarget.transform.parent = targetSpawnPoints[i].transform;

            currentTargets.Add(targetSpawnPoints[i].GetComponentInChildren<Target>());


            //play spawn particle
            //SimplePool.Spawn (spawnParticle,  targetSpawnPoints[i].transform.position, Quaternion.identity);
        }
    }

    private void Update()
    {
        UpdateCombo();
    }

    private void UpdateCombo()
    {
        if (currentCombo <= 0)
            return;

        timer += Time.deltaTime;

        if (timer >= comboTimeChance)
        {
            timer = 0f;
            currentCombo = 0;
            score = 0;
            comboPitch = 1;
            targetToGetSoundFrom.comboEndedSound.Play();

        }
    }
}
