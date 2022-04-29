using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Singletons;
using UnityEngine;

public class Game 
{
    public static  InputService InputService;


    public Game()
    {
        InputService = RegisterInputService();
        GameManager.Instance.CurrentState = GameManager.GameState.StartGame;
    }

    public static InputService RegisterInputService()
    {
        if (Application.isEditor)
        {
            return new StandaloneInputService();
        }
        else
        {
            return new MobileInputService();
        }
        
    }
}