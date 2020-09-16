using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemy : MonoBehaviour
{
    #region Movement_variables
    public float movespeed;
    #endregion

    #region Physics_components
    Rigidbody2D EnemyRB;
    #endregion

    #region Targeting_variables
    public Transform player;
    #endregion

    #region Attack_variables
    public float passiveDamage;
    public float passiveRadius;
    public GameObject explosionObj;
    public float passiveTimer;
    private float originalTime;
    #endregion

    #region Health_variables
    public float maxHealth;
    float currHealth;
    #endregion

    #region Unity_functions
    //runs once on creation
    private void Awake()
    {
        EnemyRB = GetComponent<Rigidbody2D>();

        currHealth = maxHealth;

        originalTime = passiveTimer;
    }

    //runs once every frame
    private void Update()
    {
        //check to see if we know where player is
        if (player == null)
        {
            return;
        }

        Move();

        Passive();
    }
    #endregion

    #region Movement_functions

    //move directly at player
    private void Move()
    {
        //Calculate movement vector player position - enemy position = direction of player relative to enemy
        Vector2 direction = player.position - transform.position;

        EnemyRB.velocity = direction.normalized * movespeed;
    }
    #endregion

    #region Attack_functions

    //Raycasts box for player, causes damage, spawns explosion prefab
    private void Explode()
    {
        //call AudioManager for explosion
        FindObjectOfType<AudioManager>().Play("Explosion");
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, passiveRadius, Vector2.zero);
        bool check = false;
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                check = true;
            }
        }
        if (check)
        {
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.CompareTag("Player"))
                {
                    //Cause damage
                    Debug.Log("Hit Player with explosion");

                    //spawns explosion prefab
                    Instantiate(explosionObj, transform.position, transform.rotation);
                    hit.transform.GetComponent<PlayerController>().TakeDamage(passiveDamage);
                }
            }
        }
        else
        {
            Instantiate(explosionObj, transform.position, transform.rotation);
        }

    }

    private void Passive()
    {
        if (passiveTimer <= 0)
        {
            Explode();
            passiveTimer = originalTime;
        } else
        {
            passiveTimer -= Time.deltaTime;
        }
    }
    #endregion

    #region Health_functions

    //enemy takes damage based on value param
    public void TakeDamage(float value)
    {
        FindObjectOfType<AudioManager>().Play("BatHurt");
        //decrement health
        currHealth -= value;

        Debug.Log("Health is now " + currHealth.ToString());

        //check for death
        if (currHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Destroys enemy object
        Destroy(this.gameObject);
    }
    #endregion
}
