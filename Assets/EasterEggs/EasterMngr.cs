using System;
using UnityEngine;

public class EasterMngr : MonoBehaviour
{
    private const float _SUPER_RARE_SPAWN_CHANCE = 2.0f; // Percentage
    private const float _ANY_SPAWN_CHANCE = 86.66f; // Percentage

    public static EasterMngr Instance;

    [SerializeField] private GameObject[] _easterEggs = null;
    [SerializeField] private GameObject _superRareEgg = null;

    void Awake()
    {
        Instance = this;
    }

    public void SpawnRandomEgg()
    {
        float randomNum = UnityEngine.Random.Range(0.0f, 100.0f);

        if (randomNum <= _ANY_SPAWN_CHANCE)
        {
            bool spawnSuperRare = randomNum <= _SUPER_RARE_SPAWN_CHANCE;
            GameObject eggToSpawn = spawnSuperRare ?
            _superRareEgg :
            _easterEggs[UnityEngine.Random.Range(0, _easterEggs.Length)];

            Instantiate(eggToSpawn);
        }

        JaQueEstasAquiChamaLaOGarbageCollector();
    }

    // Ps: Whoever reads this - don't mind my brain farts, I'm a cool dude. I promise.
    private void JaQueEstasAquiChamaLaOGarbageCollector()
    {
        GC.Collect();
    }
}
