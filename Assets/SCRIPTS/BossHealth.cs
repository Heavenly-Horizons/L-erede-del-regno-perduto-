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

    void Start(){
        animator = gameObject.GetComponent<Animator>();
        stun = gameObject.GetComponent<AchilleStun>();
        if(stun != null){
            Debug.Log("Il componente Stun è presente");
        }
        bossWalk = gameObject.GetComponent<Animator>().GetBehaviour<BossWalk>();
        if(bossWalk != null){
            Debug.Log("Il componente BossWalk è presente");
        }
        if (bossHealthSlider != null)
        {
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
                bossWalk.rangeMultiplier = 0;
                gameObject.GetComponent<DropHeal>().Drop(4);
                gameObject.GetComponent<DropCoin>().Drop(3);
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
