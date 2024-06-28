using System.Collections;
using UnityEngine;

public class AchilleStun : MonoBehaviour
{
    BossHealth achilleHealth;
    Animator achilleAnimator;
    Rigidbody2D rb;

    float KBForce = 40f;
    float KBCounter;
    float KBTotalTime = 0.2f;
    bool KnockFromRight;

    void Start()
    {
        achilleHealth = gameObject.GetComponent<BossHealth>();
        achilleAnimator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (KBCounter > 0)
        {
            Vector2 knockbackDirection = KnockFromRight ? Vector2.left : Vector2.right;
            rb.velocity = new Vector2(knockbackDirection.x * KBForce, KBForce / 3);
            KBCounter -= Time.deltaTime;
        }
        if (achilleHealth.damageCounter >= 40f && !achilleHealth.isDead)
        {
            achilleAnimator.SetBool("Stunned", true);
            StartCoroutine(WaitForAchilleStun());
            gameObject.GetComponent<DropHeal>().Drop(2);
            if(achilleHealth.isHit){
                StopAllCoroutines();
                achilleAnimator.SetTrigger("immediateRecovery");
                achilleHealth.damageCounter = 0;
            }
        }
    }

    public void ApplyKnockback(bool knockFromRight)
    {
        KnockFromRight = knockFromRight;
        KBCounter = KBTotalTime;
    }

    IEnumerator WaitForAchilleStun(){
        yield return new WaitForSeconds(6.5f);
        achilleHealth.damageCounter = 0;
        achilleAnimator.SetBool("Stunned", false);
    }
}
