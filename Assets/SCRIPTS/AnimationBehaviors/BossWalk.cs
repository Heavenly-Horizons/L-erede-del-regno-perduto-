using Script.Dialogue.SceneManager.AchilleBF___ToEnemy;
using UnityEngine;

public class BossWalk : StateMachineBehaviour
{
    public float speed = 2.5f;
    public float attackRange;
    public float initialBossY;
    public int rangeMultiplier;
    public Vector2 target;
    public bool isEnabled = true;
    private Boss boss;
    private BossWeapon bossWeapon;
    private Transform bossTransform;
    private Transform player;
    private PlayerStats playerStats;
    private Rigidbody2D rb;

    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<Boss>();
        bossWeapon = animator.GetComponent<BossWeapon>();
        bossTransform = animator.GetComponent<Transform>();
        initialBossY = bossTransform.position.y;
        attackRange = bossWeapon.attackRange * rangeMultiplier;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Attack");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!isEnabled){ 
            attackRange = -1;
            return; 
        }

        boss.LookAtPlayer();

        // Blocca la rotazione sull'asse Z impostando la rotazione iniziale
        var eulerRotation = bossTransform.rotation.eulerAngles;
        eulerRotation.z = 0;
        bossTransform.rotation = Quaternion.Euler(eulerRotation);

        target = new Vector2(player.position.x, initialBossY);
        var newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        
        if(!playerStats.isPlayerDead){
            rb.MovePosition(newPos);
            if (Vector2.Distance(player.position, rb.position) <= attackRange + 1){
                animator.SetTrigger("Attack");
            }
        }else{
            animator.SetBool("PlayerDead", true);
        }
    }
}
