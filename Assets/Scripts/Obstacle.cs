using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject _player;
    [SerializeField] private AudioSource deathSoundEffect;

    public GameObject explosion;
    
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(this.gameObject);
        }
        
        else if (collision.tag == "Player")
        {
            Player player = collision.GetComponent<Player>();

            if (!player.IsInvincible)
            {
                deathSoundEffect.Play();
            }
            player.Die();
        }
        
        if (CompareTag("Asteroids"))
        {
            if (collision.tag == "Projectile")
            {
                // instantiate a particle system prefab

                Instantiate(explosion,transform.position,Quaternion.identity);
                Destroy(gameObject);
                Destroy(collision.gameObject);
            }
        }
    }
}
