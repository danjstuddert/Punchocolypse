using UnityEngine;
using UnityEngine.UI;
using Valve.VR.InteractionSystem;

public class Target : MonoBehaviour {

    public int health = 3;
	public GameObject targetHitParticle;
	public Text scoreText;
	public Animator scoreAnim;
    public PlaySound comboEndedSound;

    public LayerMask arrowMask;

    private void OnCollisionEnter(Collision other)
    {
        if (arrowMask == (arrowMask | (1 << other.gameObject.layer)))
        {
            //play particle effect
           Instantiate(targetHitParticle, transform.position, Quaternion.identity);

            //pass source for pitch
            TargetManager.Instance.HitTarget(this);
        }
    }
}
