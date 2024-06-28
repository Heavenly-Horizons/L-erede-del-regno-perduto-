using System.Collections;
using UnityEngine;

public class AchilleStun : MonoBehaviour
{
    BossHealth achilleHealth;
    Animator achilleAnimator;

    void Start()
    {
        achilleHealth = gameObject.GetComponent<BossHealth>();
        achilleAnimator = gameObject.GetComponent<Animator>();
    }

    public void Stun(){
        if (achilleHealth.damageCounter >= 40f && !achilleHealth.isDead)
        {
            gameObject.GetComponent<DropHeal>().Drop(2);
            achilleAnimator.SetBool("Stunned", true);
            StartCoroutine(WaitForAchilleStun());
            if (achilleHealth.isHit)
            {
                StopAllCoroutines();
                achilleAnimator.SetBool("immediateRecover", true);
                achilleAnimator.SetBool("Stunned", true);
                achilleHealth.damageCounter = 0;
            }
        }
    }

    IEnumerator WaitForAchilleStun(){
        yield return new WaitForSeconds(6.5f);
        achilleHealth.damageCounter = 0;
        achilleAnimator.SetBool("Stunned", false);
        achilleAnimator.SetBool("immediateRecover", false);
    }
}
