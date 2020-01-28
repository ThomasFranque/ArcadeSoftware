using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ExternalSystemGames;

public class ApplicationMngr : MonoBehaviour
{
    [SerializeField] private GameObject _screenPrefab;

    private static ApplicationMngr _instance;

    private GameObject _spawnedScreen;

    private bool isFocused = true;

    public static void SpawnGameRunningScreen()
    {
        _instance?.SpawnScreen();
    }

    private void Awake() 
    {
        _instance = this;
    }

    private void OnApplicationFocus(bool focusStatus) 
    {
        isFocused = focusStatus;

        if (_spawnedScreen != null && isFocused)
        {
            StartCoroutine(CDestroyScreen());
        }

    }

    private IEnumerator CDestroyScreen()
    {
        _spawnedScreen.GetComponent<Animator>().SetTrigger("Exit");
        yield return new WaitForSeconds(0.45f);
        Destroy(_spawnedScreen);
    }

    private void SpawnScreen()
    {
        _spawnedScreen = Instantiate(_screenPrefab);
    }
}
