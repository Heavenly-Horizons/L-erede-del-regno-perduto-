using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private Animator animator;
    public Slider bossHealthSlider;
    public bool isInvulnerable = false;
    public float bossHealth;
    public float damageCounter;
    private bool playerHealed = false;

    void Start(){
        animator = GetComponent<Animator>();
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

            if (bossHealth > 0)
            {
                animator.SetTrigger("Hurt");
                bossHealth -= amount;
                bossHealthSlider.value = bossHealth;
                damageCounter += amount;
                animator.ResetTrigger("Hurt");
            }else{
                // Da attivare la sequenza finale, da attivare il trigger
            }

            if(damageCounter >= 50f){
                GetComponent<Animator>().SetBool("Invulnerable", true);
                if(!playerHealed){
                    playerHealed = true;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().HealPlayer(20);
                }
                damageCounter = 0;
            }
        }
    }

    IEnumerator BubbleShow(){
        animator.SetTrigger("BubbleShow");
        yield return new WaitForSeconds(0.8f);
        animator.ResetTrigger("BubbleShow");
    }
}
