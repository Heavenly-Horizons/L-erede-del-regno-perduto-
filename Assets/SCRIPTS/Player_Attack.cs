using UnityEngine;

public class Player_Attack : MonoBehaviour {
    public Transform attackPoint;
    public float attackRange = 5f;
    public float attackCooldown = 1f; // L'animazione dell'attacco dovrebbe durare circa .5 secondi, mo vedo che succede
    public float cooldownTimer;

    public string enemyTag = "Nemico";
    public float knockbackForce = 40f;
    public float knockbackDuration = 0.5f;
    public float staminaRecoveryCD;
    public bool staminaRecovered;
    private Animator anim;
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;

    private void Awake() {
        //anim
        anim = GetComponent<Animator>();
        Debug.Log(anim != null
            ? "GetComponent<Animator>() in Player_Attack istanziato"
            : "GetComponent<Animator>() in Player_Attack non istanziato");

        //playerMovement
        playerMovement = GetComponent<PlayerMovement>();
        Debug.Log(playerMovement != null
            ? "GetComponent<PlayerMovement>() in Player_Attack istanziato"
            : "GetComponent<PlayerMovement>() in Player_Attack non istanziato");

        //playerStats
        playerStats = GetComponent<PlayerStats>();
        Debug.Log(playerStats != null
            ? "GetComponent<PlayerStats>() in Player_Attack istanziato"
            : "GetComponent<PlayerStats>() in Player_Attack non istanziato");

        cooldownTimer = attackCooldown;
        staminaRecoveryCD = playerStats.staminaRecoveryCooldown;
    }

    private void Update() {
        // Faccio partire il timer 
        if (staminaRecoveryCD > 0) staminaRecoveryCD -= Time.deltaTime;
        // Controllo il timer
        // se il timer arriva a zero allora parte il recupero della stamina 
        // in update e na volta che arriva al massimo oppure attacca il recupero termina

        // Verifica se il pulsante del mouse è stato premuto esattamente in questo frame
        if (Input.GetKeyDown(KeyCode.J) && cooldownTimer >= attackCooldown && playerMovement.CanAttack() &&
            playerStats.GetPlayerCurrentStamina() >= 10) {
            // Ottieni lo stato corrente di "Run" dall'Animator
            bool isRunning = anim.GetBool("Run");

            // Avvia l'animazione di attacco in base allo stato di "Run"
            if (isRunning) {
                anim.SetTrigger("attackRunning");
                staminaRecoveryCD = playerStats.staminaRecoveryCooldown;
            }
            else {
                anim.SetTrigger("attack");
                staminaRecoveryCD = playerStats.staminaRecoveryCooldown;
            }

            cooldownTimer = 0;
        }

        if (staminaRecoveryCD <= 0 && !staminaRecovered) {
            staminaRecovered = playerStats.RecoverStamina();
        }
        else if (staminaRecovered) {
            staminaRecoveryCD = playerStats.staminaRecoveryCooldown;
            staminaRecovered = playerStats.RecoverStamina();
        }

        //dovrebbe andare, mo testiamo
        cooldownTimer += Time.deltaTime;
    }


    // AchilleStun achilleStun = enemy.GetComponent<AchilleStun>();
    //                 if(achilleStun != null){
    //                     Debug.Log("Chiamando la funzione OnPlayerHit");
    //                     achilleStun.OnPlayerHit();
    //                 }

    public void OnDrawGizmosSelected() {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void Attack() {
        // Trova tutti gli oggetti con il tag specificato
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in hitEnemies)
            if (enemy.CompareTag(enemyTag)) {
                Debug.Log("We hit " + enemy.name);

                var nemico = enemy.GetComponent<Nemico>();
                Debug.Log(nemico != null
                    ? "enemy.GetComponent<Nemico>() in Player_Attack istanziato"
                    : "enemy.GetComponent<Nemico>() in Player_Attack non istanziato");

                var nemico2 = enemy.GetComponent<EnemyArcher>();
                Debug.Log(nemico2 != null
                    ? "enemy.GetComponent<EnemyArcher>() in Player_Attack istanziato"
                    : "enemy.GetComponent<EnemyArcher>() in Player_Attack non istanziato");

                var bossHealth = enemy.GetComponent<BossHealth>();
                Debug.Log(bossHealth != null
                    ? "enemy.GetComponent<BossHealth>() in Player_Attack istanziato"
                    : "enemy.GetComponent<BossHealth>() in Player_Attack non istanziato");

                if (nemico != null) {
                    nemico.setHit(true);
                    nemico.KBCounter = knockbackDuration;
                    nemico.KnockFromRight = enemy.transform.position.x < transform.position.x;
                    nemico.ApplyKnockback(knockbackForce);
                    nemico.TakeDamage(playerStats.playerDamage);
                }

                if (nemico2 != null) {
                    nemico2.SetHit(true);
                    nemico2.knockbackCounter = knockbackDuration;
                    nemico2.knockFromRight = enemy.transform.position.x < transform.position.x;
                    nemico2.ApplyKnockback(knockbackForce);
                    nemico2.TakeDamage(playerStats.playerDamage);
                }

                if (bossHealth != null) {
                    bossHealth.isHit = true;
                    bossHealth.TakeDamage(playerStats.playerDamage);
                }
            }

        playerStats.UseStamina(10);
    }
}