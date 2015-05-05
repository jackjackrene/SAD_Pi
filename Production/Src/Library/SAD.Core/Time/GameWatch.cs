using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAD.Core.Time
{
    class GameWatch
    {
        private static GameWatch gameWatchInstance;
        private Stopwatch stopWatch;

        private GameWatch()
        {
            stopWatch = new Stopwatch();
        }

        public static GameWatch GetInstance()
        {
            if (gameWatchInstance == null)
                gameWatchInstance = new GameWatch();

            return gameWatchInstance;
        }

        public void StartGameWatch()
        {
            stopWatch.Start();
        }

        public TimeSpan GetCurrentTime()
        {
            return stopWatch.Elapsed;
        }

        public void RestartGameWatch()
        {
            stopWatch.Restart();
        }

        public void ResetGameWatch()
        {
            stopWatch.Reset();
        }

        public void StopGameWatch()
        {
            stopWatch.Stop();
        }
    }
}
