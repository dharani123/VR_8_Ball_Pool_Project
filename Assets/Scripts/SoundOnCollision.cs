using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnCollision : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip ballHit;
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
        Collider col = collision.collider;
        if (col.CompareTag("stripe")
            || col.CompareTag("solid")
            || col.CompareTag("black")) {
            audioSource.PlayOneShot(ballHit);

        }
    }
}
