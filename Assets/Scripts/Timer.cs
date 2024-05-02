using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    //Timer for both players
    private float _player1Timer = 60;
    private float _player2Timer = 60;

    //Timer text
    [SerializeField]
    private TextMeshProUGUI _player1TimerText;
    [SerializeField]
    private TextMeshProUGUI _player2TimerText;
    //References to players
    [SerializeField]
    private PlayerTagBehavior _player1;
    [SerializeField]
    private PlayerTagBehavior _player2;

    private void Update()
    {
        //If player 1 is tagged decrease player 1 timer
        if(_player1.IsTagged)
            _player1Timer -= Time.deltaTime;
        //Otherwise decrease player 2 timer
        else 
            _player2Timer -= Time.deltaTime;

        //Update Text
        _player1TimerText.text = ((int)_player1Timer).ToString();
        _player2TimerText.text = ((int)_player2Timer).ToString();

        //When one of the timers reaches zero,
        // display some text that says who won.
    }
}
