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
        if(playerMovement != null){
            Debug.Log("PlayerMovement non è nullo");
        }
        else{
            Debug.Log("PlayerMovement è nullo");
        }
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();

         if(playerStats != null){
            Debug.Log("PlayerStats non è nullo");
        }
        else{
            Debug.Log("PlayerStats è nullo");
        }
    }

    public void Attack() {
        Vector3 pos = attackPoint.position;
        pos += attackPoint.right * attackOffset.x;
        pos += attackPoint.up * attackOffset.y;

        Collider2D colliderInfo = Physics2D.OverlapCircle(pos, attackRange);

        if (colliderInfo != null && colliderInfo.CompareTag("Player")) {
            playerStats.TakeDamage(attackDamage);
            Debug.Log("il player prende danno");
            
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = playerMovement.transform.position.x <= transform.position.x;
        }
        else{
            Debug.Log("Il player non funziona correttamente");
        }
    }
}
