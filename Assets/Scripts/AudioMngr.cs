using UnityEngine;
using Sounds;

namespace Sounds
{
    public class AudioMngr : MonoBehaviour
    {
        public static AudioMngr Instance;

        [Range(0, 1)] [SerializeField] private float _masterVolume = 1.0f;
        [SerializeField] private ArcadeSoundProfile _soundProfile;
        private AudioSource _source;

        private float PlayVolume => _masterVolume * _soundProfile.Volume;
        public ArcadeSoundProfile SoundProfile
        {
            get => _soundProfile;
            set { _soundProfile = value; }
        }
        private void Awake()
        {
            if (Instance != null)
                Debug.LogError("There are more than one Audio Manager.\n" +
                $"This Obj: {name}\tInstance Obj: {Instance.name}");
            Instance = this;

            _source = GetComponent<AudioSource>();
        }
        
        public void PlaySound(Sound s)
        {
            if(!_source.isPlaying)
                switch (s)
                {
                    case Sound.Selection_Up:
                        PlaySound(SoundProfile.SelectionUp);
                        break;
                    case Sound.Selection_Down:
                        PlaySound(SoundProfile.SelectionDown);
                        break;
                    case Sound.Selection_Sides:
                        PlaySound(SoundProfile.SelectionSides);
                        break;
                    case Sound.Selected:
                        PlaySound(SoundProfile.Selected);
                        break;
                }
        }

        private void PlaySound(AudioClip sound)
        {
            _source.clip = sound;
            _source.volume = PlayVolume;
            _source.Play();
        }
    }
}