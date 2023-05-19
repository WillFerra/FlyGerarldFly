
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    public float initialFloatForce = 10f; // the initial force at which the character floats
    public float maxFloatForce = 20f; // the maximum force at which the character can float
    public float floatForceIncreaseRate = 1f; // the rate at which the float force increases over time
    public float downForce = 5f; // the force at which the character descends when the down arrow is pressed

    private Rigidbody2D rb;
    private float _currentFloatForce;
    
    [Header("Projectile References")] 
    [SerializeField] private Transform projectileSource;

    [SerializeField] private GameObject projectile;
    
    [SerializeField] private AudioSource audioSource;
    
    [Header("Triple Shot Powerup")]
    [SerializeField] private bool isInvincible;

    public bool IsInvincible => isInvincible;

    [SerializeField] private AudioClip shotSoundEffect;
    [SerializeField] private AudioClip powerUpSoundEffect;

    [SerializeField] private GameObject invincible;

    private Animator animator;
    
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // disable gravity for the character
        _currentFloatForce = initialFloatForce;
    }
    
    // Update is called once per frame
    void Update()
    {
        // spawn a projectile when we press the space bar
        if (Input.GetButtonDown("Jump"))
        {
            Fire();
        }
    }

    void FixedUpdate()
    {
        // apply a force in the upward direction of the GameObject
        rb.AddForce(transform.up * _currentFloatForce, ForceMode2D.Force);

        // increase the float force over time while the up arrow key is being held down
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (_currentFloatForce < maxFloatForce)
            {
                _currentFloatForce += floatForceIncreaseRate;
            }
        }
        else
        {
            _currentFloatForce = initialFloatForce;
        }

        // apply a force in the downward direction of the GameObject when the down arrow key is pressed
        if (Input.GetKey(KeyCode.DownArrow))
        {
            rb.AddForce(-transform.up * downForce, ForceMode2D.Force);
        }
    }
    
    // a custom method used to fire projectiles
    void Fire()
    {
        // Create a bullet
        Instantiate(projectile, projectileSource.position, projectileSource.rotation);
        audioSource.PlayOneShot(shotSoundEffect);
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Powerup"))
        {
            isInvincible = true;
            StartCoroutine(StopPowerUp());
        }
    }

    public void Die()
    {
        if (isInvincible == false)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator StopPowerUp()
    {
        animator.SetBool("IsInvincible", true);
        audioSource.PlayOneShot(powerUpSoundEffect);
        yield return new WaitForSeconds(5);
        isInvincible = false;
        animator.SetBool("IsInvincible", false);
    }
}