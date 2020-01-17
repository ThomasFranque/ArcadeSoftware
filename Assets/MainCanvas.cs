using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExternalSystemGames;

public class MainCanvas : MonoBehaviour
{
    private List<GameButton> _buttons;

    void Awake()
    {
        _buttons = new List<GameButton>(100);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddGameButton(GameButton button)
    {
        _buttons.Add(button);
    }
}
