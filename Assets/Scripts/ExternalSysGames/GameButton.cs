using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using ExternalSystemGames;
using Sounds;

public class GameButton : MonoBehaviour, ISelectHandler
{
    private const float _BLINK_TIME = 1.0f;
    private const float _BLINK_DELAY = .1f;

    [SerializeField] private Color _fadedColor = default;
    [SerializeField] private Color _revealedColor = default;

    private GameInfo _selfGameInfo;
    private Animator _selectionArrowAnim;
    private Button _button;
    private RawImage _img;

    private float _timeOfBlinkStart;

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
        //TODO: Add lil image blink before
        _selectionArrowAnim.SetTrigger("Click");
        AudioMngr.Instance.PlaySound(Sound.Selected);

        Blink();

        if (_selfGameInfo.ExeFile?.FullName != null)
        {
            ProcessStarter.StartGame(_selfGameInfo.ExeFile.FullName);
            Debug.LogWarning(_selfGameInfo.Name + " Launched");
        }
    }

    private void Blink()
    {
        _timeOfBlinkStart = Time.time;
        StopAllCoroutines();
        StartCoroutine(CBlink());
    }

    private IEnumerator CBlink()
    {
        _img.enabled = !_img.enabled;
        yield return new WaitForSeconds(_BLINK_DELAY);
        if (Time.time - _timeOfBlinkStart <_BLINK_TIME)
            StartCoroutine(CBlink());
        else
            _img.enabled = true;
    }
}
