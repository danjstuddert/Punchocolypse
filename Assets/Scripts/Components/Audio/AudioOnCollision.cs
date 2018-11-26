using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioOnCollision : MonoBehaviour {
	 private float minVelocity = 3f;

    private Collider myCollider;

    private void Start()
    {
        myCollider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.relativeVelocity.sqrMagnitude > minVelocity * minVelocity)
        {
            ImpactController.Instance.PlayImpact(myCollider.material, collision.contacts[0].point, 
                collision.relativeVelocity);
        }
    }
}
