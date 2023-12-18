using System.Collections;
using UnityEngine;

public class ProfesorMove : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private float calculatePost;
    private float lastPost;

    private Transform currentTarget;
    [SerializeField] private float speed = 2f;
    private Animator animator;

    [SerializeField]
    private bool isFacingRight = false;

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

    private bool randomBoolean
    {
        get
        {
            return (Random.value > 0.5f);
        }
    }

    private void Awake()
    {
        currentTarget = pointA;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, currentTarget.position) < 0f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
            animator.SetBool(AnimationString.profWalking, true);
            FacingDirection();
        }
        else if (Vector2.Distance(transform.position, currentTarget.position) == 0f)
        {
            StopMoving(randomBoolean);
            //StartCoroutine(IdleCount());
        }
    }

    private void StopMoving(bool decision)
    {
        transform.position = currentTarget.position;
        animator.SetBool(AnimationString.profWalking, false);
        if(decision)
        {
            animator.SetBool(AnimationString.profWalking, false);
            animator.SetBool(AnimationString.profReading, true);
        }
        else if(!decision)
        {
            animator.SetBool(AnimationString.profWalking, false);
            animator.SetBool(AnimationString.profExperiment, true);
        }
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

    private IEnumerator IdleCount()
    {
        yield return new WaitForSeconds(3f);
        transform.position = new Vector2(transform.position.x + 0.01f, transform.position.y);
        animator.SetBool(AnimationString.profReading, false);
        animator.SetBool(AnimationString.profExperiment, false);
        Debug.Log("Done!");
    }
}
