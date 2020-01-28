using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingCanvas : MonoBehaviour
{
    private const float _FILES_LOAD_PERCENTAGE = 0.1f;
    private const float _IMGS_LOAD_PERCENTAGE = 0.75f;
    private const float _GAMES_SET_PERCENTAGE = 0.15f;
    private const float _BAR_FILL_SPEED = 1.5f;

    [SerializeField] private Image _backdropImg;
    [SerializeField] private Image _loadingBarImg;

    private float _barFilledAmmount;

    private int gamesToLoad;

    private int _filesLoaded = 0;
    private int _imgsLoaded = 0;
    private int _canvasGamesLoaded = 0;

    private Animator _animator;

    private void Awake()
    {
        _loadingBarImg.fillAmount = 0.0f;
        _animator = GetComponent<Animator>();
    }

    private float GetPercentage(int numToCheck)
    {

        return numToCheck == 0 ? 0.0001f : (float)numToCheck / (float)gamesToLoad;
    }

    private void Update()
    {
        if (_loadingBarImg.fillAmount < _barFilledAmmount)
        {

            float newFill = _loadingBarImg.fillAmount;

            newFill += Time.deltaTime * _BAR_FILL_SPEED;

            if (newFill > _barFilledAmmount) newFill = _barFilledAmmount;

            _loadingBarImg.fillAmount = newFill;
        }

    }

    private void UpdateBar()
    {
        _barFilledAmmount =
            (_FILES_LOAD_PERCENTAGE * GetPercentage(_filesLoaded)) +
            (_IMGS_LOAD_PERCENTAGE * GetPercentage(_imgsLoaded)) +
            (_GAMES_SET_PERCENTAGE * GetPercentage(_canvasGamesLoaded));

    }


    public void SetGamesToLoad(int amount)
    {
        gamesToLoad = amount;
        UpdateBar();
    }

    public void NewGameFilesLoaded()
    {
        _filesLoaded++;
        UpdateBar();
    }
    public void NewGameImageSet()
    {
        _imgsLoaded++;
        UpdateBar();
    }
    public void NewGameOnCanvas()
    {
        _canvasGamesLoaded++;
        UpdateBar();
    }
    public void FinishedButtonLoad() // 3.1
    {
        _animator.SetTrigger("Finished");
    }

    // Called on animation event
    public void DestroySelf()
    {
        Destroy(gameObject);
    }

}
