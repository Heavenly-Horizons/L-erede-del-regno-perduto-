using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private AchilleStun stun;
    private BossWalk bossWalk;
    private Animator animator;
    public Slider bossHealthSlider;
    public float bossHealth;
    public float bossDefence = 0;
    public float damageCounter;
    public bool isInvulnerable = false;
    public bool isDead = false;
    public bool isHit = false;

   void Start() {
    // Controllo per il componente Animator
    Animator anim = GetComponent<Animator>();
    if (anim == null) {
        Debug.LogWarning("Componente Animator non trovato sul GameObject.");
    }

    // Controllo per il componente AchilleStun
    AchilleStun stun = GetComponent<AchilleStun>();
    if (stun == null) {
        Debug.LogWarning("Componente AchilleStun non trovato sul GameObject.");
    }

    // Controllo per il componente BossWalk ottenuto dall'Animator
    BossWalk bossWalk = anim.GetBehaviour<BossWalk>();
    if (bossWalk == null) {
        Debug.LogWarning("Componente BossWalk non trovato nell'Animator.");
    }

    // Controllo per il componente bossHealthSlider, se presente
    if (bossHealthSlider != null) {
        Debug.LogWarning("Componente BossHealtSlider trovato ");
        bossHealthSlider.maxValue = bossHealth;
        bossHealthSlider.value = bossHealth;
    }
}

    public void TakeDamage(float amount){

        if(bossHealthSlider != null){
            if(isInvulnerable){ 
                StartCoroutine(BubbleShow());
                return; 
            }
            if (bossHealth - (amount - bossDefence) > 0){
                animator.SetTrigger("Hurt");
                bossHealth -= amount - bossDefence;
                bossHealthSlider.value = bossHealth;
                damageCounter += amount - bossDefence;
                isHit = true;
                animator.ResetTrigger("Hurt");
            }else{
                isDead = true;
                bossHealth = 0;
                bossHealthSlider.value = bossHealth;
                damageCounter = 0;
                bossWalk.isEnabled = false;
               DropHeal dropHealComponent = gameObject.GetComponent<DropHeal>();
                if (dropHealComponent == null) {
                    Debug.LogWarning("Componente DropHeal non trovato sul GameObject.");
                } else {
                    dropHealComponent.Drop(4);
                }
                DropCoin dropCoinComponent = gameObject.GetComponent<DropCoin>();
            if (dropCoinComponent == null) {
                Debug.LogWarning("Componente DropCoin non trovato sul GameObject.");
            } else {
                dropCoinComponent.Drop(3);
            }


            }
            if(stun != null){
                stun.Stun();
                // Reset isHit after checking it during stun
                StartCoroutine(ResetHitFlag());
            }
        }
    }

    private IEnumerator ResetHitFlag()
    {
        yield return null; // Wait for one frame
        isHit = false;
    }

    IEnumerator BubbleShow(){
        animator.SetTrigger("BubbleShow");
        yield return new WaitForSeconds(2f);
        animator.ResetTrigger("BubbleShow");
    }
}
