using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainDotController : MonoBehaviour
{
    public static MainDotController Instance;

    #region Main Dot
    [Header("Main Dot")]
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private float mainDotSize = 1;
    [SerializeField] private float speed = 3;
    private Dot mainDot;
    private Rigidbody2D rb;
    #endregion

    #region Trial Dot
    [Header("Trial Dot")]
    [SerializeField] private GameObject trailDotPrefab;
    [SerializeField] private float trailDotSize = 0.4f;
    [SerializeField] private int trailDotCount = 5;
    private Transform[] trailDots;
    #endregion

    private Vector2 startPoint;
    private Vector2 endPoint;
    private Vector2 direction;
    private float distance;

    private Camera cam;


    private void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    void Start()
    {
        mainDot = Instantiate(dotPrefab, Vector3.zero, Quaternion.identity).GetComponent<Dot>();
        mainDot.isMainDot = true;
        mainDot.transform.localScale = Vector3.one * mainDotSize;
        mainDot.gameObject.SetActive(false);

        rb = mainDot.GetComponent<Rigidbody2D>();

        SpawnTrailDots();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mainDot.gameObject.SetActive(false);
            endPoint = startPoint = GetMousePosition();

            mainDot.transform.position = startPoint;
            mainDot.gameObject.SetActive(true);
            direction = Vector2.zero;
            rb.velocity = Vector2.zero;


            ToggleTrailDots(true);
        }
        else if (Input.GetMouseButton(0))
        {
            endPoint = GetMousePosition();
            CalculateDirection();
            DrawTrailDots();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            ToggleTrailDots(false);
            MovePlayer();
        }
    }

    #region Main Dot Stuff
    private Vector2 GetMousePosition()
    {
        return cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void CalculateDirection()
    {
        direction = (endPoint - startPoint).normalized;

        if (GameController.Instance.inverseDrag)
            direction *= -1;
    }

    private void MovePlayer()
    {
        float _speed = speed;

        if (GameController.Instance.distanceAffectSpeed)
            _speed = distance * (speed * 0.5f);

        rb.velocity = (direction * Time.deltaTime).normalized * _speed;
    }
    #endregion


    #region Trial Stuff
    private void SpawnTrailDots()
    {
        trailDots = new Transform[trailDotCount];
        for (int i = 0; i < trailDotCount; i++)
        {
            trailDots[i] = Instantiate(trailDotPrefab, mainDot.transform).transform;
            trailDots[i].localScale = Vector3.one * trailDotSize;
            trailDots[i].gameObject.SetActive(false);
        }
    }

    private void DrawTrailDots()
    {
        distance = Vector2.Distance(endPoint, startPoint);
        float temp = distance / trailDotCount;

        for (int i = 0; i < trailDotCount; i++)
        {
            trailDots[i].localPosition = direction * (i + 1) * temp;
        }
    }

    private void ToggleTrailDots(bool _flag)
    {
        foreach (Transform child in trailDots)
            child.gameObject.SetActive(_flag);
    }
    #endregion

}
