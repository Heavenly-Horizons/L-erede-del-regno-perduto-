using UnityEngine;
using System.Collections;
<<<<<<< HEAD

public class Nemico : MonoBehaviour
{
    public float velocita = 5f;
    private Animator nemicoAnim;

    public float velocitaInseguimento = 10f;
    public bool direzioneInizialeSinistra = true;
    public float distanzaCambioDirezione = 10f;
    public float distanzaRilevamentoGiocatore = 5f;
=======
using UnityEngine.UI;

public class Nemico : MonoBehaviour
{
    public float velocita = 300f;
    private Animator nemicoAnim;

    public float velocitaInseguimento = 500f;
    public bool direzioneInizialeSinistra = false;
    public float distanzaCambioDirezione = 30f;
    public float distanzaRilevamentoGiocatore = 30f;
>>>>>>> main
    public Transform giocatore;

    private bool isCambioDirezione = false;
    private bool isInseguendo = false;
<<<<<<< HEAD
    public Rigidbody2D rb;
=======
    private Rigidbody2D rb;
>>>>>>> main
    private Vector3 posizioneIniziale;
    private bool isGuardandoDestra = true; 
    private bool isStopped = false; 

<<<<<<< HEAD
    public Animator animator;

    // Knockback fields
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

=======
     public float normalAnimationSpeed = 1f; //velocità quando pattuglia
    public float FastAnimationSpeed = 2f; //velocità quando vede il player

    // Knockback fields
    public float KBForce = 40f;
    public float KBCounter;
    public float KBTotalTime = 0.2f;
    public bool KnockFromRight;

    public Slider nemicoHealthBar;

    public float nemicoHealth = 30;

    private bool isHit = false;

    private DropCoin dropCoin;

>>>>>>> main
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on " + gameObject.name);
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }

        isCambioDirezione = !direzioneInizialeSinistra;
        posizioneIniziale = transform.position;

        if (giocatore == null)
        {
            Debug.LogError("Giocatore non assegnato nel nemico " + gameObject.name);
        }

        nemicoAnim = GetComponent<Animator>();
<<<<<<< HEAD
=======
        dropCoin = GetComponent<DropCoin>();
>>>>>>> main
    }

    void FixedUpdate()
    {
        if (isStopped) return;

        if (KBCounter > 0)
        {
            Vector2 knockbackDirection = KnockFromRight ? Vector2.left : Vector2.right;
            rb.velocity = new Vector2(knockbackDirection.x * KBForce, KBForce / 3);
            KBCounter -= Time.deltaTime;
        }
        else
        {
            if (giocatore != null)
            {
                float distanzaDalGiocatore = Vector3.Distance(transform.position, giocatore.position);
                isInseguendo = distanzaDalGiocatore <= distanzaRilevamentoGiocatore;

                if (isInseguendo)
                {
                    if (giocatore.position.x < transform.position.x && isGuardandoDestra)
                    {
                        Flip();
                    }
                    else if (giocatore.position.x > transform.position.x && !isGuardandoDestra)
                    {
                        Flip();
                    }
                }
            }

            if (isInseguendo)
            {
<<<<<<< HEAD
                animator.SetTrigger("Corsa");
=======
                nemicoAnim.speed = FastAnimationSpeed;
>>>>>>> main
                Vector3 direzioneVersoGiocatore = (giocatore.position - transform.position).normalized;
                rb.velocity = new Vector2(direzioneVersoGiocatore.x * velocitaInseguimento * Time.deltaTime, rb.velocity.y);
            }
            else
            {
<<<<<<< HEAD
                animator.SetTrigger("Camminata");
=======
                nemicoAnim.speed=normalAnimationSpeed;
>>>>>>> main
                int moltiplicatoreDirezione = isCambioDirezione ? -1 : 1;
                rb.velocity = new Vector2(velocita * Time.deltaTime * moltiplicatoreDirezione, rb.velocity.y);

                if (moltiplicatoreDirezione > 0 && !isGuardandoDestra)
                {
                    Flip();
                }
                else if (moltiplicatoreDirezione < 0 && isGuardandoDestra)
                {
                    Flip();
                }

                if (Vector3.Distance(posizioneIniziale, transform.position) >= distanzaCambioDirezione)
                {
                    CambiaDirezione();
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NoJump"))
        {
            if (!isInseguendo)
            {
<<<<<<< HEAD
                Flip();
=======
                CambiaDirezione();
>>>>>>> main
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 35f);
            }
        }
<<<<<<< HEAD
=======

        if (collision.gameObject.CompareTag("Nemico")){
            CambiaDirezione();
        }
        
        if (collision.gameObject.CompareTag("Freccia"))
        {
            Vector3 relativePosition = collision.transform.position - transform.position;
            KnockFromRight = (relativePosition.x > 0);

            KBCounter = KBTotalTime;
        }
>>>>>>> main
    }

    void CambiaDirezione()
    {
        isCambioDirezione = !isCambioDirezione;
        transform.Rotate(0f, 180f, 0f);
        posizioneIniziale = transform.position;
        isGuardandoDestra = !isGuardandoDestra;
    }

    void Flip()
    {
        isGuardandoDestra = !isGuardandoDestra;
        Vector3 scala = transform.localScale;
        scala.x *= -1;
        transform.localScale = scala;
    }

    public IEnumerator StopMovement(float duration)
    {
        isStopped = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }

    public void ApplyKnockback(float force)
    {
        KBForce = force;
    }
<<<<<<< HEAD
=======

    public void TakeDamage(float amount)
    {
        if (isHit)
        {   nemicoAnim.SetTrigger("hurt");
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        if (nemicoHealth - amount > 0)
        {
            nemicoHealth -= amount;
            nemicoHealthBar.value = Mathf.Floor(amount);
            Debug.Log("vita nemico arciere: " + nemicoHealthBar.value);
        }
        else
        {   nemicoAnim.SetTrigger("die");
            nemicoHealth = 0;
            nemicoHealthBar.value = nemicoHealth;
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
             GetComponentInParent<Nemico>().enabled = false;
             GetComponent<Nemico_Attack>().enabled= false;

            if (dropCoin != null)
            {
                dropCoin.Drop();
            }
        }
    }

    public void setHit(bool value){
        isHit = value;
    }
>>>>>>> main
}
