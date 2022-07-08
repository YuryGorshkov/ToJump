using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border : MonoBehaviour
{
    public bool right = false;

    void Start()
    {

    }

    void Update()
    {
        var min = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));   //ѕолучаем вектор нижнего левого угла камеры
        var max = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, Camera.main.nearClipPlane));   //ѕолучаем верхний правый угол камеры

        if (right) transform.position = new Vector2(max.x + 1, Camera.main.transform.position.y);
        else transform.position = new Vector2(min.x - 1, Camera.main.transform.position.y);
    }
}
