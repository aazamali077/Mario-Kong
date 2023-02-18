using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    [SerializeField] private float speed;

    private void Awake()
    {
        rigidBody= GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            rigidBody.AddForce(collision.transform.right * speed, ForceMode2D.Impulse);
        }
    }
}
