using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Transform attackPoint; 
    public float attackRange = 0.5f;
    public string enemyTag = "Nemico";
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.5f;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.J))
            Attack();
    }

    void Attack()
    {
        // Trova tutti gli oggetti con il tag specificato
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Controlla se l'oggetto ha il tag nemico
            if (enemy.CompareTag(enemyTag))
            {
                Debug.Log("We hit " + enemy.name);
                
                // Esegui altre azioni sull'oggetto nemico
                Nemico nemico = enemy.GetComponent<Nemico>();
                if (nemico != null)
                {
                    nemico.KBCounter = knockbackDuration;
                    nemico.KnockFromRight = enemy.transform.position.x < transform.position.x;
                    nemico.ApplyKnockback(knockbackForce);
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }
}
