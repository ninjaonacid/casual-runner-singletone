using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : Singleton<ScoreManager>
{


    private Text txt;
    private int _score;
    public int Score
    {
        get { return _score; }
        set { _score = value;  }
    }

    private void OnEnable()
    {
        txt = gameObject.GetComponentInChildren<Text>();
       
    }

   public void AddToScore()
    {
        _score += 1;
        PrintScore();
    }

   public void PrintScore()
    {
        txt.text = _score.ToString();
    }
}

    

