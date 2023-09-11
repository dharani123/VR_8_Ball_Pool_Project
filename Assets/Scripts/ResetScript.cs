using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetScript : MonoBehaviour
{
    // This class is used to reset an GameObject to its initial position and rotation.
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    void Start()
    {
        initialPosition = this.transform.position;
        initialRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        this.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        this.transform.position = initialPosition;
        this.transform.rotation = initialRotation;
    }
}
