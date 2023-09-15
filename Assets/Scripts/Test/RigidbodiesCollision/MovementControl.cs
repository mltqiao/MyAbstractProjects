using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementControl : MonoBehaviour
{
    public Rigidbody rig;
    private Vector3 _v3Speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _v3Speed = rig.velocity;
        MovementWithArrowKeys();
    }

    private void MovementWithArrowKeys()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            rig.velocity = new Vector3(_v3Speed.x, 0, 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            rig.velocity = new Vector3(_v3Speed.x, 0, -1);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rig.velocity = new Vector3(-1, 0, _v3Speed.z);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rig.velocity = new Vector3(1, 0, _v3Speed.z);
        }
    }
}
