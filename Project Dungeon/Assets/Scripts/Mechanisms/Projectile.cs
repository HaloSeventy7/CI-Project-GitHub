﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 20f;
    Rigidbody2D myRigidbody;

    [SerializeField] int damageToDeal;
    [SerializeField] GameObject damageBurst;
    [SerializeField] bool damagePlayer = false;

    private bool enemyHit = false;

    void Start()
    {
        ChangeVelocity();
    }

    public void ChangeVelocity()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myRigidbody.velocity = transform.up * projectileSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (!damagePlayer)
        {
            if (collider.gameObject.tag != "Ignore" && collider.gameObject.tag != "Player" && collider.gameObject.tag != "Enemy (Base)")
            {
                if (collider.gameObject.tag == "Enemy") //if the projectile makes contact with an enemy, destroy it after a small time //destroying it immediately will prevent any damage from registering
                {
                    if (!enemyHit)
                    {
                        enemyHit = true;
                        collider.GetComponent<EnemyHealthManager>().HurtEnemy(damageToDeal);
                        Destroy(gameObject, 0.05f); //destroyed after a really small amount of time to ensure damage was dealt before being destroyed
                        GameObject tempBurst = Instantiate(damageBurst, collider.transform.position, collider.transform.rotation);
                        Destroy(tempBurst, 1.0f);
                        try //temporarily using a try, catch just in case the specific enemy uses a different controller
                        {
                            collider.GetComponentInParent<StandardEnemyController>().SetTarget(); //sets the player as the target once the enemy is hit //the enemy controller is in the parent object
                            collider.GetComponentInParent<StandardEnemyController>().SetFollowOnceSeen(); //sets the enemy to follow the player
                        }
                        catch
                        {
                            //pass
                        }
                    }
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (collider.gameObject.tag != "Ignore" && collider.gameObject.tag != "Enemy (Base)")
        {
            if (collider.gameObject.tag == "Player")
            {
                collider.GetComponent<PlayerHealthManager>().HurtPlayer(damageToDeal);
                Destroy(gameObject, 0.05f); //destroyed after a really small amount of time to ensure damage was dealt before being destroyed
                GameObject tempBurst = Instantiate(damageBurst, collider.transform.position, collider.transform.rotation);
                Destroy(tempBurst, 1.0f);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    public int GetDamageToDeal()
    {
        return damageToDeal;
    }

    public void SetDamageToDeal(int damage)
    {
        damageToDeal = damage;
    }

    public void SetDamagePlayer(bool status)
    {
        damagePlayer = status;
    }
}
