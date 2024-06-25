using UnityEngine;

public class Tyr_Invulnerable : StateMachineBehaviour
{
    private Vector3 positionToFix;
    private Transform tyrTransform;
    private GameObject tyr;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tyr = GameObject.FindGameObjectWithTag("Nemico");
        tyrTransform = animator.transform;
        positionToFix = new Vector3(tyrTransform.position.x, tyrTransform.position.y, tyrTransform.position.z);
        animator.GetComponent<BossHealth>().isInvulnerable = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        tyr.transform.position = positionToFix;
        tyr.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex) {
        animator.SetBool("Invulnerable", false);
        animator.GetComponent<BossHealth>().isInvulnerable = false;
    }
}
