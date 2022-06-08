using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
///The global game timer
/// <summary>
public class GameTime : MonoBehaviour
{
    //This is the variable that stores game time
    public static GameDateTime Now { get; set; }

    private bool GamePaused { get; set; }

    //The amount of second to update the GameTime once
    private float UpdateInterval { get; set; } = 1f;

    //for pausing the game
    private float TimePassedSinceLastUpdate { get; set; }

    //time to change game time after game started
    private float NextUpdateTime { get; set; } = 0;

    private float LastUpdateTime { get; set; } = 0;

    /// <summary>
    /// get the current game date time
    /// </summary>
    /// <returns>current game date time</returns>
    public static GameDateTime GetCurrentGameDateTime()
    {
        GameDateTime newGameDateTime = new GameDateTime(Now);
        return newGameDateTime;
    }

    //public GameDateTime gameDateTimeTest;


    public void Start()
    {
        if (NextUpdateTime == 0)
        {
            NextUpdateTime = UpdateInterval;
        }

        if (Now == null)
        {
            Now = new GameDateTime
            {
                //Game start time
                Year = 1,
                Month = 1,
                Date = 1,
                Hour = 0
            };
        }
        InvokeRepeating("GameTimeUpdate", 0, UpdateInterval);
    }
    public void Update()
    {

        
    }

    private void GameTimeUpdate()
    {

        if (!GamePaused)
        {

            if (Time.time > NextUpdateTime)
            {
                Now = GameDateTime.TimeLapseDay(10, Now);
                NextUpdateTime += UpdateInterval;
                LastUpdateTime = Time.time;

                //These lines are for debugging
                Debug.Log(Now.Year + "." + Now.Month + "." + Now.Date + "." + Now.Hour);
            }
        }
    }

    public void PauseGameTime()
    {
        //record this when update time

        TimePassedSinceLastUpdate = Time.time - LastUpdateTime;
        GamePaused = true;

    }
    
    public void ContinueGameTime()
    {
        NextUpdateTime = Time.time + UpdateInterval - TimePassedSinceLastUpdate;
        TimePassedSinceLastUpdate = 0;
        GamePaused = false;
    }

}

