using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyArcher : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private Transform playerTransform;

    public float patrolSpeed = 300f;
    public bool startDirectionLeft = true;
    public float patrolDistance = 30f;
    public float attackRange = 50f;
    public GameObject arrowPrefab;
    public Transform launchPoint;
    public float arrowSpeed = 20f;
    public float arrowCooldown = 2f;

    public float KBForce = 40f;
    public float knockbackDuration = 0.2f;
    public float knockbackCounter;
    
    public bool knockFromRight;

    public float nemicoHealth = 30f;
    public Slider nemicoHealthBar;
    public GameObject player;

    private DropCoin dropCoin;
    private DropHeal dropHeal;
    private Vector3 initialPosition;

    private bool isAttacking;
    private bool isFacingRight = true;
    private bool isHit;
    private bool isPatrollingLeft = true;
    private bool isShooting;
    private bool isStopped;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        playerTransform = player.transform;

        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        isPatrollingLeft = !startDirectionLeft;
        initialPosition = transform.position;

        if (nemicoHealthBar != null)
        {
            nemicoHealthBar.maxValue = nemicoHealth;
        }

        dropCoin = GetComponent<DropCoin>();
        dropHeal = GetComponent<DropHeal>();
    }

    void Update()
    {
        if (isStopped)
            return;

        if (knockbackCounter > 0)
        {
            Vector2 knockbackDirection = knockFromRight ? Vector2.left : Vector2.right;
            rb.velocity = new Vector2(knockbackDirection.x * KBForce, KBForce / 3);
            knockbackCounter -= Time.deltaTime;
        }
        else
        {
            float playerDistance = Vector3.Distance(transform.position, playerTransform.position);
            if (playerDistance <= attackRange)
            {
                rb.velocity = Vector2.zero;
                isAttacking = true;

                if ((playerTransform.position.x > transform.position.x && !isFacingRight) ||
                    (playerTransform.position.x < transform.position.x && isFacingRight))
                    Flip();

                if (!isShooting) StartCoroutine(ShootArrowWithCooldown());
                animator.SetTrigger("Attack");
            }
            else
            {
                isAttacking = false;
                animator.ResetTrigger("Attack");
                Patrol();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NoJump"))
        {
            if (Vector3.Distance(initialPosition, transform.position) >= patrolDistance)
                ChangeDirection();
            else
                rb.velocity = new Vector2(rb.velocity.x, 35f);
        }

        if (collision.gameObject.CompareTag("Nemico"))
        {
            ChangeDirection();
        }
    }

    private void Patrol()
    {
        animator.SetTrigger("Run");
        int directionMultiplier = isPatrollingLeft ? -1 : 1;
        rb.velocity = new Vector2(patrolSpeed * Time.deltaTime * directionMultiplier, rb.velocity.y);

        if ((directionMultiplier > 0 && !isFacingRight) || (directionMultiplier < 0 && isFacingRight))
            Flip();

        if (Vector3.Distance(initialPosition, transform.position) >= patrolDistance)
            ChangeDirection();
    }

    private void ChangeDirection()
    {
        isPatrollingLeft = !isPatrollingLeft;
        transform.Rotate(0f, 180f, 0f);
        initialPosition = transform.position;
        isFacingRight = !isFacingRight;
    }

    private void Flip()
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

    private IEnumerator ShootArrowWithCooldown()
    {
        isShooting = true;
        yield return new WaitForSeconds(arrowCooldown);
        isShooting = false;
    }

    public void ShootArrow()
    {
        if (isAttacking)
        {
            if (arrowPrefab != null && launchPoint != null)
            {
                Vector2 arrowDirection = (playerTransform.position - launchPoint.position).normalized;
                GameObject arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity);

                bool playerToLeft = playerTransform.position.x < transform.position.x;

                if (playerToLeft)
                {
                    Vector3 scale = arrow.transform.localScale;
                    scale.x = Mathf.Abs(scale.x) * -1;
                    arrow.transform.localScale = scale;
                }
                else
                {
                    Vector3 scale = arrow.transform.localScale;
                    scale.x = Mathf.Abs(scale.x);
                    arrow.transform.localScale = scale;
                }

                arrow.GetComponent<Rigidbody2D>().velocity = arrowDirection * arrowSpeed;
                Physics2D.IgnoreCollision(arrow.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            }
        }
    }

    public void TakeDamage(float amount)
    {
        if (isHit)
        {
            animator.SetTrigger("Hurt");

            Transform childTransform = transform.GetChild(0);
            if (childTransform != null && childTransform.childCount > 0)
            {
                GameObject childObject = childTransform.GetChild(0).gameObject;
                if (childObject != null)
                {
                    childObject.SetActive(true);
                }
            }
        }

        if (nemicoHealth - amount > 0)
        {
            nemicoHealth -= amount;
            nemicoHealthBar.value = nemicoHealth;
        }
        else
        {
            nemicoHealth = 0;
            nemicoHealthBar.value = nemicoHealth;

            if (dropCoin != null && dropHeal != null)
            {
                dropCoin.Drop(1);
                dropHeal.Drop(1);
            }

            animator.SetTrigger("Die");

            if (boxCollider2D != null)
            {
                boxCollider2D.enabled = false;
                Debug.Log("BoxCollider2D disattivato");
            }
            else
            {
                Debug.LogError("BoxCollider2D non trovato su questo GameObject");
            }

            StartCoroutine(PlayDeathAnimation());
        }
    }

    private IEnumerator PlayDeathAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void SetHit(bool value)
    {
        isHit = value;
    }
}
