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
    // Ottenere il componente BossHealth
    achilleHealth = gameObject.GetComponent<BossHealth>();
    if (achilleHealth != null)
    {
        Debug.Log("Componente BossHealth Achille trovato.");
    }
    else
    {
        Debug.LogWarning("Componente BossHealth non trovato sul GameObject.");
    }

    // Ottenere il componente Animator
    achilleAnimator = gameObject.GetComponent<Animator>();
    if (achilleAnimator != null)
    {
        Debug.Log("Componente Animator Achille trovato.");
    }
    else
    {
        Debug.LogWarning("Componente Animator non trovato sul GameObject.");
    }

    // Ottenere il Behaviour BossWalk dall'Animator
    bossWalk = gameObject.GetComponent<Animator>().GetBehaviour<BossWalk>();
    if (bossWalk != null)
    {
        Debug.Log("Behaviour BossWalk Achille trovato.");
        // Ottenere l'attacco range da BossWalk
        bossRangeFromBossWalk = bossWalk.attackRange;
        Debug.Log("Range d'attacco da BossWalk: " + bossRangeFromBossWalk);
    }
    else
    {
        Debug.LogWarning("Behaviour BossWalk non trovato sull'Animator.");
    }

    // Ottenere il componente DropHeal
    dropHeal = gameObject.GetComponent<DropHeal>();
    if (dropHeal != null)
    {
        Debug.Log("Componente DropHeal trovato.");
    }
    else
    {
        Debug.LogWarning("Componente DropHeal non trovato sul GameObject.");
    }
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
