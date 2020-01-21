using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SystemCanvas : MonoBehaviour
{
    // 8,192: The odds, one against, of encountering a shiny Pokémon under normal circumstances in the first 5 generations.
    [SerializeField] private TextMeshProUGUI _sysTimePro;
    [SerializeField] private TextMeshProUGUI _sysDatePro;

    private float Seconds => DateTime.Now.Second;
    private float Minutes => DateTime.Now.Minute;
    private float Hours => DateTime.Now.Hour;
    private float Day => DateTime.Now.Day;
    private float Month => DateTime.Now.Month;
    private float Year => DateTime.Now.Year;

    // Update is called once per frame
    void Update()
    {
        _sysTimePro.text = 
            $"{Hours.ToString().PadLeft(2,'0')}:" +
            $"{Minutes.ToString().PadLeft(2,'0')}:"+
            $"{Seconds.ToString().PadLeft(2,'0')}";
        _sysDatePro.text = 
            $"{Day.ToString().PadLeft(2,'0')}/" +
            $"{Month.ToString().PadLeft(2,'0')}/"+
            $"{Year.ToString().PadLeft(2,'0')}";
    }
}
