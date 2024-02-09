using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public Transform graphics;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public float jumpHeightNodeRequirement = 0.8f;
    public float jumpHeight = 0.3f;
    public float jumpCheckOffset = 0.1f;
    public float activateDistance = 3f;

    public bool canJump = true;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    bool isGrounded = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    Seeker seeker;
    Rigidbody2D rb;

    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone() && TargetInDistance())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(path == null)
        {
            return;
        }
        if(currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        } else
        {
            reachedEndOfPath = false;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        if (isGrounded && canJump) 
        {
            //rb.AddForce(Vector2.up * speed * jumpHeight);
            if (direction.y > jumpHeightNodeRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpHeight);
            }
            //rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        }

        rb.AddForce(force);
        //rb.velocity = force;
        //rb.velocity = new Vector2(force.x, rb.velocity.y);
        
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if(distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        if (rb.velocity.x >= 0.01f)
        {
            graphics.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (rb.velocity.x <= -0.01f)
        {
            graphics.localScale = new Vector3(1f, 1f, 1f);
        }

        animator.SetFloat("Speed", Mathf.Abs(direction.x));
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, activateDistance);
    }

    bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }
}
