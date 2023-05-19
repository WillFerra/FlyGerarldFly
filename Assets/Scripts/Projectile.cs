using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    // The projectile speed
    [SerializeField] private float speed = 50f;
    
    // The projectile lifetime
    [SerializeField] private float lifetime = 20;
    
    // The rigidbody component
    private Rigidbody2D _rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = transform.right * speed;

        // Destroy the object after some time.
        Destroy(gameObject, lifetime);
        
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            Destroy(this.gameObject);
        }
    }
}