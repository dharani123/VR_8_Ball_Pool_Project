using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cue_Collision : MonoBehaviour
{

    private Vector3 prevPosition;
    private Vector3 currPosition;
    private Vector3 prevVelocity;
    private Vector3 currVelocity;
    private Vector3 accelaration;

    // Start is called before the first frame update
    void Start()
    {
        prevPosition = this.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currPosition = this.transform.position;
        
        currVelocity = (currPosition - prevPosition) / Time.deltaTime;
        accelaration = (currVelocity - prevVelocity) / Time.deltaTime;


        prevPosition = currPosition;
        prevVelocity = currVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("cue_ball"))
        {

            Rigidbody cueRB = other.gameObject.GetComponent<Rigidbody>();
            Vector3 accelarationAlongPlane = new Vector3(accelaration.x, 0, accelaration.z);

            float magnitude = accelarationAlongPlane.magnitude;
            Vector3 direction = -this.transform.forward;


            cueRB.AddForce((new Vector3(direction.x, 0, direction.z)) * magnitude / 5, ForceMode.Impulse);
        }
    }
}
