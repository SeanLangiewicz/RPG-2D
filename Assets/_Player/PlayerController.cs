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
    bool isAttacking = false;

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

   
        void Move()
    {
        if(velocity ==0)
        {
          
            return;
        }
        else
        {
            if (!isAttacking)
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
    void Attack()
    {
        if (isAttacking)
        {
            return;
        }
        else
        {
            anim.SetInteger("Condition", 2);
            StartCoroutine(AttackRoutine());
        }
    }

    IEnumerator AttackRoutine()
    {
        isAttacking = true;
        yield return new WaitForSeconds(0.5f);
        anim.SetInteger("Condition", 0);
        yield return new WaitForSeconds(1); ;
        isAttacking = false;
    }
}
