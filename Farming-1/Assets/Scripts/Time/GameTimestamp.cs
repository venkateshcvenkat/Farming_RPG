using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameTimestamp
{
    public int year;
    public int day;
    public int hour;
    public int minute;
    public Season season;
    public enum Season
    {
        Spring,
        Summer,
        Fall,
        Winter
    }
    public enum DayofTheWeek
    {
        Saturday,
        Sunday,
        Monday,
        Tuesday,
        Wednesday,
        Thursday,
        Friday

    }
    public GameTimestamp(int year, int day, int hour, int minute, Season season)
    {
        this.year = year;
        this.day = day;
        this.hour = hour;
        this.minute = minute;
        this.season = season;
    }
    public GameTimestamp(GameTimestamp timestamp)
    {
        this.year = timestamp.year;
        this.day = timestamp.day;
        this.hour = timestamp.hour;
        this.minute = timestamp.minute;
        this.season = timestamp.season;
    }
    public void UpdateClock()
    {
        minute++;
        if (minute >= 60)
        {
            minute = 0;
            hour++;
        }

        if (hour >= 24)
        {
            hour = 0;
            day++;
        }

        if (day > 30)
        {
            day = 1;
            if (season == Season.Winter)
            {
                season = Season.Spring;
                year++;
            }
            else
            {
                season++;
            }
        }
    }

    public DayofTheWeek GetDayofTheWeek()
    {
        int daysPassed = YearsToDays(year) + SeasonsToDays(season) + day;
        int dayIndex = daysPassed % 7;
        return (DayofTheWeek)dayIndex;
    }
    public static int HoursToMinutes(int hour)
    {
        return hour * 60;
    }
    public static int DaysToHours(int days)
    {
        return days * 24;
    }
    public static int SeasonsToDays(Season season)
    {
        int seasonIndex = (int)season;
        return seasonIndex * 30;
    }
    public static int YearsToDays(int year)
    {
        return year * 4 * 30;
    }

    //calculate the difference bwtn 2 timestamps
    public static int CompareTimesstamps(GameTimestamp timestamp1, GameTimestamp timestamp2)
    {
        //Convert timestamps to hours
        int timestam1Hours = DaysToHours(YearsToDays(timestamp1.year)) + DaysToHours(SeasonsToDays(timestamp1.season)) + DaysToHours(timestamp1.day) + timestamp1.hour;
        int timestam2Hours = DaysToHours(YearsToDays(timestamp2.year)) + DaysToHours(SeasonsToDays(timestamp2.season)) + DaysToHours(timestamp2.day) + timestamp2.hour;
        int difference = (timestam2Hours - timestam1Hours);
        return Mathf.Abs(difference);
    }

    public static int TimestapInMinutes(GameTimestamp timestamp)
    {
        return HoursToMinutes(DaysToHours(YearsToDays(timestamp.year)) + DaysToHours(SeasonsToDays(timestamp.season)) + DaysToHours(timestamp.day) + timestamp.hour) + timestamp.minute;
    }

}