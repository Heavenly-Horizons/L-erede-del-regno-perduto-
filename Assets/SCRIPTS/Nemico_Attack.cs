using UnityEngine;

public class Nemico_Attack : MonoBehaviour {
    private static readonly int EnemyAttack = Animator.StringToHash("enemyAttack");
    public float damage = 15f;
    public float attackCooldown = 1.0f;
    [SerializeField] private GameObject player;
    public PlayerMovement playerMovement;

    private Animator animator;
    private float cooldownTimer;
    private bool isAttacking;

    private Animator playerAnimator;
    private bool playerInRange;
    private PlayerStats playerStats;

    void Start() {
        if (player != null) {
            //playerStats
            playerStats = player.GetComponent<PlayerStats>();
            Debug.Log(playerStats != null
                ? "player.GetComponent<PlayerStats>() in Nemico_Attack istanziato"
                : "player.GetComponent<PlayerStats>() in Nemico_Attack non istanziato");

            //playerMovement
            playerMovement = player.GetComponent<PlayerMovement>();
            Debug.Log(playerMovement != null
                ? "player.GetComponent<PlayerMovement>() in Nemico_Attack istanziato"
                : "player.GetComponent<PlayerMovement>() in Nemico_Attack non istanziato");

            //playerAnimator
            playerAnimator = player.GetComponent<Animator>();
            Debug.Log(playerAnimator != null
                ? "player.GetComponent<Animator>() in Nemico_Attack istanziato"
                : "player.GetComponent<Animator>() in Nemico_Attack non istanziato");
        }

        //animator
        animator = GetComponent<Animator>();
        Debug.Log(animator != null
            ? "GetComponent<Animator>() in Nemico_Attack istanziato"
            : "GetComponent<Animator>() in Nemico_Attack non istanziato");
    }

    private void Update() {
        cooldownTimer += Time.deltaTime;

        if (playerInRange && !isAttacking && cooldownTimer >= attackCooldown) {
            cooldownTimer = 0;
            isAttacking = true;
            animator.SetTrigger(EnemyAttack);
            isAttacking = false;
        }

        if (playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("die") ||
            playerAnimator.GetCurrentAnimatorStateInfo(0).IsName("Death")) cooldownTimer = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) playerInRange = true;
    }

    private void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) playerInRange = false;
    }

    // This function should be called by the animation event
    public void InflictDamage() {
        if (playerInRange && playerStats != null && playerMovement != null) {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = player.transform.position.x <= transform.position.x;

            playerStats.TakeDamage(damage);

            // Stop enemy movement for 0.5 seconds
            var nemico = GetComponent<Nemico>();
            Debug.Log(nemico != null
                ? "GetComponent<Nemico>() in Nemico_Attack istanziato"
                : "GetComponent<Nemico>() in Nemico_Attack non istanziato");
            if (nemico != null) StartCoroutine(nemico.StopMovement(0.3f));
        }

        isAttacking = false; // Reset attacking state
    }
}
