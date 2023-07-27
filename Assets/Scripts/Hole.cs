using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public Transform SolidsPosition;
    public Transform StripesPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("solid") ||
            other.CompareTag("stripe") ||
            other.CompareTag("black"))
        {

            int ballNumber = int.Parse(other.gameObject.name.Substring(4));
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;


            if (ballNumber <= 7) {
                other.gameObject.transform.position = SolidsPosition.position + (-SolidsPosition.transform.up) * 0.1f * (ballNumber - 1);
            }

            if (ballNumber >= 9)
            {
                other.gameObject.transform.position = StripesPosition.position + (-StripesPosition.transform.up) * 0.1f * (ballNumber - 9);
            }

        }
    }
}
