using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Animator anim;
    public float totalHealth;
    public float currentHealth;
    public float expGranted;
    public float attackDamage;
    public float moveSpeed;

	// Use this for initialization
	void Start ()
    {
        currentHealth = totalHealth;

	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetMouseButtonDown(0))
        {
            
        }
	}

    public void GetHit(float damage)
    {
        anim.SetInteger("Condition", 2);
        currentHealth -= damage;
        StartCoroutine(RecoverFromHit());
        print(currentHealth);
    }
    IEnumerator RecoverFromHit()
    {
        yield return new WaitForSeconds(0.1f);
        anim.SetInteger("Condition", 0);
    }
}
