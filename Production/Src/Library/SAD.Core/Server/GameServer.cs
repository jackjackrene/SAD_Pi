using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TargetServerCommunicator;
using TargetServerCommunicator.Servers;

namespace SAD.Core.Server
{
    /*
     *  This class adapts the abstract class GameServerInterface.
     *  It is a singleton
     */ 

    class GameServer
    {
        private static GameServer gameServerInstance = null;

        private IGameServer gameServerInterface;
        private GameServerType serverType;
        private string teamName;
        private string ipAddress;
        private int portNumber;

        private GameServer()
        {
        }

        public static GameServer GetInstance()
        {
            if (gameServerInstance == null)
                gameServerInstance = new GameServer();

            return gameServerInstance;
        }

        public void ConnectToGameServer(GameServerType serverType,
                                        string teamName,
                                        string ipAddress,
                                        int portNumber)
        {
            gameServerInterface = GameServerFactory.Create(serverType, teamName, ipAddress, portNumber);
        }

        public IEnumerable RetrieveServerGameList()
        {
            return gameServerInstance.RetrieveServerGameList();
        }

        public IEnumerable RetrieveServerTargetList(string gameType)
        {
            return gameServerInterface.RetrieveTargetList(gameType);
        }

        public void StopRunningGame()
        {
            gameServerInstance.StopRunningGame();
        }

        public GameServerType ServerType
        {
            get { return serverType; }
            set { serverType = value; }
        }

        public string TeamName
        {
            get { return teamName; }
            set { teamName = value; }
        }

        public string IpAddess
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        public int PortNumber
        {
            get { return portNumber; }
            set { portNumber = value; }
        }
    }
}
