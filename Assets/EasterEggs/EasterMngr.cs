using UnityEngine;

public class EasterMngr : MonoBehaviour
{
    public static EasterMngr Instance;
    
    [SerializeField] private GameObject[] _easterEggs = null;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnRandomEgg()
    {
        Instantiate(_easterEggs[Random.Range(0, _easterEggs.Length)]);
    }
}
