using UnityEngine;

public class BossWeapon : MonoBehaviour {
    public Transform attackPoint;
    public float attackDamage = 15f;
    public float attackRange;
    public Vector3 attackOffset;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Transform playerTransform;

    private void OnDrawGizmosSelected() {
        if (attackPoint == null) return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Attack() {
        Vector3 pos = attackPoint.position;
        pos += attackPoint.right * attackOffset.x;
        pos += attackPoint.up * attackOffset.y;

        Collider2D colliderInfo = Physics2D.OverlapCircle(pos, attackRange);

        if (colliderInfo != null && colliderInfo.CompareTag("Player")) {
            playerStats.TakeDamage(attackDamage);
            Debug.Log("Player colpito");

            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = playerTransform.position.x <= transform.position.x;
        }
    }
}