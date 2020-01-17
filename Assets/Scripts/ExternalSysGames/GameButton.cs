using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace ExternalSystemGames
{
    public class GameButton : MonoBehaviour, ISelectHandler
    {
        private GameInfo _selfGameInfo;
        private Animator _selectionArrowAnim;
        private Button _button;

        // Start is called before the first frame update
        void Awake()
        {
            _selectionArrowAnim = GetComponentInChildren<Animator>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnGameSelected);
        }

        public void SetGameInfo(GameInfo gameInfo)
        {
            _selfGameInfo = gameInfo;
        }

        public void OnSelect(BaseEventData eventData)
        {
            _selectionArrowAnim.SetTrigger("Idle");
            GameInfoDisplay.Instance.SetName(_selfGameInfo.Name);
            GameInfoDisplay.Instance.SetDescription(_selfGameInfo.Description);
        }

        private void OnGameSelected()
        {
            // add lil animation before
            if (_selfGameInfo.ExeFile?.FullName != null)
                ProcessStarter.StartGame(_selfGameInfo.ExeFile.FullName);
        }
    }
}
