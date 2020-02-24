using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtaqueMelee : MonoBehaviour
{
    //public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    float at = 0f;
    bool estaAtac = false;

    public bool enemyHitted;
    
    
    Animator animator;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("estaAtacando", true);
            estaAtac = true;
            StartCoroutine(Attack());
            at = 0f;
        }
        
    }

    private void FixedUpdate()
    {
        Haddleattack();
        resetValues();
    }

    private void Haddleattack()
    {
        if (estaAtac)
        {
            animator.SetTrigger("attack");
        }
    }

    IEnumerator Attack() {
        
        while (at < 1f)
        {
            animator.SetFloat("ataque", at);
            yield return new WaitForEndOfFrame();
            at += 0.000001f;
        }
        //detectar enemigos
        // Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        /*
         foreach (Collider2D enemy in hitEnemies)
         {
             Debug.Log("HIT");
             enemyHitted = true;
         }
         enemyHitted = false;
         StopCoroutine(Attack());*/
    }


    void resetValues () {
        //animacion
        estaAtac = false;
        animator.SetBool("estaAtacando", false)
    }
}
