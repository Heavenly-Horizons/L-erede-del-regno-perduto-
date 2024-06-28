using System.Collections;
using UnityEngine;

public class AchilleStun : MonoBehaviour {
    private static readonly int Stunned = Animator.StringToHash("Stunned");
    private static readonly int ImmediateRecover = Animator.StringToHash("immediateRecover");
    private Animator achilleAnimator;
    private BossHealth achilleHealth;

    private void Start() {
        achilleHealth = gameObject.GetComponent<BossHealth>();
        achilleAnimator = gameObject.GetComponent<Animator>();
    }

    public void Stun() {
        if (achilleHealth.damageCounter >= 40f && !achilleHealth.isDead) {
            gameObject.GetComponent<DropHeal>().Drop(2);
            achilleAnimator.SetBool(Stunned, true);
            StartCoroutine(WaitForAchilleStun());
            if (achilleHealth.isHit) {
                StopAllCoroutines();
                achilleAnimator.SetBool(ImmediateRecover, true);
                achilleAnimator.SetBool(Stunned, false);
                achilleHealth.damageCounter = 0;
                achilleAnimator.SetBool(ImmediateRecover, false);
            }
        }
    }

    private IEnumerator WaitForAchilleStun() {
        yield return new WaitForSeconds(6.5f);
        achilleHealth.damageCounter = 0;
        achilleAnimator.SetBool(Stunned, false);
        achilleAnimator.SetBool(ImmediateRecover, false);
    }
}