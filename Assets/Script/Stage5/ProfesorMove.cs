using System.Collections;
using UnityEngine;

public class ProfesorMove : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private float calculatePost;
    private float lastPost;

    public float stopCount = 5f;
    [HideInInspector] public float timeStop = 0f;
    [HideInInspector] public bool isStopped;

    private Transform currentTarget;
    [SerializeField] private float speed = 1.7f;
    private Animator animator;
    private ProfessorDetection detection;

    [SerializeField]
    private bool isFacingRight;

    public bool IsFacingRight
    {
        get
        {
            return isFacingRight;
        }
        private set
        {
            if (isFacingRight != value)
            {
                transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
            }

            isFacingRight = value;
        }
    }

    private bool profActivity;

    private void Awake()
    {
        currentTarget = pointA;
        animator = GetComponent<Animator>();
        detection = GetComponent<ProfessorDetection>();
    }

    private void FixedUpdate()
    {
        if (currentTarget == pointA)
        {
            profActivity = true;
        }
        else if (currentTarget == pointB)
        {
            profActivity = false;
        }

        MoveToTarget();
    }

    private void MoveToTarget()
    {
        if (!isStopped)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed);
            animator.SetBool(AnimationString.profWalking, true);

            if (Vector2.Distance(transform.position, currentTarget.position) <= 0f)
            {
                timeStop = stopCount;
                isStopped = true;
                FacingDirection();
            }
        }
        else
        {
            if (detection.isDetected)
            {
                NoActivity();
                detection.theTime -= Time.deltaTime;
            }
            else if (!detection.isDetected)
            {
                StopMoving(profActivity);
                timeStop -= Time.deltaTime;
            }

            if (timeStop <= 0f)
            {
                currentTarget = (currentTarget == pointA) ? pointB : pointA;
                NoActivity();
                isStopped = false;
            }
        }
    }

    private void StopMoving(bool decision)
    {
        animator.SetBool(AnimationString.profWalking, false);
        if (decision)
        {
            animator.SetBool(AnimationString.profReading, true);
            animator.SetBool(AnimationString.profExperiment, false);
        }
        else if(!decision)
        {
            animator.SetBool(AnimationString.profReading, false);
            animator.SetBool(AnimationString.profExperiment, true);
        }
    }

    private void NoActivity()
    {
        animator.SetBool(AnimationString.profWalking, false);
        animator.SetBool(AnimationString.profReading, false);
        animator.SetBool(AnimationString.profExperiment, false);
        FacingDirection();
    }

    private void FacingDirection()
    {
        calculatePost = transform.position.x - lastPost;

        if (calculatePost > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        else if (calculatePost < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }

        lastPost = transform.position.x;
    }
}
