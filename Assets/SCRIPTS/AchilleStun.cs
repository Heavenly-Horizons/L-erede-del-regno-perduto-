using System.Collections;
using UnityEngine;

public class AchilleStun : MonoBehaviour
{
    BossHealth achilleHealth;
    Animator achilleAnimator;
    BossWalk bossWalk;
    DropHeal dropHeal;
    private float bossRangeFromBossWalk;
    public bool isStunned = false;
    private bool isCurrentlyStunned = false;

    void Start()
    {
        achilleHealth = gameObject.GetComponent<BossHealth>();
        achilleAnimator = gameObject.GetComponent<Animator>();
        bossWalk = gameObject.GetComponent<Animator>().GetBehaviour<BossWalk>();
        bossRangeFromBossWalk = bossWalk.attackRange;
        dropHeal = gameObject.GetComponent<DropHeal>();
    }

    public void Stun(){
        if (achilleHealth.damageCounter >= 40f && !achilleHealth.isDead)
        {
            dropHeal.Drop(2);
            achilleAnimator.SetBool("Stunned", true);
            isStunned = true;
            bossWalk.attackRange = -1;
            StartCoroutine(WaitForAchilleStun());
            if (achilleHealth.isHit && isStunned && isCurrentlyStunned){
                StopAllCoroutines();
                achilleAnimator.SetBool("Stunned", false);
                achilleHealth.damageCounter = 0;
                bossWalk.attackRange = bossRangeFromBossWalk;
                isStunned = false;
                isCurrentlyStunned = false;
            }else{
                isCurrentlyStunned = true;
            }
        }
    }

    IEnumerator WaitForAchilleStun(){
        yield return new WaitForSeconds(5.5f);
        achilleHealth.damageCounter = 0;
        achilleAnimator.SetBool("Stunned", false);
        isStunned = false;
        isCurrentlyStunned = false;
        bossWalk.attackRange = bossRangeFromBossWalk;
    }
}
