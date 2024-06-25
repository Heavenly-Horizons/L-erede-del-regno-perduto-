using System.Collections;
using UnityEngine;

public class Nemico_Attack : MonoBehaviour
{
<<<<<<< HEAD
    public int damage;
    [SerializeField] private GameObject player;
    private PlayerStats playerStats;
    public PlayerMovement playerMovement;
    
=======
    public float damage = 15f;
    public float attackCooldown = 1.0f;
    private float cooldownTimer = 0f;
    [SerializeField] private GameObject player;
    private PlayerStats playerStats;
    public PlayerMovement playerMovement;

    private Animator animator;
    private bool isAttacking = false;
    private bool playerInRange = false;

>>>>>>> main
    private void Awake()
    {
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
            playerMovement = player.GetComponent<PlayerMovement>();
        }
<<<<<<< HEAD
=======
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (playerInRange && !isAttacking && cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            isAttacking = true;
            animator.SetTrigger("enemyAttack");
        }
>>>>>>> main
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
<<<<<<< HEAD
        if (collision.gameObject.CompareTag("Player") && playerStats != null && playerMovement != null)
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = collision.transform.position.x <= transform.position.x;
=======
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    // This function should be called by the animation event
    public void InflictDamage()
    {
        if (playerInRange && playerStats != null && playerMovement != null)
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = player.transform.position.x <= transform.position.x;
>>>>>>> main
            
            playerStats.TakeDamage(damage);

            // Stop enemy movement for 0.5 seconds
            Nemico nemico = GetComponent<Nemico>();
            if (nemico != null)
            {
<<<<<<< HEAD
                StartCoroutine(nemico.StopMovement(0.5f));
            }
        }
=======
                StartCoroutine(nemico.StopMovement(0.3f));
            }
        }

        isAttacking = false; // Reset attacking state
>>>>>>> main
    }
}
