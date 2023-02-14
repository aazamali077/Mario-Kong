using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite[] runsprites;
    public Sprite climbSprite;
    private int index;

    private Rigidbody2D rb2d;

    private Collider2D[] results;
    private Collider2D colliders;

    private Vector2 direction;
    [SerializeField] float moveSpeed = 1f, jumpStrength = 1f;

    private bool isGrounded, isClimbing;



    void Start()
    {
        spriteRenderer= GetComponent<SpriteRenderer>();
        rb2d= GetComponent<Rigidbody2D>();
        colliders= GetComponent<Collider2D>();
        results = new Collider2D[4];
    }

    private void OnEnable()
    {
        InvokeRepeating(nameof(AnimatePlayer), 1/12f, 1/12f);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    private void CheckCollision()
    {
        isGrounded= false;
        isClimbing= false;
        Vector2 size = colliders.bounds.size;
        size.y += 0.1f;
        size.x /= 2;
        int amount  = Physics2D.OverlapBoxNonAlloc(transform.position, size,0,results);
        for (int i = 0;i<amount;i++)
        {
            GameObject hit = results[i].gameObject;
            if (hit.layer == LayerMask.NameToLayer("Ground")) 
            {
                isGrounded= hit.transform.position.y<(transform.position.y-0.1f);
                Physics2D.IgnoreCollision(colliders, results[i], !isGrounded);
            }
            else if (hit.layer == LayerMask.NameToLayer("Ladder"))
            {
                isClimbing = true;
            }
        }
    }
    void Update()
    {
        CheckCollision();

        if (isClimbing)
        {
            direction.y = Input.GetAxis("Vertical") * moveSpeed;
        }


        if (isGrounded&& Input.GetButtonDown("Jump"))
        {
            direction = Vector2.up * jumpStrength;
        }
        else
        {
            direction += Physics2D.gravity * Time.deltaTime;
        }

        direction.x = Input.GetAxis("Horizontal")*moveSpeed;

        if (isGrounded)
        {
            direction.y = Mathf.Max(direction.y, -1f);
        }
       
        if (direction.x > 0f)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (direction.x<0f)
        {
            transform.eulerAngles = new Vector3(0,180,0);
        }
    }


    private void FixedUpdate()
    {
        rb2d.MovePosition(rb2d.position+direction*Time.fixedDeltaTime);
    }

    private void AnimatePlayer()
    {
        if (isClimbing)
        {
            spriteRenderer.sprite = climbSprite;

        }
        else if(direction.x!=0)
        {
            index++;
            if(index>= runsprites.Length)
            {
                index= 0;
            }
            spriteRenderer.sprite = runsprites[index];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            enabled= false;
            FindObjectOfType<GameManager>().levelComplete();
        }

        else if (collision.gameObject.CompareTag("Objective"))
        {
            enabled= false;
            FindObjectOfType<GameManager>().LevelFailed();
        }
    }
}
