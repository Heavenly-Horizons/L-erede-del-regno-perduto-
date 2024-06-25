<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
=======
>>>>>>> main
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Transform attackPoint; 
<<<<<<< HEAD
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
=======
    public float attackRange = 5f;
    private Animator anim;
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;

    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;

    public string enemyTag = "Nemico";
    public float knockbackForce = 40f;
    public float knockbackDuration = 0.5f;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>();

        if (anim == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component is missing on " + gameObject.name);
        }
    }

    private void Update()
    {
        // Verifica se il pulsante del mouse è stato premuto esattamente in questo frame
        if (Input.GetKeyDown(KeyCode.J) && cooldownTimer > attackCooldown && playerMovement.canAttack())
        {
            // Ottieni lo stato corrente di "Run" dall'Animator
            bool isRunning = anim.GetBool("Run");

            // Avvia l'animazione di attacco in base allo stato di "Run"
            if (isRunning)
            {
                anim.SetTrigger("attackRunning");
            }
            else
            {
                anim.SetTrigger("attack");
            }

            cooldownTimer = 0;
        }

        cooldownTimer += Time.deltaTime;
    }

   void Attack()
>>>>>>> main
    {
        // Trova tutti gli oggetti con il tag specificato
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
<<<<<<< HEAD
            // Controlla se l'oggetto ha il tag nemico
=======
>>>>>>> main
            if (enemy.CompareTag(enemyTag))
            {
                Debug.Log("We hit " + enemy.name);
                
<<<<<<< HEAD
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
=======
                Nemico nemico = enemy.GetComponent<Nemico>();
                if (nemico != null)
                {
                    nemico.setHit(true);
                    nemico.KBCounter = knockbackDuration;
                    nemico.KnockFromRight = enemy.transform.position.x < transform.position.x;
                    nemico.ApplyKnockback(knockbackForce);
                    nemico.TakeDamage(playerStats.playerDamage);
                }

                EnemyArcher nemico2 = enemy.GetComponent<EnemyArcher>();
                if (nemico2 != null)
                {
                    nemico2.setHit(true);
                    nemico2.knockbackCounter = knockbackDuration;
                    nemico2.knockFromRight = enemy.transform.position.x < transform.position.x;
                    nemico2.ApplyKnockback(knockbackForce);
                    nemico2.TakeDamage(playerStats.playerDamage);
                }
            }
        }
        
>>>>>>> main
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }
}
