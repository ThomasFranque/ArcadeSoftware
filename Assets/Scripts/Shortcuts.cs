using UnityEngine;
using ExternalSystemGames;

//https://stackoverflow.com/questions/102567/how-to-shut-down-the-computer-from-c-sharp
namespace ArcadeFunctionality
{
    public class Shortcuts : MonoBehaviour
    {
        private const float _SHORTCUT_DELAY = 3.0f;


        [SerializeField] KeyCode[] _shutdownSequence = null;
        [SerializeField] KeyCode[] _restartSequence = null;

        float _timePressingShutdown = 0;
        float _timePressingRestart = 0;

        bool _shortcutTaken;

        // Update is called once per frame
        void Update()
        {
            if (!_shortcutTaken)
            {
                if (PressingAll(_shutdownSequence))
                    _timePressingShutdown += Time.deltaTime;
                else
                    _timePressingShutdown = 0;

                if (PressingAll(_restartSequence))
                    _timePressingRestart += Time.deltaTime;
                else
                    _timePressingRestart = 0;

                if (_timePressingShutdown >= _SHORTCUT_DELAY)
                {
                    OnShortcutTaken();
                    Shutdown();
                }

                else if (_timePressingRestart >= _SHORTCUT_DELAY)
                {
                    OnShortcutTaken();
                    Restart();
                }
            }

        }

        private bool PressingAll(KeyCode[] sequence)
        {
            foreach (KeyCode k in sequence)
                if (!Input.GetKey(k))
                    return false;
            return true;
        }

        private void OnShortcutTaken()
        {
            _shortcutTaken = true;
            Debug.LogWarning("Shortcut taken.");
        }

        private void Shutdown()
        {
			ProcessStarter.ShutDownMachine();
        }

        private void Restart()
        {
            ProcessStarter.RestartMachine();
        }
    }
}
