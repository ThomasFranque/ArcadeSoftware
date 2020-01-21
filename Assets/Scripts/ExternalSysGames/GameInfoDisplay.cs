using System.Collections;
using UnityEngine;
using TMPro;

namespace ExternalSystemGames
{
    public class GameInfoDisplay : MonoBehaviour
    {
        private const float _DELAY_BETWEEN_CHARS = 0.02f;

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
            _nameTextPro.text = name;
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
            foreach (char c in textToDisplay)
            {
                _descriptionTextPro.text += c;
                yield return new WaitForSeconds(_DELAY_BETWEEN_CHARS);
            }
        }
    }
}