﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float moveSpeed;

    public Vector2 lastMove; //Use Vector 2 for (x,y); not Vector 3 b/c it's (x,y,z)
    private bool playerMoving;

    [SerializeField] float attackTime = 0.5f;
    private float attackTimeCounter;
    private bool playerAttacking;
    private bool abilityActive = false;

    private float storeMoveSpeed;
    private float storeAttackTime;

    private static bool playerExists; //sets the player to true and keeps it true when entering back into the starting scene (this avoids duplicates)

    public Rigidbody2D myRigidbody; //keep this public
    Animator animator;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        storeMoveSpeed = moveSpeed;
        storeAttackTime = attackTime;

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        abilityActive = false;
    }

    void Update() //don't do fixed update because it's not checked every frame
    {
        playerMoving = false;

        if (!playerAttacking && !abilityActive)
        {
            if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f) //GetAxisRaw takes the input of that very second
            {
                myRigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, myRigidbody.velocity.y); //In this case, Time.deltaTime made the player move a LOT slower
                playerMoving = true;
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
            }

            if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f) //GetAxisRaw takes the input of that very second
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                playerMoving = true;
                lastMove = new Vector2(0, Input.GetAxisRaw("Vertical"));
            }

            if (Input.GetAxisRaw("Horizontal") < 0.5f && Input.GetAxisRaw("Horizontal") > -0.5f) //prevents the "skating" movement effect
            {
                myRigidbody.velocity = new Vector2(0f, myRigidbody.velocity.y);
            }

            if (Input.GetAxisRaw("Vertical") < 0.5f && Input.GetAxisRaw("Vertical") > -0.5f)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 0f);
            }

            if (Input.GetKeyDown(KeyCode.F)) //inside the !playerAttacking so that the player isn't able to spam their light attack and make the character look like it's attack is "frozen"
            {
                LightAttack();
            }
        }

        if (attackTimeCounter > 0)
        {
            attackTimeCounter -= Time.deltaTime; //ticks the timer down over time
        }

        if (attackTimeCounter <= 0)
        {
            playerAttacking = false;
            animator.SetBool("PlayerAttacking", false);
        }

        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal")); //all these animator.Set(s) update the animator variables that were created wit
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        animator.SetBool("PlayerMoving", playerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
        //In order to not make the animations take time to activate (i.e. the player starts moving, but is still in the idle animation for a little while),
        //click on the transition, uncheck "Has Exit Time," uncheck "Fixed Duration," and set "Transition Duration" to 0. Supposedly, "Transition Duration
        //is what causes this the most.
    }

    public void LightAttack()
    {
        attackTimeCounter = attackTime;
        playerAttacking = true;
        myRigidbody.velocity = Vector2.zero; //x and y value of 0 //prevents the player from sliding while attacking
        animator.SetBool("PlayerAttacking", true); //what dictates whether the attack is done and animated
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public float GetAttackTime()
    {
        return attackTime;
    }

    public void SetAttackTime(float time)
    {
        attackTime = time;
    }

    public bool GetPlayerAttacking()
    {
        return playerAttacking;
    }

    public bool GetAbilityActive()
    {
        return abilityActive;
    }

    public void SetAbilityActive(bool status)
    {
        abilityActive = status;
    }

    public Vector2 GetLastMove()
    {
        return lastMove;
    }

    public void ResetMoveSpeedAndAttackTime() //was specifically made because abilities would glitch (save changed values) between scene loads
    {
        moveSpeed = storeMoveSpeed;
        attackTime = storeAttackTime;
    }
}