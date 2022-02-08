using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinishMenu : MonoBehaviour
{

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowFinishMenu += GetMenu;
        }
        
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ShowFinishMenu -= GetMenu;
        }
    }

    void GetMenu()
    {
        
            transform.GetChild(1).gameObject.SetActive(true);
    }
    public void NextLevelButton()
    {
        GameManager.Instance.NextLevel();
    }

    
}
