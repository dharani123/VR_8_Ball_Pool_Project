using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSomeSeconds : MonoBehaviour
{
    // Destory the text after 10 seconds.

    private float timer = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer = timer - Time.deltaTime;

        if (timer < 0) {
            Destroy(this.gameObject);
        }
    }
}
