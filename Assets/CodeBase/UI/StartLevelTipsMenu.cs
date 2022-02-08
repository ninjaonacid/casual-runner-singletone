using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelTipsMenu : MonoBehaviour
{
    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartLevelTips += GetMenu;
        }
         ;
    }
    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartLevelTips -= GetMenu;
        }
    }

    void GetMenu()
    {

        transform.GetChild(2).gameObject.SetActive(true);
    }
    



}
