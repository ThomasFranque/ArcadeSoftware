using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ExternalSystemGames;
using Sounds;

public class GameButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private Color _fadedColor = default;
    [SerializeField] private Color _revealedColor = default;

    private GameInfo _selfGameInfo;
    private Animator _selectionArrowAnim;
    private Button _button;
    private RawImage _img;

    public Button Button => _button;

    // Start is called before the first frame update
    void Awake()
    {
        _selectionArrowAnim = GetComponentInChildren<Animator>();
        _img = GetComponent<RawImage>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnGameSelected);
    }

    public void SetGameInfo(GameInfo gameInfo)
    {
        _selfGameInfo = gameInfo;
    }

    public void OnSelect(BaseEventData eventData)
    {
        AudioMngr.Instance?.PlaySound(Sound.Selection_Sides);
        _selectionArrowAnim.SetTrigger("Idle");
        GameInfoDisplay.Instance.SetName(_selfGameInfo.Name);
        GameInfoDisplay.Instance.SetDescription(_selfGameInfo.Description);
    }

    public void FadeImage()
    {
        _img.color = _fadedColor;
    }

    public void RevealImage()
    {
        _img.color = _revealedColor;
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
