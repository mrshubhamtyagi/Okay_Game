using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    public bool distanceAffectSpeed = false;
    public bool inverseDrag = false;
    public bool limitSpeedOverTime = false;

    private void Awake() => Instance = this;

    void Start()
    {

    }

}
