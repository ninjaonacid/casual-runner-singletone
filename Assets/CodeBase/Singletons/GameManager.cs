using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{

    public event Action ShowFinishMenu;
    public event Action ShowGameOverMenu;
    public event Action StartLevelTips;

    private Interstitial _interstitial;
    private int _deadCount = 0;
    public enum GameState { StartLevel, Playing, Dead, Finish };
    private GameState _currentState;
    public GameState CurrentState
    {
        get
        {
            return _currentState;
        }

        set
        {
            _currentState = value;
            if (_currentState == GameState.Dead)
            {
                GameOver();
                if (_deadCount % 2 == 0)
                {
                    _interstitial = GetComponent<Interstitial>();
                    _interstitial.ShowInterstitial();
                }
            }
            else if (_currentState == GameState.Finish)
            {
                LevelPassed();
            }
            else if (_currentState == GameState.StartLevel)
            {
                ScoreManager.Instance.Score = 0;
                ScoreManager.Instance.PrintScore();
            } 
            
        }
    }

    
    void Update()
    {
         
        
    }
    
    void StartLevel()
    {
        StartLevelTips?.Invoke();
    }
    void GameOver()
    {
       ShowGameOverMenu?.Invoke();
        _deadCount++;
    }

    void LevelPassed()
    {
        ShowFinishMenu?.Invoke();
    }
   

    
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
