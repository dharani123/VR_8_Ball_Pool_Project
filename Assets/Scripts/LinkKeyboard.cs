using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
public class LinkKeyboard : MonoBehaviour
{

    private TMP_InputField inputText;
    // Start is called before the first frame update
    void Start()
    {
        inputText = GetComponent<TMP_InputField>();
        inputText.onSelect.AddListener(x => ShowKeyboard());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputText;
        NonNativeKeyboard.Instance.PresentKeyboard(inputText.text);
    }
}
