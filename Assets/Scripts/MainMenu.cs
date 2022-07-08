using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator[] panels;

    [Header("Image ON for SHOP")]
    public GameObject blackON;
    public GameObject blueON;
    public GameObject greenON;
    public GameObject orangeON;

    [Header("Scores All")]
    public Text scoresMenu;

    [Header("Sound Button Image")]
    public GameObject soundON;
    public GameObject soundOFF;

    private float scores = 0;

    private bool playClick = false;    
    private bool soundClick = false;

    void Start()
    {
        
    }

    
    void Update()
    {
        if (playClick)
        {
            SceneManager.LoadScene(1);
        }

        if (scores != SinglVar.scoresAll)
        {
            scores = SinglVar.scoresAll;
            if (scores < 0) SinglVar.scoresAll = scores = 0;
            scoresMenu.text = Mathf.Round(scores).ToString();
        }

        if (soundClick)
        {
            SinglVar.soundVolume = !SinglVar.soundVolume;
            SinglVar.SaveState();
        }

        if (SinglVar.soundVolume)
        {
            soundON.SetActive(true);
            soundOFF.SetActive(false);
            AudioListener.pause = false;
        }
        else
        {
            soundON.SetActive(false);
            soundOFF.SetActive(true);
            AudioListener.pause = true;
        }

        if (SinglVar.blackZubik)
        {
            blackON.SetActive(true);//
            blueON.SetActive(false); 
            greenON.SetActive(false);
            orangeON.SetActive(false);
        }

        if (SinglVar.blueZubik)
        {
            blackON.SetActive(false);
            blueON.SetActive(true); //
            greenON.SetActive(false);
            orangeON.SetActive(false);            
        }

        if (SinglVar.greenZubik)
        {
            blackON.SetActive(false);
            blueON.SetActive(false);
            greenON.SetActive(true); //
            orangeON.SetActive(false);
        }

        if (SinglVar.orangeZubik)
        {
            blackON.SetActive(false);
            blueON.SetActive(false);
            greenON.SetActive(false);
            orangeON.SetActive(true); //
        }


        playClick = false;
        soundClick = false;        
    }
    public void ShowPanel(int state)
    {
        if (panels[state].gameObject.activeSelf) return;

        for (int i = 0; i < panels.Length; i++)
            if (panels[i].gameObject.activeSelf)
                panels[i].Play("out");

        panels[state].gameObject.SetActive(true);
        panels[state].Play("in");
    }
    public void ActivateBlackZubik()
    {
        SinglVar.blackZubik = true; //
        blackON.SetActive(true);
        SinglVar.blueZubik = false;        
        SinglVar.greenZubik = false;
        SinglVar.orangeZubik = false;
    }
    public void ActivateBlueZubik()
    {
        SinglVar.blackZubik = false;
        SinglVar.blueZubik = true; // 
        blueON.SetActive(true);
        SinglVar.greenZubik = false;
        SinglVar.orangeZubik = false;        
    }
    public void ActivateGreenZubik()
    {
        SinglVar.blackZubik = false;
        SinglVar.blueZubik = false;         
        SinglVar.greenZubik = true; //
        greenON.SetActive(true);
        SinglVar.orangeZubik = false;
    }
    public void ActivateOrangeZubik()
    {
        SinglVar.blackZubik = false;
        SinglVar.blueZubik = false;
        SinglVar.greenZubik = false;        
        SinglVar.orangeZubik = true;//
        orangeON.SetActive(true);
    }

    public void PlayClick()
    {
        playClick = true;
    }
    public void SoundON()
    {
        soundClick = true;
    }
    public void Exitt() 
    {
        Application.Quit();
    }

}
