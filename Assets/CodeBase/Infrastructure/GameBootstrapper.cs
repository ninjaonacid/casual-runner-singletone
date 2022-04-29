using Assets.CodeBase.Infrastructure.Services;
using Assets.CodeBase.Infrastructure.Singletons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        public GameBootstrapper()
        {

        }
        private void Awake()
        {

            _game = new Game();
            
        }

        
    }
}