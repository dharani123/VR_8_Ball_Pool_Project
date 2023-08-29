using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterSomeSeconds : MonoBehaviour
{
    // Start is called before the first frame update

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
