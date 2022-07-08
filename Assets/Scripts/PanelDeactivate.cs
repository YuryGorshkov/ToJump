using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelDeactivate : MonoBehaviour
{
    public void Out()
    {
        // print(gameObject.name);
        gameObject.SetActive(false);
    }
}
