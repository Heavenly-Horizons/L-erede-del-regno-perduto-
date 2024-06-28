using System.Collections;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    private Animator animator;
    public Slider bossHealthSlider;
    public bool isInvulnerable = false;
    public float bossHealth;
    public float damageCounter;
    public bool isDead = false;

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

            if (bossHealth - amount > 0){
                animator.SetTrigger("Hurt");
                bossHealth -= amount;
                bossHealthSlider.value = bossHealth;
                damageCounter += amount;
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
        }
    }

    IEnumerator BubbleShow(){
        animator.SetTrigger("BubbleShow");
        yield return new WaitForSeconds(2f);
        animator.ResetTrigger("BubbleShow");
    }
}
