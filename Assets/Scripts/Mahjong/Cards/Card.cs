using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Rigidbody rig;

    private void OnEnable()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hand") && Hand.MouseButtonDown)
        {
            Vector3 forceDirection = Hand.V3Speed/10f;
            float Distance = Vector3.Distance(other.transform.position, transform.position);
            rig.AddForce(forceDirection * 25/Distance, ForceMode.Impulse);
        }
    }
}
