using System.Diagnostics;

namespace ExternalSystemGames
{
    //https://stackoverflow.com/questions/240171/launching-an-application-exe-from-c
    //https://ss64.com/nt/shutdown.html
    public static class ProcessStarter
    {
        private const string _SHUTDOWN_ARGUMENTS = 
        "/s /t 6 /c \"Shut Down Shortcut was performed.\nShutting Down Machine...\"";
        private const string _RESTART_ARGUMENTS = 
        "/r /t 6 /c \"Restart Shortcut was performed.\nRestarting Machine...\"";
        
        private static Process _process;
        public static bool ProcessActive => !(_process == null || _process.HasExited);

        public static void StartGame(string gamePath)
        {
            // Prepare the process to run
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = gamePath,
                WindowStyle = ProcessWindowStyle.Maximized,
                CreateNoWindow = true
            };

            if (!ProcessActive)
            {
                ApplicationMngr.SpawnGameRunningScreen();
                _process = Process.Start(start);   
            }
        }
    
        public static void ShutDownMachine()
        {
            ProcessStartInfo shutdownInfo = new ProcessStartInfo
            {
                FileName = "shutdown",
                Arguments = _SHUTDOWN_ARGUMENTS,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process.Start(shutdownInfo);
        }
        
        public static void RestartMachine()
        {
            ProcessStartInfo restartInfo = new ProcessStartInfo
            {
                FileName = "shutdown",
                Arguments = _RESTART_ARGUMENTS,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process.Start(restartInfo);
        }
    }
}