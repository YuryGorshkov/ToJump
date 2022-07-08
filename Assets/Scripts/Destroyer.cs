using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroyer : MonoBehaviour
{
    public int platformCount = 26;
    public GameObject player;
    public GameObject loosePanel;
    public GameObject pausePanel;
    public GameObject newSound;

    [Header("Platforms")]
    public GameObject platform;
    public GameObject platformBad;
    public GameObject platformMoved;

    [Header("PlatformParams")]
    public float badCoef;
    public float superPlatCoef;

    private Vector3 min;
    private Vector3 max;

    private void Start()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));   
        max = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));   

        for (int i = 0; i < platformCount; i++)
        {
            Instantiate(platform, new Vector2(Random.Range(min.x, max.x), Random.Range(min.y,max.y)), Quaternion.identity);
        }
        
        Instantiate(platform, new Vector2(player.transform.position.x,player.transform.position.y - 1), Quaternion.identity);
    }

    private void Update()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));   
        max = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));   

        transform.position = new Vector2(Camera.main.transform.position.x, min.y - 2);
    }

    public void Spawn()
    {
        min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));   
        max = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));

        if (Random.Range(0, 100) >= superPlatCoef * 100)
        {
            Instantiate(platform, new Vector2(Random.Range(min.x, max.x), max.y + Random.Range(0.5f, 1f)), Quaternion.identity);
        }
        else
        {
            if (Random.Range(0, 100) <= badCoef * 100)
                Instantiate(platformBad, new Vector2(Random.Range(min.x, max.x), max.y + Random.Range(0.5f, 1f)), Quaternion.identity);
            else
                Instantiate(platformMoved, new Vector2(Random.Range(min.x, max.x), max.y + Random.Range(0.5f, 1f)), Quaternion.identity);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {       
        Spawn();

        if(collision.tag == "Player")
        {
            SinglVar.SaveStars();
            loosePanel.SetActive(true);
            Instantiate(newSound, transform.position, Quaternion.identity).SendMessage("Play", "endLevel");
        }

        Destroy(collision.gameObject);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
        SinglVar.SaveStars();
    }
    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        SinglVar.startGame = false;
        Instantiate(newSound, transform.position, Quaternion.identity).SendMessage("Play", "endLevel");
    }
    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        SinglVar.startGame = true;
    }

}
