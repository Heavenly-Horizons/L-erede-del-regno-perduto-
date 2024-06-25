<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
=======
>>>>>>> main
using UnityEngine;

public class Knight3_PlayerMovement : MonoBehaviour
{
    private Animator animator; //animator
    private int parryFloatHash; //parryFloat hash code
    private float parryFloatValue = 0.0f;

    [SerializeField] private int acceleration = 10;
    [SerializeField] private int deceleration = 5;

    // Start is called before the first frame update
    void Start()
    {
        AnimatorInit();
    }

    private void AnimatorInit()
    {
        animator = GetComponent<Animator>(); //get animator

        parryFloatHash = Animator.StringToHash("parryFloat"); //get hash code from animator
    }

    // Update is called once per frame
    void Update()
    {
        ParryAnimation();
    }

    private void ParryAnimation()
    {
        bool isLeftCtrlButton = Input.GetKey(KeyCode.LeftControl);

        if (isLeftCtrlButton && parryFloatValue < 1.0f)
        {
            parryFloatValue += Time.deltaTime * acceleration;
        }
        if (!isLeftCtrlButton && parryFloatValue > 0.0f)
        {
            parryFloatValue -= Time.deltaTime * deceleration;
        }
        if (!isLeftCtrlButton && parryFloatValue < 0.0f)
            parryFloatValue = 0.0f;
        else if (!isLeftCtrlButton && parryFloatValue > 1f)
            parryFloatValue = 1f;

        animator.SetFloat(parryFloatHash, parryFloatValue);
    }
}
