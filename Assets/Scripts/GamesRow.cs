using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamesRow : MonoBehaviour
{
    private const float _DISTANCE_BETWEEN_ROWS = 2.0f;
    private const float _SIZE_FACTOR = 0.9f;

    [SerializeField] private Transform _gamesRowContentTransform = null;
    //// private Animator _anim;

    public int Index { get; private set; }
    private List<GameButton> _buttons;

    public GameObject MiddleButton => _buttons[(_buttons.Count -1) / 2].gameObject;

    private void Awake()
    {
        Index = 0;
        _buttons = new List<GameButton>();
        //// _anim = GetComponentInChildren<Animator>();
    }

    public void AddToRow(GameButton button)
    {
        button.transform.SetParent(_gamesRowContentTransform, false);
        _buttons.Add(button);
    }

    public void Down()
    {
        Index--;
        Vector3 newPos = transform.position;
        newPos.y -= _DISTANCE_BETWEEN_ROWS;
        transform.position = newPos;
        
        UpdateInteractableState();

        // // switch (_index)
        // // {
        // //     case -1:
        // //         _anim.SetTrigger("GoDown");
        // //         break;
        // //     case 0:
        // //         gameObject.SetActive(true);
        // //         _anim.SetTrigger("GoToMiddleFromUp");
        // //         break;
        // //     case 1:
        // //         break;
        // //     default:
        // //         gameObject.SetActive(false);
        // //         break;
        // // }
    }

    public void Up()
    {
        Index++;
        Vector3 newPos = transform.position;
        newPos.y += _DISTANCE_BETWEEN_ROWS;
        transform.position = newPos;

        UpdateInteractableState();

        // // switch (_index)
        // // {
        // //     case 1:
        // //         _anim.SetTrigger("GoUp");
        // //         break;
        // //     case 0:
        // //         gameObject.SetActive(true);
        // //         _anim.SetTrigger("GoToMiddleFromDown");
        // //         break;
        // //     case -1:
        // //         break;
        // //     default:
        // //         gameObject.SetActive(false);
        // //         break;
        // // }
    }

    private void UpdateInteractableState()
    {
        if (Index == 0)
        {
            transform.localScale = new Vector3(1.0f,1.0f,1.0f);
            ToggleButtonInteraction(true);
            foreach (GameButton b in _buttons)
                b.RevealImage();
        }
        else
        {
            transform.localScale = new Vector3(0.6f,0.6f,1f);
            ToggleButtonInteraction(false); 
            foreach (GameButton b in _buttons)
                b.FadeImage();
        }
        

    }

    public void ToggleButtonInteraction(bool selectable)
    {
        foreach(GameButton gb in _buttons)
            gb.Button.interactable = selectable;
    }
}
