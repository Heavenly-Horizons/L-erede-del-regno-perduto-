using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyArcher : MonoBehaviour
{
    public float patrolSpeed = 300f;
    public bool startDirectionLeft = true;
    public float patrolDistance = 30f;
    public float attackRange = 50f;
    public Transform player;
    public GameObject arrowPrefab;
    public Transform launchPoint;
    public float arrowSpeed = 20f;

    private bool isDead = false; 
    public float arrowCooldown = 2f;  // Tempo di cooldown tra i lanci
    
    public Animator animator;  // Aggiungi un riferimento all'Animator

    private bool isAttacking = false;  // Variabile per tenere traccia se il nemico è in attacco

    private bool isPatrollingLeft;
    private Rigidbody2D rb;
    private Vector3 initialPosition;
    private bool isFacingRight = true;
    private bool isStopped = false;
    private bool isShooting = false;

    // Knockback fields
    public float KBForce = 40f;
    public float knockbackDuration = 0.2f;
    public float knockbackCounter;
    public bool knockFromRight;

    public float nemicoHealth = 30f;
    public Slider nemicoHealthBar;

    private bool isHit = false;

    private DropCoin dropCoin;
    private DropHeal dropHeal;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on " + gameObject.name);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        isPatrollingLeft = !startDirectionLeft;
        initialPosition = transform.position;

        if(nemicoHealthBar != null){
            nemicoHealthBar.maxValue = nemicoHealth;
        }

        if (player == null)
        {
            Debug.LogError("Player not assigned to enemy " + gameObject.name);
        }

          if (animator == null)
        {
            Debug.LogError("Animator not assigned to enemy " + gameObject.name);
        }

        dropCoin = GetComponent<DropCoin>();
        dropHeal = GetComponent<DropHeal>();
    }

    void FixedUpdate()
    {
         if (isStopped || isDead) return; 

        if (knockbackCounter > 0)
        {
            Vector2 knockbackDirection = knockFromRight ? Vector2.left : Vector2.right;
            rb.velocity = new Vector2(knockbackDirection.x * KBForce, KBForce / 3);
            knockbackCounter -= Time.deltaTime;
        }
        else
        {
            float playerDistance = Vector3.Distance(transform.position, player.position);
            if (playerDistance <= attackRange)
            {
                rb.velocity = Vector2.zero;
                isAttacking = true;

                // Guarda verso il giocatore
                if ((player.position.x > transform.position.x && !isFacingRight) ||
                    (player.position.x < transform.position.x && isFacingRight))
                {
                    Flip();
                }

                if (!isShooting)
                {
                    StartCoroutine(ShootArrowWithCooldown());
                }
                animator.SetTrigger("Attack");
            }
            else
            {   isAttacking = false;
                animator.ResetTrigger("Attack");
                Patrol();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NoJump"))
        {
            if (Vector3.Distance(initialPosition, transform.position) >= patrolDistance )
            {
                ChangeDirection();
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 35f);
            }
        }

        if (collision.gameObject.CompareTag("Nemico")){
            ChangeDirection();
        }
        
    }

    void Patrol()
    {   
        animator.SetTrigger("Run");
        int directionMultiplier = isPatrollingLeft ? -1 : 1;
        rb.velocity = new Vector2(patrolSpeed * Time.deltaTime * directionMultiplier, rb.velocity.y);

        if ((directionMultiplier > 0 && !isFacingRight) || (directionMultiplier < 0 && isFacingRight))
        {
            Flip();
        }

        if (Vector3.Distance(initialPosition, transform.position) >= patrolDistance)
        {
            ChangeDirection();
        }
    }

    void ChangeDirection()
    {
        isPatrollingLeft = !isPatrollingLeft;
        transform.Rotate(0f, 180f, 0f);
        initialPosition = transform.position;
        isFacingRight = !isFacingRight;
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public IEnumerator StopMovement(float duration)
    {
        isStopped = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }

    public void ApplyKnockback(float force)
    {
        KBForce = force;
    }

    IEnumerator ShootArrowWithCooldown()
    {
        isShooting = true;
        yield return new WaitForSeconds(arrowCooldown);
        isShooting = false;
    }

    public void ShootArrow()
    {
        if (isAttacking) // Controlla se il nemico è in attacco
     {
        if (arrowPrefab != null && launchPoint != null)
        {
            Debug.Log("Shooting arrow");

            // Calcola la direzione della freccia
            Vector2 arrowDirection = (player.position - launchPoint.position).normalized;

            // Istanzia la freccia nel launchPoint con la direzione e rotazione corrette
            GameObject arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity);

            // Determina se il giocatore è alla sinistra del nemico
            bool playerToLeft = player.position.x < transform.position.x;

            // Flippa la freccia se il giocatore è alla sinistra del nemico
            if (playerToLeft)
            {
                Vector3 scale = arrow.transform.localScale;
                scale.x = Mathf.Abs(scale.x) * -1; // Assicura che la scala sia negativa per flipparla
                arrow.transform.localScale = scale;
            }
            else
            {
                Vector3 scale = arrow.transform.localScale;
                scale.x = Mathf.Abs(scale.x); // Assicura che la scala sia positiva
                arrow.transform.localScale = scale;
            }

            // Applica la velocità alla freccia
            arrow.GetComponent<Rigidbody2D>().velocity = arrowDirection * arrowSpeed;

            // Ignora le collisioni tra la freccia e l'arciere
            Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }
        else
        {
            Debug.LogError("ArrowPrefab or LaunchPoint is not assigned.");
        }
     }
    }

    public void TakeDamage(float amount)
    {
        if (isDead) return;
        if (isHit)
        {    animator.SetTrigger("hurt");
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        if (nemicoHealth - amount > 0)
        {
            nemicoHealth -= amount;
            nemicoHealthBar.value = Mathf.Floor(amount);
            Debug.Log("vita nemico arciere: " + nemicoHealthBar.value);
        }
        else
        {   animator.SetTrigger("die");
            nemicoHealth = 0;
            nemicoHealthBar.value = nemicoHealth;
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

            if (dropCoin != null && dropHeal != null)
            {
                dropCoin.Drop(1);
                dropHeal.Drop(1);
            }
            //Die
            Destroy(gameObject);
        }
    }

    public void setHit(bool value){
        isHit = value;
    }
}
