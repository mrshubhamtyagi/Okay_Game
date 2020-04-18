using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public static BallController Instance;

    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private float ballSize = 1;
    private Ball ball;

    private Vector2 startPosition;
    private Vector2 endPosition;
    private Vector2 direction;
    private Vector2 distance;

    private Camera cam;


    private void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    void Start()
    {
        ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity).GetComponent<Ball>();
        ball.transform.localScale = Vector3.one * ballSize;
        ball.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = GetMousePosition();
        }
        else if (Input.GetMouseButtonUp(0))


        {
            endPosition = GetMousePosition();

            direction = endPosition - startPosition;

        }
    }

    private Vector2 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }
}
