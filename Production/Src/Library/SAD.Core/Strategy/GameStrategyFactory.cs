using SAD.Core.Strategy.Data;
using SAD.Core.Strategy.Subclasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAD.Core.Server;

namespace SAD.Core.Strategy
{
    class GameStrategyFactory
    {
        private static GameStrategyFactory gameStrategyInstance;
        private GameServer gameServer;

        private string gameOne;
        private string gameTwo;
        private string gameThree;
        private string gameFour;
        private string gameFive;

        private GameStrategyFactory()
        {
            gameServer = GameServer.GetInstance();

            GameOne = "one";
            GameTwo = "two";
            GameThree = "three";
            GameFour = "four";
            GameFive = "five";
        }

        public static GameStrategyFactory GetInstance()
        {
            if (gameStrategyInstance == null)
            {
                gameStrategyInstance = new GameStrategyFactory();
            }

            return gameStrategyInstance;
        }

        public GameStrategy CreateGameStrategy(string gameType)
        {
            GameStrategy strategy = null;

            if (string.Equals(gameType, gameOne, StringComparison.OrdinalIgnoreCase) == true)
                strategy = new GameOneStrategy();
            else if (string.Equals(gameType, gameTwo, StringComparison.OrdinalIgnoreCase) == true)
                strategy = new GameTwoStrategy();
            else if (string.Equals(gameType, gameThree, StringComparison.OrdinalIgnoreCase) == true)
                strategy = new GameThreeStrategy();
            else if (string.Equals(gameType, gameFour, StringComparison.OrdinalIgnoreCase) == true)
                strategy = new GameFourStrategy();
            else if (string.Equals(gameType, gameFive, StringComparison.OrdinalIgnoreCase) == true)
                strategy = new GameFiveStrategy();
            else
                strategy = new GameDefaultStrategy();

            return strategy;
        }

        public string GameOne
        {
            get { return gameOne; }
            set { gameOne = value; }
        }

        public string GameTwo
        {
            get { return gameTwo; }
            set { gameTwo = value; }
        }

        public string GameThree
        {
            get { return gameThree; }
            set { gameThree = value; }
        }
        public string GameFour
        {
            get { return gameFour; }
            set { gameFour = value; }
        }

        public string GameFive
        {
            get { return gameFive; }
            set { gameFive = value; }
        }
    }
}
