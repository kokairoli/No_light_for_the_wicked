using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Windows;

public class Enemy_movement : MonoBehaviour
{

    private float speed = 1.5f;
    private GameObject playerCollider;
    private LayerMask colliderLayers;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private Rigidbody2D body;

    private Rigidbody2D playerBody;

    public bool haslineOfSight = false;
    private void DetermineHasLineOfSight()
    {
        RaycastHit2D ray = Physics2D.Raycast(transform.position, playerCollider.transform.position - transform.position,Mathf.Infinity, colliderLayers);
        if (ray.collider != null)
        {
            haslineOfSight = ray.collider.CompareTag("Player");
            if (haslineOfSight)
            {
                Debug.DrawRay(transform.position, playerCollider.transform.position - transform.position,Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, playerCollider.transform.position - transform.position, Color.red);
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerCollider = GameObject.FindGameObjectWithTag("Player");
        playerBody = playerCollider.GetComponentInParent<Rigidbody2D>();
        colliderLayers = LayerMask.GetMask("Ground", "Player", "Wall");
    }

    // Update is called once per frame
    void Update()
    {
        if (haslineOfSight)
        {
            Move();
        }
        checkForAnimations();
    }

    private void FixedUpdate()
    {
        DetermineHasLineOfSight();
    }

    private void Move()
    {
        float directionX = playerBody.position.x - body.position.x;
        body.linearVelocity = new Vector2(directionX, 0).normalized * speed;
    }

    private void checkForAnimations()
    {
        checkForRunAnimations();
    }

    private void checkForRunAnimations()
    {
        animator.SetFloat("Speed", Mathf.Abs(body.linearVelocity.x));
    }
}
