using UnityEngine;

public class GuardAI : MonoBehaviour
{
    public Transform[] patrolPoints;
    private int currentPoint = 0;

    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    public float searchRadius = 8f;
    public float maxSearchTime = 5f;

    private float searchTimer = 0f;

    private Vector3 lastKnownPosition;

    private VisionSensor vision;
    private HearingSensor hearing;
    private GuardMotor motor;

    private bool alarmaActivada = false;

    private enum State
    {
        Patrol,
        Chase,
        Search
    }

    private State state;

    void Start()
    {
        vision = GetComponent<VisionSensor>();
        hearing = GetComponent<HearingSensor>();
        motor = GetComponent<GuardMotor>();

        state = State.Patrol;
        GoToNextPoint();
    }

    void Update()
    {
        Perceive();

        switch (state)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Search:
                Search();
                break;
        }
    }

    void Perceive()
    {
        if (vision.CanSeeTarget())
        {
            alarmaActivada = true;
            lastKnownPosition = vision.target.position;
            state = State.Chase;
            return;
        }

        if (hearing.CanHearTarget(out Vector3 heardPos))
        {
            alarmaActivada = true;
            lastKnownPosition = heardPos;
            state = State.Search;
        }
    }

    void Patrol()
    {
        if (alarmaActivada) return; // 🔥 Nunca volverá a patrullar

        if (motor.HasArrived())
        {
            GoToNextPoint();
        }
    }

    void Chase()
    {
        motor.MoveTo(vision.target.position, chaseSpeed);

        if (!vision.CanSeeTarget())
        {
            searchTimer = 0f;
            state = State.Search;
        }

        if (Vector3.Distance(transform.position, vision.target.position) < 1.2f)
        {
            Debug.Log("¡CAPTURADO!");
            vision.target.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    void Search()
    {
        searchTimer += Time.deltaTime;

        if (motor.HasArrived())
        {
            Vector3 randomPoint = lastKnownPosition +
                                  Random.insideUnitSphere * searchRadius;
            randomPoint.y = transform.position.y;

            motor.MoveTo(randomPoint, patrolSpeed);
        }

    }

    void GoToNextPoint()
    {
        if (patrolPoints.Length == 0) return;

        motor.MoveTo(patrolPoints[currentPoint].position, patrolSpeed);
        currentPoint = (currentPoint + 1) % patrolPoints.Length;
    }
}