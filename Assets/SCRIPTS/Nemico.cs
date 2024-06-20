using UnityEngine;
using System.Collections;

public class Nemico : MonoBehaviour
{
    public float velocita = 5f;
    private Animator nemicoAnim;

    public float velocitaInseguimento = 10f;
    public bool direzioneInizialeSinistra = true;
    public float distanzaCambioDirezione = 10f;
    public float distanzaRilevamentoGiocatore = 5f;
    public Transform giocatore;

    private bool isCambioDirezione = false;
    private bool isInseguendo = false;
    public Rigidbody2D rb;
    private Vector3 posizioneIniziale;
    private bool isGuardandoDestra = true; 
    private bool isStopped = false; 

    public Animator animator;

    // Knockback fields
    public float KBForce;
    public float KBCounter;
    public float KBTotalTime;
    public bool KnockFromRight;

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
                animator.SetTrigger("Corsa");
                Vector3 direzioneVersoGiocatore = (giocatore.position - transform.position).normalized;
                rb.velocity = new Vector2(direzioneVersoGiocatore.x * velocitaInseguimento * Time.deltaTime, rb.velocity.y);
            }
            else
            {
                animator.SetTrigger("Camminata");
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
                Flip();
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 35f);
            }
        }
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
}
