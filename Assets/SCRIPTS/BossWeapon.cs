using UnityEngine;

public class BossWeapon : MonoBehaviour
{
    public Transform attackPoint;
    public float attackDamage = 15f;
    public float attackRange = 4f;
    public Vector3 attackOffset;
    private PlayerStats playerStats;
    private PlayerMovement playerMovement;

    public void Attack(){
        Vector3 pos = attackPoint.position;
        pos += attackPoint.right * attackOffset.x;
        pos += attackPoint.up * attackOffset.y;

        Collider2D colliderInfo = Physics2D.OverlapCircle(pos, attackRange);

        if(colliderInfo != null && colliderInfo.CompareTag("Player")){
            playerStats = colliderInfo.GetComponent<PlayerStats>();
            playerMovement = colliderInfo.GetComponent<PlayerMovement>();
            playerStats.TakeDamage(attackDamage);

            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = GameObject.FindGameObjectWithTag("Player").transform.position.x <= transform.position.x;
           // StartCoroutine(gameObject.GetComponent<Boss>().StopMovement(1f));
        }
    }

    void OnDrawGizmosSelected()
    {
        if(attackPoint == null){ return; }

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
