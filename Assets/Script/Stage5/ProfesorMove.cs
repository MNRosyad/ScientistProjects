using System.Collections;
using UnityEngine;

public class ProfesorMove : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;

    private Transform currentTarget;
    [SerializeField] private float speed = 2f;
    Animator anim;
    private void Awake()
    {
        currentTarget = pointA;
        anim = GetComponent<Animator>();  
    }

    private void Update()
    {
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, Time.deltaTime * speed);

        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f)
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
            anim.SetBool("Walking", true);
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
    }
}
