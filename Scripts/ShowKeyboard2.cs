using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using OVRSimpleJSON;

public class ShowKeyboard : MonoBehaviour
{
    private TMP_InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(x => OpenKeyboard());
    }

    // Update is called once per frame
    void OpenKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
    }

    void ShowHistory()
    {
        var username = NonNativeKeyboard.Instance.InputField.text;

        if (PlayerPrefs.HasKey("userHistory - " + username))
        {
            string userJSON = PlayerPrefs.GetString("userHistory - " + username);

            var user = JSON.Parse(userJSON);


        }
    }
}
