using System;
using System.Collections;
using System.Collections.Generic;

public class GameDateTime
{
    public int Year { get; set; }
    public int Month { get; set; }
    public int Date { get; set; }
    public int Hour { get; set; }

    public GameDateTime()
    {

    }

    public GameDateTime(GameDateTime gameDateTime)
    {
        this.Year = gameDateTime.Year;
        this.Month = gameDateTime.Month;
        this.Date = gameDateTime.Date;
        this.Hour = gameDateTime.Hour;
    }

    /// <summary>
    /// Find out the maximum day of the current month
    /// </summary>
    /// <param name="Year">which year this month belongs to</param>
    /// <param name="Month"> Month that needs to be checked</param>
    /// <returns>The maximum day of this month</returns>
    public static int MaximumDayOfThisMonth(int Year, int Month)
    {

        switch (Month)
        {
            //Febrary
            case 2:
                return DateTime.IsLeapYear(Year) ? 29 : 28;

            //April June September October 
            case 4:
            case 6:
            case 9:
            case 11:
                return 30;

            //The rest months
            default:
                return 31;
        }
    }

    /// <summary>
    /// Get the new date time after this amount of year is passed
    /// </summary>
    /// <param name="yearPassed">amount of year passed</param>
    /// <param name="gameDateTime">initial date time</param>
    /// <returns>new date time</returns>

    public static GameDateTime TimeLapseYear(int yearPassed, GameDateTime gameDateTime)
    {
        GameDateTime newGameDateTime = new GameDateTime(gameDateTime);
        newGameDateTime.Year += yearPassed;
        return newGameDateTime;
    }

    /// <summary>
    /// Get the new date time after this amount of months is passed
    /// </summary>
    /// <param name="monthPassed">amount of months passed</param>
    /// <param name="gameDateTime">initial date time</param>
    /// <returns>new date time</returns>
    public static GameDateTime TimeLapseMonth(int monthPassed, GameDateTime gameDateTime)
    {
        GameDateTime newGameDateTime = new GameDateTime(gameDateTime);
        int sumMonth = newGameDateTime.Month + monthPassed;

        if (sumMonth <= 12)
        {
            newGameDateTime.Month = sumMonth;
            return newGameDateTime;
        }

        else
        {
            newGameDateTime.Month = sumMonth % 12;
            newGameDateTime = TimeLapseYear(sumMonth / 12, newGameDateTime);
            return newGameDateTime;
        }

    }

    /// <summary>
    /// Get the new date time after this amount of days is passed, not suitable if day passed is more than an entire month
    /// </summary>
    /// <param name="dayPassed">amount of days passed</param>
    /// <param name="gameDateTime">initial date time</param>
    /// <returns>new date time</returns>
    public static GameDateTime TimeLapseDay(int dayPassed, GameDateTime gameDateTime)
    {
        GameDateTime newGameDateTime = new GameDateTime(gameDateTime);
        int sumDays = newGameDateTime.Date + dayPassed;
        int maxDayThisMonth = MaximumDayOfThisMonth(newGameDateTime.Year, newGameDateTime.Month);

        if (sumDays <= maxDayThisMonth)
        {
            newGameDateTime.Date = sumDays;
            return newGameDateTime;
        }

        else
        {
            int remainingDays = sumDays - maxDayThisMonth;
            newGameDateTime = TimeLapseMonth(1, newGameDateTime);
            newGameDateTime.Date = 0;
            newGameDateTime = TimeLapseDay(remainingDays, newGameDateTime);
            return newGameDateTime;
        }

    }

    /// <summary>
    /// Get the new date time after this amount of hours is passed
    /// </summary>
    /// <param name="hourPassed">amount of hours passed</param>
    /// <param name="gameDateTime">initial date time</param>
    /// <returns>new date time</returns>
    public static GameDateTime TimeLapseHour(int hourPassed, GameDateTime gameDateTime)
    {
        GameDateTime newGameDateTime = new GameDateTime(gameDateTime);
        int sumHours = newGameDateTime.Hour + hourPassed;

        if (sumHours <= 23)
        {
            newGameDateTime.Hour = sumHours;
            return newGameDateTime;
        }

        else
        {
            newGameDateTime.Hour = sumHours % 24;
            newGameDateTime = TimeLapseDay(sumHours / 24, newGameDateTime);
            return newGameDateTime;
        }

    }

    /// <summary>
    /// This method is used when hours doesn't matter and user want to set gameDateTime's hour to zero
    /// </summary>
    /// <param name="gameDateTime">gameDateTime with hour that needs to be set to zero</param>
    /// <returns>gameDateTime with hour equals to zero</returns>
    public static GameDateTime ReturnZeroHour(GameDateTime gameDateTime)
    {
        GameDateTime newGameDateTime = new GameDateTime(gameDateTime)
        {
            Hour = 0
        };
        return newGameDateTime;
    }

    /// <summary>
    /// compare two gameDateTime and find out which one is earlier. 
    /// </summary>
    /// <param name="gameDateTime1">game date time one</param>
    /// <param name="gameDateTime2">game date time two</param>
    /// <returns>1 means the first one is earlier, -1 means the second one is earlier, 0 means they are exactly the same</returns>
    public static int Compare(GameDateTime gameDateTime1, GameDateTime gameDateTime2)
    {

        if (gameDateTime1.Year < gameDateTime2.Year)
        {
            return 1;
        }

        else if (gameDateTime1.Year > gameDateTime2.Year)
        {
            return -1;
        }

        else
        {

            if (gameDateTime1.Month < gameDateTime2.Month)
            {
                return 1;
            }

            else if (gameDateTime1.Month > gameDateTime2.Month)
            {
                return -1;
            }

            else
            {

                if (gameDateTime1.Date < gameDateTime2.Date)
                {
                    return 1;
                }

                else if (gameDateTime1.Date > gameDateTime2.Date)
                {
                    return -1;
                }

                else
                {

                    if (gameDateTime1.Hour < gameDateTime2.Hour)
                    {
                        return 1;
                    }

                    else if (gameDateTime1.Hour > gameDateTime2.Hour)
                    {
                        return -1;
                    }

                    else
                    {
                        return 0;
                    }

                }

            }

        }

    }

}
