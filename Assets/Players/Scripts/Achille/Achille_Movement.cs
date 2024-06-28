using System;
using UnityEngine;

public class Achille_Movement : MonoBehaviour
{
    private Animator animator;
    private Transform myTransform;
    public float life;
    private float run;
    private bool takesDamage = false;
    [SerializeField] private float damage = 0;
    


    // Start is called before the first frame update
    void Start()
    {
        //Animator
        animator = GetComponent<Animator>();
        life = animator.GetFloat("Life");
        run = animator.GetFloat("Run");

        //Transform
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        checkRun();
        checkLife();
        checkDamage(damage);

        // Vector3 position = myTransform.position;
        // myTransform.position = new Vector2(position.x - 0.1f, 0);
    }

    private void checkRun()
    {
        if (run < 0) run = 0;
        if (run > 1) run = 1;
    }

    private void checkLife()
    {
        if (life <= 0)
        {
            animator.SetFloat("Life", 0);
            animator.SetTrigger("Hurt");
        }
    }

    private void checkDamage(float damage)
    {
        if (takesDamage) life -= damage;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        String objectName = other.gameObject.name;
        Debug.Log("Object name: " + objectName);
    }
}
