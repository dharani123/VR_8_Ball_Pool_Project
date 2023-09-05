using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision_Testing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();

        rb.AddForce(new Vector3(0f, 0f, 32f), ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
