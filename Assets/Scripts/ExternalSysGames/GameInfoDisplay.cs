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
        private const byte _MAX_DESCRIPTION_CHARS_PER_LINE = 30;

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
            _descriptionTextPro.text = "<mspace=0.60em>";
        }

        private IEnumerator CSlowDisplay(string textToDisplay)
        {
            int charsWritten = 0;
            byte line = 1;

            string[] words = textToDisplay.Split(' ', '\n');

            foreach (string w in words)
            {
                if (charsWritten + w.Length > _MAX_DESCRIPTION_CHARS_PER_LINE * line)
                {
                    charsWritten = line * _MAX_DESCRIPTION_CHARS_PER_LINE;
                    _descriptionTextPro.text += '\n';
                    line++;
                }
                foreach (char c in w)
                {
                    if (charsWritten < _MAX_DESCRIPTION_LENGTH)
                    {
                        _descriptionTextPro.text += c;
                        charsWritten ++;
                        if (charsWritten % _MAX_DESCRIPTION_CHARS_PER_LINE == 0)
                            line++;
                        yield return new WaitForSeconds(_DELAY_BETWEEN_CHARS);
                    }
                }
                _descriptionTextPro.text += ' ';
                charsWritten++;
            }
        }
    }
}