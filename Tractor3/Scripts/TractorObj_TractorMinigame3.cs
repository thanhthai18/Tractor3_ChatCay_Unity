using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorObj_TractorMinigame3 : MonoBehaviour
{
    public bool isVaChamCay;
    public Tree_TractorMinigame3 currentTree;
    public float distanceTree;

    private void Awake()
    {
        isVaChamCay = false;
    }

    private void Start()
    {
        distanceTree = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            isVaChamCay = true;
            currentTree = collision.GetComponent<Tree_TractorMinigame3>();
            distanceTree = transform.position.x - currentTree.transform.position.x;
            if(distanceTree > 0)
            {
                currentTree.transform.DORotate(new Vector3(0, 0, 80), 0.1f);
            }
            else
            {
                currentTree.transform.DORotate(new Vector3(0, 0, -80), 0.1f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Tree"))
        {
            isVaChamCay = false;
            currentTree = null;
            distanceTree = 0;
        }
    }

    private void Update()
    {
        if (isVaChamCay && currentTree != null)
        {
            currentTree.transform.position = new Vector2(transform.position.x - distanceTree, currentTree.transform.position.y);
        }
    }
}
