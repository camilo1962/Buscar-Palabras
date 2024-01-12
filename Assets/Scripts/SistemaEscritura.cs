using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;


public class SistemaEscritura : MonoBehaviour
{
    public TMP_InputField palabraInputField;

    public void SaveToJason()
    {
        GuardaPalabra data = new GuardaPalabra();
        data.Palabra = palabraInputField.text;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(Application.dataPath + "/dicionary.json", json);
    }

}
