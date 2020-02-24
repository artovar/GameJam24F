using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMelee : MonoBehaviour
{
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public bool enemyHitted;
    
    
    public Animator animator;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F)) 
        {
            attack();
        }
    }
    void attack() {
        //animacion
        animator.SetTrigger("Attack");
        animator.SetBool("estaAtacando", true);
        //detectar enemigos
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position,attackRange,enemyLayers);

        foreach (Collider2D enemy in hitEnemies) 
        {
            Debug.Log("HIT");
            enemyHitted = true;
        }
        enemyHitted = false;
    }
}
