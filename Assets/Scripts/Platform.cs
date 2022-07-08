using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [Header("Triger")]
    public Vector3 offset;
    public Vector3 boxSize;

    public GameObject dest;

    public bool moved = false;
    private bool triger = true;    

    void Start()
    {

    }

    void Update()
    {
        TrigerCollider();

        if (moved)
        {
            var min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
            var max = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));   //ѕолучаем верхний правый угол камеры

            if (transform.position.x == min.x || transform.position.x == max.x)
                triger = !triger;

            if (triger)
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(max.x, transform.position.y), Time.deltaTime);
            else
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(min.x, transform.position.y), Time.deltaTime);
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
            if (hit.tag != "Player" && hit.tag != "Destroyer" && hit.gameObject != gameObject && !hit.tag.StartsWith("Border") && hit.tag != "PlatformMoved" && gameObject.tag != "PlatformMoved")
            {
                dest.SendMessage("Spawn", SendMessageOptions.DontRequireReceiver);
                Destroy(hit.gameObject);
            }
        }
    }
}
