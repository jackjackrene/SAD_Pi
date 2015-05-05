using SAD.Core.Strategy.Data;
using SAD.Core.Strategy.Subclasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Strategy
{
    class GameStrategyFactory
    {
        private static GameStrategyFactory gameStrategyInstance;

        private GameStrategyFactory()
        {
        }

        public static GameStrategyFactory GetInstance()
        {
            if (gameStrategyInstance == null)
            {
                gameStrategyInstance = new GameStrategyFactory();
            }

            return gameStrategyInstance;
        }

        public GameStrategy CreateGameStrategy(GameStrategyType strategyType)
        {
            GameStrategy strategy = null;

            if (strategyType == GameStrategyType.PrioritizeClosestTargets)
                strategy = new PriotizeDistanceStrategy();
            else if (strategyType == GameStrategyType.PrioritizeHighestPoints)
                strategy = new PrioritizePointsStrategy();
            else if (strategyType == GameStrategyType.PrioritizeFoes)
                strategy = new PriotitizeFoesStrategy();
            else if (strategyType == GameStrategyType.PrioritizeFriends)
                strategy = new PriotitizeFoesStrategy();
            else
                strategy = new PriotitizeFoesStrategy();

            return strategy;
        }
    }
}
