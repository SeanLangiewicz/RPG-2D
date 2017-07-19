using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Animation")]
    public Animator anim = null;

    [Header("Movement")]
    public float movementSpeed;
    public float velocity;
    public Rigidbody rb;


    [Header("Combat")]
    private List<Transform> enemiesInRange = new List<Transform>();
    private bool canMove = true;
    private bool canAttack = true;
    private bool isAttacking = false;

    public float attackDamage;
    public float attackSpeed;
    public float attackRange;


	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
        Move();
       
	}

    void GetInput()
    {
        //Attack
        if(Input.GetMouseButtonDown(0))
        {
            print("attacking");
            Attack();
        }
        // Move left
        if (Input.GetKey(KeyCode.A))
        {
            SetVelocity(-1);
            anim.SetInteger("Condition", 0);
        }

        else if (Input.GetKeyUp(KeyCode.A))
        {
            SetVelocity(0);
            anim.SetInteger("Condition", 0);
        }

        //move right
        if (Input.GetKey(KeyCode.D))
        {
            SetVelocity(1);
            anim.SetInteger("Condition", 0);
        }

        else if (Input.GetKeyUp(KeyCode.D))
        {
            SetVelocity(0);
            anim.SetInteger("Condition", 0);
        }
    }

    #region MOVEMENT
    void Move()
    {
        if(velocity ==0)
        {
          
            return;
        }
        else
        {
            if (canAttack)
            {
                anim.SetInteger("Condition", 1);
                rb.MovePosition(transform.position + (Vector3.right * velocity * movementSpeed * Time.deltaTime));
            }
        }
     }

   

    void SetVelocity(float dir)
    {
            if (dir < 0)
                {
            transform.LookAt(transform.position + Vector3.left);
            
                }

            else if (dir > 0)
            {
                transform.LookAt(transform.position + Vector3.right);
            
            }
            velocity = dir;

    }
    #endregion

    #region COMBAT
    void Attack()
    {
        if (!canAttack)
        {
            return;
        }
        else
        {
            anim.SetInteger("Condition", 2);
            StartCoroutine(AttackRoutine());
            StartCoroutine(AttackCooldown());
        }
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(1 / attackSpeed);
        canAttack = true;
    }

    IEnumerator AttackRoutine()
    {
        canMove = false;
        canAttack = false;
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition", 0);
        GetEnemiesInRange();
        foreach(Transform enemy in enemiesInRange)
        {
            EnemyController ec = enemy.GetComponent<EnemyController>();
            if (ec == null) continue;
            ec.GetHit(attackDamage);
        }

        yield return new WaitForSeconds(.065f); ;
        canMove = true;
    }
    #endregion

    void GetEnemiesInRange()
    {
        foreach(Collider c in Physics.OverlapSphere((transform.position + transform.forward * 0.5f),0.5f))
            {
                if(c.gameObject.CompareTag("Enemy"))
                {
                    enemiesInRange.Add(c.transform);
                }
            }
    }
}

