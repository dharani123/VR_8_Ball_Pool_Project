using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectionScript : MonoBehaviour
{

    public enum Direction { Horizontal, Vertical};

    public Direction type;
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

        if (type == Direction.Horizontal)
        {
            ballRB.velocity = new Vector3(ballRB.velocity.x, 0, -ballRB.velocity.z);
        }

        if (type == Direction.Vertical)
        {
            ballRB.velocity = new Vector3(-ballRB.velocity.x, 0, ballRB.velocity.z);
        }
    }
}
