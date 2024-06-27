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
                gameObject.GetComponent<DropHeal>().Drop(4);
                gameObject.GetComponent<DropCoin>().Drop(3);
            }

            if(damageCounter >= 50f){
                GetComponent<Animator>().SetBool("Invulnerable", true);
                gameObject.GetComponent<DropHeal>().Drop(2);
                damageCounter = 0;
            }
        }
    }

    IEnumerator BubbleShow(){
        animator.SetTrigger("BubbleShow");
        yield return new WaitForSeconds(2f);
        animator.ResetTrigger("BubbleShow");
    }
}
