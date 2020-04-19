using UnityEngine;

public class Block : MonoBehaviour
{
    public float speed = 0.2f;
    private SpriteRenderer sr;
    private PhysicsMaterial2D pm;

    private bool isCollided = false;
    private Color fadeColor;


    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        pm = GetComponent<Collider2D>().sharedMaterial;
    }

    void Start()
    {
        fadeColor = sr.color;
        fadeColor.a = 0;

        if (GameController.Instance.limitSpeedOverTime)
            pm.bounciness = 0.5f;
        else
            pm.bounciness = 1;
    }

    private void Update()
    {
        if (isCollided)
        {
            sr.color = Color.Lerp(sr.color, fadeColor, speed);
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, speed);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //isCollided = true;
    }
}
