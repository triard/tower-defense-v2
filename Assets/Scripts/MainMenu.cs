﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject mainMenu;
    [SerializeField]
    private GameObject optionMenu;
    // Start is called before the first frame update

    public  void Options()
    {
        if (optionMenu.activeSelf)
        {
            mainMenu.SetActive(true);
            optionMenu.SetActive(false);
        }
        else
        {
            mainMenu.SetActive(false);
            optionMenu.SetActive(true);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Play()
    {
        SceneManager.LoadScene(2);
    }


}
