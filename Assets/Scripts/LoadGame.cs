using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGame : MonoBehaviour
{
    public GameObject panelAjustes;

    public void Start()
    {
        panelAjustes.SetActive(false);
    }


    public void Jugar()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void BorraRecord()
    {
        PlayerPrefs.DeleteKey("Best");
    }
}


