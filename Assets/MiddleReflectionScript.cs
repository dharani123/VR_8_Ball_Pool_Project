using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleReflectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody ballRB = collision.gameObject.GetComponent<Rigidbody>();

        ballRB.velocity = Vector3.Reflect(ballRB.velocity, this.transform.right);
    }
}
