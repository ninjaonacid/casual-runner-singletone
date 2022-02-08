using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class GameOverMenu : MonoBehaviour
{


    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowGameOverMenu += GetMenu;
        }
        ;
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowGameOverMenu -= GetMenu;
        }
    }

   

     void GetMenu()
    {

        transform.GetChild(0).gameObject.SetActive(true);

    }
    public void RestartButton()
    {
        
        GameManager.Instance.RestartLevel();

    }

    public void SkipLevelButton()
    {
        GameManager.Instance.NextLevel();

    }

    public void Quitbutton()
    {
        GameManager.Instance.QuitGame();
    }
}
