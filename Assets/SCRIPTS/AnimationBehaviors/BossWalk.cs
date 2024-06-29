using UnityEngine;

public class BossWalk : StateMachineBehaviour {
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int PlayerDead = Animator.StringToHash("PlayerDead");
    public float speed = 2.5f;
    public float attackRange;
    public float initialBossY;
    public int rangeMultiplier;
    public Vector2 target;
    public bool isEnabled = true;
    private Boss boss;
    private Transform bossTransform;
    private BossWeapon bossWeapon;
    private PlayerStats playerStats;
    private GameObject player;
    private Transform playerTransform;
    private Rigidbody2D rb;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        player = GameObject.Find("Player");
        playerStats = player.GetComponent<PlayerStats>();
        playerTransform = player.GetComponent<Transform>();
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        bossWeapon = animator.GetComponent<BossWeapon>();
        bossTransform = animator.GetComponent<Transform>();
        initialBossY = bossTransform.position.y;
        attackRange = bossWeapon.attackRange * rangeMultiplier;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.ResetTrigger(Attack);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (!isEnabled) {
            attackRange = -1;
            return;
        }

        boss.LookAtPlayer();

        // Blocca la rotazione sull'asse Z impostando la rotazione iniziale
        Vector3 eulerRotation = bossTransform.rotation.eulerAngles;
        eulerRotation.z = 0;
        bossTransform.rotation = Quaternion.Euler(eulerRotation);

        target = new(playerTransform.position.x, initialBossY);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);

        if (!playerStats.isPlayerDead) {
            rb.MovePosition(newPos);
            if (Vector2.Distance(playerTransform.position, rb.position) <= attackRange + 1) animator.SetTrigger(Attack);
        }
        else {
            animator.SetBool(PlayerDead, true);
        }
    }
}
