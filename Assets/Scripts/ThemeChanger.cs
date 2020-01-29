using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ThemeChanger : MonoBehaviour
{
    private const float _THEME_ACTIVE_TIME = 3.0f;

    public static ThemeChanger Instance {get; private set;}

    [SerializeField] private PostProcessProfile[] _possibleProfiles;
    private PostProcessVolume _mainVolume;
    private PostProcessProfile _mainProfile;

    private float _timeOfLastChange;
    
    void Awake()
    {
        Instance = this;
        _mainVolume = Camera.main.GetComponent<PostProcessVolume>();
        _mainProfile = _mainVolume.profile;
    }

    // Update is called once per frame
    void Update()
    {
        if (_mainVolume.profile != _mainProfile && Time.time - _timeOfLastChange > _THEME_ACTIVE_TIME )
        {
            _mainVolume.profile = _mainProfile;
        }
    }

    public void ChangeTheme()
    {
        _timeOfLastChange = Time.time;
        PostProcessProfile newProfile = _possibleProfiles[Random.Range(0, _possibleProfiles.Length)];
        _mainVolume.profile = newProfile == null ? _mainVolume.profile : newProfile;
    }
}
