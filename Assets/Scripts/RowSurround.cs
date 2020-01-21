using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RowSurround : MonoBehaviour
{
    private const float _BLINK_DURATION = 0.1f;

    [SerializeField] private Color _normalColor = Color.white;
    [SerializeField] private Color _blinkColor = Color.white;

    private Image _img;
    private void Awake()
    {
        _img = GetComponent<Image>();
        _img.color = _normalColor;
    }

    public void Blink()
    {
        StartCoroutine(CBlink());
    }

    private IEnumerator CBlink()
    {
        _img.color = _blinkColor;
        yield return new WaitForSeconds(_BLINK_DURATION);
        _img.color = _normalColor;
    }

    public void ToggleVisibility(bool active)
    {
        _img.enabled = active;
    }
}
