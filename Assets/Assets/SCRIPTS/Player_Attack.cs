using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public Transform attackPoint; 
    public float attackRange = 5f;
    private Animator anim;
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;
    public float attackCooldown = 1f; // L'animazione dell'attacco dovrebbe durare circa .5 secondi, mo vedo che succede
    public float cooldownTimer = 0;

    public string enemyTag = "Nemico";
    public float knockbackForce = 40f;
    public float knockbackDuration = 0.5f;
    public float staminaRecoveryCD;
    public bool staminaRecovered = false;

    void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>();
        staminaRecoveryCD = playerStats.staminaRecoveryCooldown;

        if (anim == null)
        {
            Debug.LogError("Animator component is missing on " + gameObject.name);
        }

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement component is missing on " + gameObject.name);
        }
    }

    void FixedUpdate()
    {
        // Faccio partire il timer 
        if(staminaRecoveryCD > 0){
            staminaRecoveryCD -= Time.deltaTime;
        }
        // Controllo il timer
        // se il timer arriva a zero allora parte il recupero della stamina 
        // in update e na volta che arriva al massimo oppure attacca il recupero termina

        // Verifica se il pulsante del mouse è stato premuto esattamente in questo frame
        if (Input.GetKeyDown(KeyCode.J) && cooldownTimer >= attackCooldown && playerMovement.canAttack() && playerStats.GetPlayerCurrentStamina() >= 10)
        {
            // Ottieni lo stato corrente di "Run" dall'Animator
            bool isRunning = anim.GetBool("Run");

            // Avvia l'animazione di attacco in base allo stato di "Run"
            if (isRunning){
                anim.SetTrigger("attackRunning");
                Attack();
                staminaRecoveryCD = playerStats.staminaRecoveryCooldown;
            }else{
                anim.SetTrigger("attack");
                Attack();
                staminaRecoveryCD = playerStats.staminaRecoveryCooldown;
            }

            cooldownTimer = 0;
        }
        if(staminaRecoveryCD <= 0 && !staminaRecovered){
            staminaRecovered = playerStats.RecoverStamina();
        }else if(staminaRecovered){
            staminaRecoveryCD = playerStats.staminaRecoveryCooldown;
            staminaRecovered = playerStats.RecoverStamina();
        }

        //dovrebbe andare, mo testiamo

        cooldownTimer += Time.deltaTime;
    }

   void Attack()
    {
        // Trova tutti gli oggetti con il tag specificato
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag(enemyTag))
            {
                Debug.Log("We hit " + enemy.name);
                
                Nemico nemico = enemy.GetComponent<Nemico>();
                EnemyArcher nemico2 = enemy.GetComponent<EnemyArcher>();
                BossHealth bossHealth = enemy.GetComponent<BossHealth>();

                if (nemico != null)
                {
                    nemico.setHit(true);
                    nemico.KBCounter = knockbackDuration;
                    nemico.KnockFromRight = enemy.transform.position.x < transform.position.x;
                    nemico.ApplyKnockback(knockbackForce);
                    nemico.TakeDamage(playerStats.playerDamage);
                }
                if (nemico2 != null)
                {
                    nemico2.setHit(true);
                    nemico2.knockbackCounter = knockbackDuration;
                    nemico2.knockFromRight = enemy.transform.position.x < transform.position.x;
                    nemico2.ApplyKnockback(knockbackForce);
                    nemico2.TakeDamage(playerStats.playerDamage);
                }
                if(bossHealth != null){
                    bossHealth.TakeDamage(playerStats.playerDamage);
                }
            }
        }
        playerStats.UseStamina(10);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange); 
    }
}
