using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Singletons;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static  IInputService InputService;


    public Game()
    {
        InputService = RegisterInputService();
        GameManager.Instance.CurrentState = GameManager.GameState.StartGame;
    }

    public static IInputService RegisterInputService()
    {
        if (Application.isEditor)
        {
            return new StandaloneInputService();
        }
        else
        {
            return new MobileInputService(new SwipeManager());
        }
        
    }
}