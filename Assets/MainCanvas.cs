using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ExternalSystemGames;
using UnityEngine.UI;

public class MainCanvas : MonoBehaviour
{
    //! Beware of constraint count of grid layout on the prefab
    private const byte _BUTTONS_PER_ROW = 6;

    private List<GamesRow> _rows;
    [SerializeField] private GameObject _gameButtonPrefab = null;
    [SerializeField] private GameObject _gamesRowPrefab = null;
    private EventSystem _eventSystem;

    private GamesRow _currentRow;

    private int _buttonAmount;
    private int _rowIndex;
    void Awake()
    {
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        _rows = new List<GamesRow>();
        _buttonAmount = 0;
        _rowIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W) && _rowIndex < _rows.Count - 1)
            AllRowsDown();
        else if (Input.GetKeyDown(KeyCode.S) && _rowIndex > 0)
            AllRowsUp();
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
        _eventSystem.SetSelectedGameObject(_currentRow.MiddleButton);

        //Debug.Log((_buttonAmount-1) % _BUTTONS_PER_ROW);

    }

    private void CreateNewRow()
    {
        AllRowsUp();
        _rowIndex = 0;
        _currentRow = Instantiate(_gamesRowPrefab, transform).GetComponent<GamesRow>();
        // Adding to collection
        _rows.Add(_currentRow);
    }

    private void AllRowsUp()
    {   
        _rowIndex--;

        foreach(GamesRow r in _rows)
        {
            r.Up();
            if (r.Index == 0) SetNewSelectedButton(r.MiddleButton);
        }
            
    }

    private void AllRowsDown()
    {
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
}
