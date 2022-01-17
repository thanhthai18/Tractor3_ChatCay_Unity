using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController_TractorMinigame3 : MonoBehaviour
{
    public static GameController_TractorMinigame3 instance;
    public Vector2 mouseCurrentPos;
    public RaycastHit2D[] hit;
    public Camera mainCamera;
    public GameObject tractorObj;
    public bool isWin, isLose;
    public float screenRatio;
    public bool isHoldLeft, isHoldRight, isHoldMouse;
    public float speed;
    public Text txtTime;
    public int time;
    public Coroutine timeCoroutine;
    public float minutes;
    public float seconds;
    public List<GameObject> listC = new List<GameObject>();



    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(instance);

        isWin = false;
        isLose = false;
        isHoldLeft = false;
        isHoldRight = false;
        isHoldMouse = false;
        speed = 5;
        time = 90;
    }

    private void Start()
    {
        SetSizeCamera();
        screenRatio = Screen.width * 1f / Screen.height;
        timeCoroutine = StartCoroutine(CountingTime());
    }

    void SetSizeCamera()
    {
        float f1 = 16.0f / 9;
        float f2 = Screen.width * 1.0f / Screen.height;

        mainCamera.orthographicSize *= f1 / f2;
    }

    IEnumerator CountingTime()
    {
        while (time > 0)
        {
            yield return new WaitForSeconds(1);
            time--;
            minutes = Mathf.FloorToInt(time / 60);
            seconds = Mathf.FloorToInt(time % 60);

            txtTime.text = string.Format("{0:00}:{1:00}", minutes, seconds);


            if (time == 0 && !isWin)
            {
                StopCoroutine(timeCoroutine);
                Lose();
            }
        }
    }

    public void Win()
    {
        isWin = true;
        Debug.Log("Win");
        StopAllCoroutines();
    }

    public void Lose()
    {
        isLose = true;
        Debug.Log("Thua");
        StopAllCoroutines();
    }

    private void Update()
    {
        if (!isWin && !isLose)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseCurrentPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                hit = Physics2D.RaycastAll(mouseCurrentPos, Vector2.zero);
                if (hit.Length != 0)
                {
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (hit[i].collider.gameObject.CompareTag("People"))
                        {

                            isHoldLeft = true;
                            isHoldRight = false;
                            tractorObj.transform.localScale = new Vector2(0.15f, 0.15f);
                        }
                        if (hit[i].collider.gameObject.CompareTag("Box"))
                        {
                            isHoldRight = true;
                            isHoldLeft = false;
                            tractorObj.transform.localScale = new Vector2(-0.15f, 0.15f);
                        }
                    }
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                isHoldLeft = false;
                isHoldRight = false;
                isHoldMouse = false;
            }


            if (isHoldLeft)
            {
                tractorObj.transform.Translate(Vector2.left * speed * Time.deltaTime);
                tractorObj.transform.position = new Vector2(Mathf.Clamp(tractorObj.transform.position.x, -mainCamera.orthographicSize * screenRatio + 0.5f, mainCamera.orthographicSize * screenRatio), tractorObj.transform.position.y);
            }
            if (isHoldRight)
            {
                tractorObj.transform.Translate(Vector2.right * speed * Time.deltaTime);
                tractorObj.transform.position = new Vector2(Mathf.Clamp(tractorObj.transform.position.x, -mainCamera.orthographicSize * screenRatio, mainCamera.orthographicSize * screenRatio - 0.5f), tractorObj.transform.position.y);
            }


        }
    }
}
