using System.Collections;
using UnityEngine;
using TMPro;

namespace ExternalSystemGames
{
    public class GameInfoDisplay : MonoBehaviour
    {
        private const float _DELAY_BETWEEN_CHARS = 0.02f;
        private const byte _MAX_NAME_LENGTH = 17;
        private const byte _MAX_DESCRIPTION_LENGTH = 90;

        public static GameInfoDisplay Instance { get; private set; }

        [SerializeField] private TextMeshProUGUI _nameTextPro = null;
        [SerializeField] private TextMeshProUGUI _descriptionTextPro = null;

        // Start is called before the first frame update
        void Awake()
        {
            Instance = this;
        }

        public void SetName(string name)
        {
            int finalLength =
                name.Length > _MAX_NAME_LENGTH ? _MAX_NAME_LENGTH : name.Length;

            _nameTextPro.text = name.Substring(0, finalLength);
        }

        public void SetDescription(string text)
        {
            // Clear
            ClearText();
            StopAllCoroutines();
            StartCoroutine(CSlowDisplay(text));
        }

        private void ClearText()
        {
            _descriptionTextPro.text = "";
        }

        private IEnumerator CSlowDisplay(string textToDisplay)
        {
            int charsWritten = 0;
            foreach (char c in textToDisplay)
            {
                if (charsWritten < _MAX_DESCRIPTION_LENGTH)
                {
                    _descriptionTextPro.text += c;
                    charsWritten ++;
                    yield return new WaitForSeconds(_DELAY_BETWEEN_CHARS);
                }
            }
        }
    }
}