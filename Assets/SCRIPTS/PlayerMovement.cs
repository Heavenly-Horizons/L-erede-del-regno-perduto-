using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator anim;

    private bool Grounded;
    [SerializeField] private float speed = 25f;  // Campo serializzabile per impostare la velocità da Unity
    [SerializeField] private float jumpForce = 5f;  // Forza del salto
    private bool canMove = true;
    public float KBForce=40f;
    public float KBCounter;
    public float KBTotalTime=0.2f;
    
    public bool KnockFromRight;

    private float horizontalInput;


    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (body == null)
        {
            Debug.LogError("Rigidbody2D component is missing on " + gameObject.name);
        }
        else
        {
            body.constraints = RigidbodyConstraints2D.FreezeRotation;  // Blocca la rotazione sull'asse Z
            body.isKinematic = false;
        }
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // Ottieni l'input orizzontale in Update
             horizontalInput = Input.GetAxis("Horizontal");  // Correzione del nome della variabile
            if(KBCounter <= 0){
               // Imposta la velocità del rigidbody
                body.velocity = new Vector2(horizontalInput * speed, body.velocity.y); 
            } else{
                if(KnockFromRight) body.velocity = new Vector2(-KBForce , KBForce/3);
                else body.velocity = new Vector2(KBForce , KBForce/3);
                
                KBCounter -= Time.deltaTime;
            }

            // Serve per far girare il personaggio da una parte all'altra
            if (horizontalInput > 0.01f)
            {
                transform.localScale = new Vector3(2f, 2f, 2f);
            }
            else if (horizontalInput < -0.01f)
            {  
                transform.localScale = new Vector3(-2f, 2f, 2f);
            }

            // Logica per il salto
            if (Input.GetKey(KeyCode.Space) && Grounded)
            {
                Jump();
            }

            anim.SetBool("Run", horizontalInput != 0);
            anim.SetBool("Grounded", Grounded);
        }
    }

    private void Jump()
    {
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        anim.SetTrigger("Jump");
        Grounded = false;
    }

   private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("terra"))
        {
            Grounded = true;
        }

        // Gestione del knockback dalla freccia
        if (collision.gameObject.CompareTag("Freccia"))
        {
            // Calcola la posizione della freccia rispetto al giocatore
            Vector3 relativePosition = collision.transform.position - transform.position;

            // Determina la direzione del knockback in base alla posizione della freccia
            KnockFromRight = relativePosition.x > 0; // Se la freccia è alla destra del giocatore, KnockFromRight sarà true

            // Applica il knockback e altri effetti desiderati
            KBCounter = KBTotalTime;
        }
    }


    public void CanMove()
    {
        canMove = true;
    }

    public void CanNotMove()
    {
        canMove = false;
    }

    public bool canAttack(){
        return Grounded;
    }

    
    
}
