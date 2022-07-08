using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglVar
{
    public static bool startGame = false;

    public static float scoresAll = 0;
    public static float scoresLevel = 0;

    public static bool soundVolume = true;
    
    public static bool blackZubik = true;
    public static bool blueZubik = false;
    public static bool greenZubik = false;
    public static bool orangeZubik = false;

    static SinglVar()
    {
        scoresAll = PlayerPrefs.GetFloat("scoresAll", 0);
    }
    public static void SaveStars()
    {
        PlayerPrefs.SetFloat("scoresAll", scoresAll);
        PlayerPrefs.Save();
    }
    public static void LoadState()
    {
        soundVolume = PlayerPrefs.GetInt("soundVolume", 1) == 1 ? true : false;
    }
    public static void SaveState()
    {
        if (soundVolume) 
            PlayerPrefs.SetInt("soundVolume", 1);
        else 
            PlayerPrefs.SetInt("soundVolume", 0);

        PlayerPrefs.Save();
    }
}
