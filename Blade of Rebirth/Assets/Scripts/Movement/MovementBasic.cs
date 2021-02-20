using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBasic : MonoBehaviour
{
    float accel;


    // Start is called before the first frame update
    void Start()
    {
        accel = 5.0f;
        //gameObject.GetComponent<Rigidbody>().velocity = direction * dashSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.GetComponent<Rigidbody>().velocity += gameObject.GetComponent<PlayerInput>();
    }
}
