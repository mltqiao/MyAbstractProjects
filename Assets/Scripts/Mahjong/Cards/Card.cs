using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Rigidbody rig;
    private bool isBeingBuild;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rig.AddForce(Vector3.down * 200f);
        if (Vector3.Distance(transform.position, transform.parent.position) >= 200f)
        {
            rig.velocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 洗牌
        if (other.gameObject.layer == LayerMask.NameToLayer("Hand") && Hand.MouseMixingButtonDown)
        {
            Vector3 forceDirection = Hand.V3Speed.normalized;
            float Distance = Vector3.Distance(other.transform.position, transform.position);
            rig.AddForce(forceDirection * 5000f/Distance, ForceMode.Impulse);
        }
        // 码牌
        else if (other.gameObject.layer == LayerMask.NameToLayer("Hand") && Hand.MouseBuildingButtonDown)
        {
            Vector3 forceDirection = Hand.V3Speed.normalized;
            rig.AddForce(forceDirection * 125f, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Walls") && Hand.MouseBuildingButtonDown)
        {
            Vector3 buildDirection = Hand.V3Speed.normalized;
            // bottom build
            if (buildDirection.z < 0 && Math.Abs(buildDirection.z) > Math.Abs(buildDirection.x))
            {
                Debug.Log("Bottom Build");
            }
            // Left build
            else if (buildDirection.x < 0 && Math.Abs(buildDirection.x) > Math.Abs(buildDirection.z))
            {
                Debug.Log("Left Build");
            }
            // Top build
            else if (buildDirection.z > 0 && Math.Abs(buildDirection.z) > Math.Abs(buildDirection.x))
            {
                Debug.Log("Top Build");
            }
            // Right build
            else if (buildDirection.x > 0 && Math.Abs(buildDirection.x) > Math.Abs(buildDirection.z))
            {
                Debug.Log("Right Build");
            }
        }
    }
}
