using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float speedHorizontal = 5f;

    [Header("ZUBIK SPRITE & ANIMATOR")]
    public SpriteRenderer blackZubikSprite;
    public SpriteRenderer blueZubikSprite;
    public SpriteRenderer greenZubikSprite;
    public SpriteRenderer orangeZubikSprite;
    public Animator zubikBlackAnim;
    public Animator zubikBlueAnim;
    public Animator zubikGreenAnim;
    public Animator zubikOrangeAnim;

    [Header("Public Game Object")]
    public GameObject newSound;
    public GameObject readyPanel;
    public GameObject loosePanel;
    public Text scoresT;

    [Header("Hit collider triger")]
    public Vector2 boxSize;
    public Vector3 offset;
    public LayerMask maskHit;

    private float scores = 0;
    private float horizontal;

    private Rigidbody2D rb2d;
    private SpriteRenderer hero;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        readyPanel.SetActive(true);
        Instantiate(newSound, transform.position, Quaternion.identity).SendMessage("Play", "endLevel");

        zubikBlackAnim.GetComponent<Animator>().enabled = false;
        zubikBlueAnim.GetComponent<Animator>().enabled = false;
        zubikGreenAnim.GetComponent<Animator>().enabled = false;
        zubikOrangeAnim.GetComponent<Animator>().enabled = false;

        if (SinglVar.blackZubik)
        {
            blackZubikSprite.GetComponent<SpriteRenderer>().enabled = true; //
            blueZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            greenZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            orangeZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            hero = blackZubikSprite;
        }
        if (SinglVar.blueZubik)
        {
            blackZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            blueZubikSprite.GetComponent<SpriteRenderer>().enabled = true; //
            greenZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            orangeZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            hero = blueZubikSprite;
        }
        if (SinglVar.greenZubik)
        {
            blackZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            blueZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            greenZubikSprite.GetComponent<SpriteRenderer>().enabled = true; //
            orangeZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            hero = greenZubikSprite;
        }
        if (SinglVar.orangeZubik)
        {
            blackZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            blueZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            greenZubikSprite.GetComponent<SpriteRenderer>().enabled = false;
            orangeZubikSprite.GetComponent<SpriteRenderer>().enabled = true;
            hero = orangeZubikSprite;
        }

        SinglVar.scoresLevel = 0;
    }


    void Update()
    {
        if (SinglVar.startGame)
        {
            if (rb2d.velocity.y > 0 && transform.position.y > scores)
            {
                scores = transform.position.y;
            }

            TrigerCollider();            

            horizontal = Input.acceleration.x;

            if (Input.acceleration.x < 0)
            {
                hero.GetComponent<SpriteRenderer>().flipX = false;
            }
            if (Input.acceleration.x > 0)
            {
                hero.GetComponent<SpriteRenderer>().flipX = true;
            }

            rb2d.velocity = new Vector2(Input.acceleration.x * speedHorizontal, rb2d.velocity.y);

            if (scores != SinglVar.scoresLevel)
            {                
                if (scores < 0) SinglVar.scoresLevel = scores = 0;
                scoresT.text = Mathf.Round(scores).ToString();
                SinglVar.scoresLevel = scores;
            }
            if (SinglVar.scoresLevel > SinglVar.scoresAll)
            {
                SinglVar.scoresAll = SinglVar.scoresLevel;
                SinglVar.SaveStars();
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Vector3 target = transform.position + offset;

        Gizmos.DrawWireCube(target, boxSize);
    }

    void TrigerCollider()
    {
        Collider2D[] hitObject = Physics2D.OverlapBoxAll(transform.position + offset, boxSize, 0);

        foreach (Collider2D hit in hitObject)
        {
            if ((hit.tag == "Platform" || hit.tag == "PlatformMoved") && rb2d.velocity.y == 0)
            {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
                Instantiate(newSound, transform.position, Quaternion.identity).SendMessage("Play", "jump");
            }
            else if (hit.tag == "BorderLeft")
            {
                var max = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));   //ѕолучаем верхний правый угол камеры
                transform.position = new Vector2(max.x, transform.position.y);
            }
            else if (hit.tag == "BorderRight")
            {
                var min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));   //ѕолучаем вектор нижнего левого угла камеры
                transform.position = new Vector2(min.x, transform.position.y);
            }
            if (hit.tag == "PlatformBad" && rb2d.velocity.y <= 0)
            {
                Destroy(hit.gameObject);
            }
        }

    }

    public void LevelGo()
    {
        SinglVar.startGame = true;
        readyPanel.SetActive(false);
        StartAnima();
    }

    public void StartAnima()
    {
        if (SinglVar.blackZubik)
        {
            zubikBlackAnim.GetComponent<Animator>().enabled = true; //
            zubikBlueAnim.GetComponent<Animator>().enabled = false;
            zubikGreenAnim.GetComponent<Animator>().enabled = false;
            zubikOrangeAnim.GetComponent<Animator>().enabled = false;
        }
        if (SinglVar.blueZubik)
        {
            zubikBlackAnim.GetComponent<Animator>().enabled = false;
            zubikBlueAnim.GetComponent<Animator>().enabled = true; //
            zubikGreenAnim.GetComponent<Animator>().enabled = false;
            zubikOrangeAnim.GetComponent<Animator>().enabled = false;
        }
        if (SinglVar.greenZubik)
        {
            zubikBlackAnim.GetComponent<Animator>().enabled = false;
            zubikBlueAnim.GetComponent<Animator>().enabled = false;
            zubikGreenAnim.GetComponent<Animator>().enabled = true; //
            zubikOrangeAnim.GetComponent<Animator>().enabled = false;
        }
        if (SinglVar.orangeZubik)
        {
            zubikBlackAnim.GetComponent<Animator>().enabled = false;
            zubikBlueAnim.GetComponent<Animator>().enabled = false;
            zubikGreenAnim.GetComponent<Animator>().enabled = false;
            zubikOrangeAnim.GetComponent<Animator>().enabled = true; //            
        }
    }
}
