using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class AnimateHandOnInput : MonoBehaviour
{

    public InputActionProperty pinchAction;
    public InputActionProperty gripAction;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Trigger", pinchAction.action.ReadValue<float>());
        animator.SetFloat("Grip", gripAction.action.ReadValue<float>());
    }
}
