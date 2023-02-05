using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

public class Foxi : Entity
{
    [SerializeField] private AudioSource GrrrSound;
    public float speed = 2f;
    private bool movingRight = true;
    public float rayDistance = 3f;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Vector3 pos;
    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    float upperLimit;


    private Transform target;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>(); 
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindGameObjectWithTag("Player1").GetComponent<Transform>();
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        //sprite.flipX = target.position.x > transform.position.x
        sprite.flipX = target.position.x > transform.position.x;


        if (movingRight == false)
        {
            sprite.flipX = true;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.y * Vector2.right, rayDistance);
            if (hit.collider != null)
            {
                GrrrSound.Play();
            }
        }

        transform.position = new Vector3
            (Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
            Mathf.Clamp(transform.position.y, bottomLimit, upperLimit),
            transform.position.z);
        //       RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, rayDistance);


    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0,-2,0), new Vector3(0, -2, 0)+ transform.position + transform.localScale.x * Vector3.left * rayDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + new Vector3(0, -2, 0), transform.position+ new Vector3(0,-2,0) + transform.localScale.x * Vector3.right * rayDistance);

    }
}
