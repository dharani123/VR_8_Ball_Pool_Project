using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeReflectScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Vector3 normal;
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

        ballRB.velocity = Vector3.Reflect(ballRB.velocity, normal);
    }
}
