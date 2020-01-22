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

    private bool _timeToggle;
    private char _toggleChar = ':';

    private float _lastMinuteEasterEgg;

    private float Seconds => DateTime.Now.Second;
    private float Minutes => DateTime.Now.Minute;
    private float Hours => DateTime.Now.Hour;
    private float Day => DateTime.Now.Day;
    private float Month => DateTime.Now.Month;
    private float Year => DateTime.Now.Year;

    // Update is called once per frame
    void Update()
    {
        string newTime = 
            "<mspace=0.50em>" +
            $"{Hours.ToString().PadLeft(2,'0')}{_toggleChar}" +
            $"{Minutes.ToString().PadLeft(2,'0')}{_toggleChar}"+
            $"{Seconds.ToString().PadLeft(2,'0')}";

        // A second has passed
        if (newTime != _sysTimePro.text)
        {
            char oldChar = _toggleChar;

            if (_timeToggle)
                _toggleChar = ' ';
            else 
                _toggleChar = ':';

            newTime = newTime.Replace(oldChar, _toggleChar);
            _timeToggle = !_timeToggle;

            // 5 Minutes have passed
            if (Minutes % 5 == 0 && _lastMinuteEasterEgg != Minutes)
            {
                _lastMinuteEasterEgg = Minutes;
                EasterMngr.Instance.SpawnRandomEgg();
            }
        }

        _sysTimePro.SetText(newTime);
        _sysDatePro.text = 
            $"{Day.ToString().PadLeft(2,'0')}/" +
            $"{Month.ToString().PadLeft(2,'0')}/"+
            $"{Year.ToString().PadLeft(2,'0')}";
    }
}
