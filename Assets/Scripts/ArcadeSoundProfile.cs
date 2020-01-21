using UnityEngine;

namespace Sounds
{
    [CreateAssetMenu(menuName = "Arcade Sound Profile")]
    public class ArcadeSoundProfile : ScriptableObject
    {
        [SerializeField] private AudioClip _selectionUp = null;
        [SerializeField] private AudioClip _selectionDown = null;
        [SerializeField] private AudioClip _selectionSides = null;
        [SerializeField] private AudioClip _selected = null;
        [Range(0, 1)] [SerializeField] private float _volume = 1;

        public AudioClip SelectionUp => _selectionUp;
        public AudioClip SelectionDown => _selectionDown;
        public AudioClip SelectionSides => _selectionSides;
        public AudioClip Selected => _selected;
        public float Volume => _volume;
    }
}