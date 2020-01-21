using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupEaster : EasterEgg
{
    [SerializeField] private GameObject _reversePrefab;

    private void Start()
    {
        AddActionOnDeath(SpawnReversePacman);
    }

    private void SpawnReversePacman()
    {
        Instantiate(_reversePrefab);
    }
}
