using UnityEngine;

public class AttackPlayer : MonoBehaviour {
    [SerializeField] private float attackCooldown;
    private Animator anim;

    private float cooldownTimer = Mathf.Infinity;
    private PlayerMovement playerMovement;

   private void Start() {
    // Controllo per il componente Animator
    anim = GetComponent<Animator>();
    if (anim == null) {
        Debug.LogWarning("Componente Animator non trovato sul GameObject.");
    }

    // Controllo per il componente PlayerMovement
    playerMovement = GetComponent<PlayerMovement>();
    if (playerMovement == null) {
        Debug.LogWarning("Componente PlayerMovement non trovato sul GameObject.");
    }
}


    void Update() {
        // Verifica se il pulsante del mouse è stato premuto esattamente in questo frame
        if (Input.GetKeyDown(KeyCode.J) && cooldownTimer > attackCooldown && playerMovement.CanAttack()) {
            // Ottieni lo stato corrente di "Run" dall'Animator
            bool isRunning = anim.GetBool("Run");

            // Avvia l'animazione di attacco in base allo stato di "Run"
            if (isRunning)
                anim.SetTrigger("attackRunning");
            else
                anim.SetTrigger("attack");

            cooldownTimer = 0;
        }

        cooldownTimer += Time.deltaTime;
    }
}
