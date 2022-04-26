using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using Assets.CodeBase.AdMob.Scripts;

namespace Assets.CodeBase.Infrastructure.Singletons
{
    public class GameManager : Singleton<GameManager>
    {

        public event Action ShowFinishMenu;
        public event Action ShowGameOverMenu;
        public event Action StartLevelTips;

        private Interstitial _interstitial;
        private int _deadCount = 0;
        public enum GameState { StartGame, Playing, Dead, Finish };
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
                else if (_currentState == GameState.StartGame)
                {
                    
                }

            }
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
            CurrentState = GameState.StartGame;

        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}