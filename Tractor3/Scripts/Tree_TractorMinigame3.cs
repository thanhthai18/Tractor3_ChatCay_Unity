using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_TractorMinigame3 : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            GameController_TractorMinigame3.instance.Lose();
        }
    }
}
