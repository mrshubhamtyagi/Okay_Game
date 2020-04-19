using UnityEngine;

public class Dot : MonoBehaviour
{
    public bool isMainDot = false;
    public Transform target;

    private float trailSpeed = 0.2f;

    private TrailRenderer trail;

    void Awake()
    {
        if (isMainDot)
            trail = transform.GetChild(0).GetComponent<TrailRenderer>();
    }


    private void OnDisable()
    {
        if (trail)
            trail.Clear();
    }


    void Update()
    {
        if (target && !isMainDot)
        {
            transform.position = Vector2.Lerp(transform.position, target.position, trailSpeed);
        }
    }


    private void OnBecameInvisible()
    {
        if (isMainDot)
            gameObject.SetActive(false);
    }
}
