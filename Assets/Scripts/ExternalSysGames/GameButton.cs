using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ExternalSystemGames;
using Sounds;

public class GameButton : MonoBehaviour, ISelectHandler
{
    private GameInfo _selfGameInfo;
    private Animator _selectionArrowAnim;
    private Button _button;

    public Button Button => _button;

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
        AudioMngr.Instance.PlaySound(Sound.Selection_Sides);
        _selectionArrowAnim.SetTrigger("Idle");
        GameInfoDisplay.Instance.SetName(_selfGameInfo.Name);
        GameInfoDisplay.Instance.SetDescription(_selfGameInfo.Description);
    }

    private void OnGameSelected()
    {
        //TODO: Add lil animation before
        if (_selfGameInfo.ExeFile?.FullName != null)
        {
            ProcessStarter.StartGame(_selfGameInfo.ExeFile.FullName);
            Debug.LogWarning(_selfGameInfo.Name + " Launched");
        }

    }
}
