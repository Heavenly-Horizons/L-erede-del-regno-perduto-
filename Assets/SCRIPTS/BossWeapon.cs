using UnityEngine;

public class BossWeapon : MonoBehaviour {
    public Transform attackPoint;
    public float attackDamage = 15f;
    public float attackRange;
    public Vector3 attackOffset;
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;
    

    private void OnDrawGizmosSelected() {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    void Start(){
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    public void Attack() {
        Vector3 pos = attackPoint.position;
        pos += attackPoint.right * attackOffset.x;
        pos += attackPoint.up * attackOffset.y;

        Collider2D colliderInfo = Physics2D.OverlapCircle(pos, attackRange);

        if (colliderInfo != null && colliderInfo.CompareTag("Player")) {
            playerStats.TakeDamage(attackDamage);
            
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = playerMovement.transform.position.x <= transform.position.x;
        }
    }
}
