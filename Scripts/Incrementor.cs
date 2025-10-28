using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.AssemblyQualifiedNameParser;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class Incrementor : MonoBehaviour
{
    public TextMeshProUGUI number;

    private void Start()
    {
        PlayerPrefs.SetInt("nrOfEx", 20);
    }

    public void Increment()
    {
        var num = Int32.Parse(number.text); 
        num++;
        number.text = num.ToString();

        PlayerPrefs.SetInt("nrOfEx", num);
    }

    public void Decrement()
    {
        var num = Int32.Parse(number.text);
        if(num > 0)
            num--;
        number.text = num.ToString();

        PlayerPrefs.SetInt("nrOfEx", num);
    }
}
