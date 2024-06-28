using System.Collections;
using UnityEngine;

public class AchilleStun : MonoBehaviour
{
    BossHealth achilleHealth;
    Animator achilleAnimator;
    BossWalk bossWalk;
    private float bossRangeFromBossWalk;
    public bool isStunned = false;

    void Start()
    {
        achilleHealth = gameObject.GetComponent<BossHealth>();
        achilleAnimator = gameObject.GetComponent<Animator>();
        bossWalk = gameObject.GetComponent<Animator>().GetBehaviour<BossWalk>();
        bossRangeFromBossWalk = bossWalk.attackRange;
    }

    public void Stun(){
        if (achilleHealth.damageCounter >= 40f && !achilleHealth.isDead)
        {
            achilleAnimator.SetBool("Stunned", true);
            isStunned = true;
            bossWalk.attackRange = -1;
            StartCoroutine(WaitForAchilleStun());
            if (achilleHealth.isHit && isStunned)
            {
                StopAllCoroutines();
                achilleAnimator.SetTrigger("immediateRecover");
                StartCoroutine(StunnedToFalse());
                achilleHealth.damageCounter = 0;
                bossWalk.attackRange = bossRangeFromBossWalk;
                achilleAnimator.ResetTrigger("immediateRecover");
                isStunned = false;
            }
        }
    }

    IEnumerator WaitForAchilleStun(){
        yield return new WaitForSeconds(5.5f);
        achilleHealth.damageCounter = 0;
        achilleAnimator.SetBool("Stunned", false);
        isStunned = false;
        bossWalk.attackRange = bossRangeFromBossWalk;
    }

    IEnumerator StunnedToFalse(){
        yield return new WaitForSeconds(3f);
        achilleAnimator.SetBool("Stunned", false);
    }
}
