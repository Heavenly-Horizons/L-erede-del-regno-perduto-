using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private AchilleStun stun;
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
                animator.ResetTrigger("Hurt");
            }else{
                isDead = true;
                bossHealth = 0;
                bossHealthSlider.value = bossHealth;
                damageCounter = 0;
                gameObject.GetComponent<Animator>().SetTrigger("Defeat");
                gameObject.GetComponent<DropHeal>().Drop(4);
                gameObject.GetComponent<DropCoin>().Drop(3);
            }
            if(stun != null){
                stun.Stun();
            }
        }
    }

    IEnumerator BubbleShow(){
        animator.SetTrigger("BubbleShow");
        yield return new WaitForSeconds(2f);
        animator.ResetTrigger("BubbleShow");
    }
}
