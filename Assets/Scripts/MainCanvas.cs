using System.Collections.Generic;
using UnityEngine.EventSystems;
using ExternalSystemGames;
using UnityEngine.UI;
using UnityEngine;
using System;
using Sounds;

public class MainCanvas : MonoBehaviour
{
    //! Beware of constraint count of grid layout on the prefab
    private const byte _BUTTONS_PER_ROW = 5;
    private const float _DELAY_BETWEEN_LINE_CHANGE = 0.2f;

    private List<GamesRow> _rows;
    [Header("Prefabs")]
    [SerializeField] private GameObject _gameButtonPrefab = null;
    [SerializeField] private GameObject _gamesRowPrefab = null;
    [Header("References")]
    [SerializeField] private RowSurround _rowSurroundTop = null;
    [SerializeField] private RowSurround _rowSurroundBottom = null;
    private Transform RowsHolderTransform;
    private EventSystem _eventSystem;

    private GamesRow _currentRow;

    private float _timeOfLastRowChange;
    private int _buttonAmount;
    private int _rowIndex;

    private bool CanChangeLine => Time.time - _timeOfLastRowChange > _DELAY_BETWEEN_LINE_CHANGE;
    private bool CanRowDown => HasRowBelow && CanChangeLine;
    private bool CanRowUp => HasRowAbove && CanChangeLine;

    private bool HasRowAbove =>  _rowIndex > 0;
    private bool HasRowBelow => _rowIndex < _rows.Count - 1;

    void Awake()
    {
        RowsHolderTransform =
            Instantiate(new GameObject(), transform).transform;
        RowsHolderTransform.name = "Rows Holder";
        RowsHolderTransform.SetSiblingIndex(1);

        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        _rows = new List<GamesRow>();
        _buttonAmount = 0;
        _rowIndex = 0;

        RowsChange += UpdateTimeOfRowChange;
        RowsChange += UpdateRowSurroundVisibility;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) && CanRowDown)
        {
            AllRowsDown();
            OnRowChange();
        }
        else if (Input.GetKey(KeyCode.S) && CanRowUp)
        {
            AllRowsUp();
            OnRowChange();
        }
    }

    public void AddGameButton(GameInfo gInfo, Texture2D texture)
    {
        // Increment the amount of buttons
        _buttonAmount++;

        // Is a new row necessary?
        if (_buttonAmount % _BUTTONS_PER_ROW == 0 || _buttonAmount == 1)
            CreateNewRow();

        // Instantiation
        GameObject newButton = Instantiate(_gameButtonPrefab);
        // Assigning Texture
        newButton.GetComponent<RawImage>().texture = texture;
        // Getting script
        GameButton buttonScript = newButton.GetComponent<GameButton>();
        // Setting Game Info
        buttonScript.SetGameInfo(gInfo);
        // Adding to row
        _currentRow.AddToRow(buttonScript);
    }

    public void FinishedButtonLoad()
    {
        _eventSystem.SetSelectedGameObject(_currentRow.MiddleButton);
        UpdateRowSurroundVisibility();
    }

    private void CreateNewRow()
    {
        AllRowsUp(false);
        _rowIndex = 0;
        _currentRow = Instantiate(_gamesRowPrefab, RowsHolderTransform).GetComponent<GamesRow>();
        // Adding to collection
        _rows.Add(_currentRow);
    }

    private void AllRowsUp(bool playSound = true)
    {   
        if(playSound) PlayRowChangeSound(Sound.Selection_Down);
        _rowSurroundBottom.Blink();
        _rowIndex--;

        foreach(GamesRow r in _rows)
        {
            r.Up();
            if (r.Index == 0) SetNewSelectedButton(r.MiddleButton);
        }            
    }

    private void AllRowsDown(bool playSound = true)
    {
        if(playSound) PlayRowChangeSound(Sound.Selection_Up);
        _rowSurroundTop.Blink();
        _rowIndex++;

        foreach(GamesRow r in _rows)
        {
            r.Down();
            if (r.Index == 0) SetNewSelectedButton(r.MiddleButton);
        }
    }

    private void SetNewSelectedButton(GameObject b)
    {
        _eventSystem.SetSelectedGameObject(b);
    }

    private void UpdateRowSurroundVisibility()
    {
        _rowSurroundBottom.ToggleVisibility(HasRowAbove);
        _rowSurroundTop.ToggleVisibility(HasRowBelow);
    }

    private void UpdateTimeOfRowChange()
    {
        _timeOfLastRowChange = Time.time;
    }

    private void PlayRowChangeSound(Sound s)
    {
        AudioMngr.Instance.PlaySound(s);
    }

    private void OnRowChange()
    {
        RowsChange.Invoke();
    }

    // Pass rows up or down
    private Action RowsChange;
}
