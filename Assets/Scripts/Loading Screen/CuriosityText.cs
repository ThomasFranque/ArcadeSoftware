using UnityEngine;
using TMPro;

public class CuriosityText : MonoBehaviour
{
    private const float _CURIOSITY_TEXT_ONSCREEN_TIME = 3.1f;
    private float _timeOfLastCuriosity;
    private TextMeshProUGUI _curiosityTxt;

    private void Awake()
    {
        _curiosityTxt = GetComponent<TextMeshProUGUI>();
        ChangeCuriosity();
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time - _timeOfLastCuriosity > _CURIOSITY_TEXT_ONSCREEN_TIME)
            ChangeCuriosity();

    }

    private void ChangeCuriosity()
    {
        _timeOfLastCuriosity = Time.time;

        _curiosityTxt.SetText(RandomTexts.RandomLoadingScreenText);
    }
}
