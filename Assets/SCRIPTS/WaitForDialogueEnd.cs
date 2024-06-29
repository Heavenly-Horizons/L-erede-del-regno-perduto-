using UnityEngine;

public class WaitForDialogueEnd : StateMachineBehaviour {
    private Boss boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Debug.Log(animator.GetComponent<Boss>()
            ? "animator.GetComponent<Boss>() trovato"
            : "animator.GetComponent<Boss>() non trovato");
        boss = animator.GetComponent<Boss>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (boss != null && boss.isDialogueEnded) {
            Debug.Log(boss != null
                ? "boss inizializzato"
                : "boss non inizializzato");
            animator.SetBool("DialogueEnded", true);
        }
    }
}