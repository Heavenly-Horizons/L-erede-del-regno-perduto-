using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Nemico : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator nemicoAnim;
    private BoxCollider2D boxCollider2D;
    private Transform giocatore;
    private DropCoin dropCoin;
    private DropHeal dropHeal;

    public float velocita = 300f;
    public float velocitaInseguimento = 500f;
    public float distanzaCambioDirezione = 30f;
    public float distanzaRilevamentoGiocatore = 30f;
    public float normalAnimationSpeed = 1f;
    public float fastAnimationSpeed = 2f;
    public float nemicoHealth = 30f;
    public float KBForce = 40f;
    public float KBCounter;
    public float KBTotalTime = 0.2f;
    public bool KnockFromRight;

    private Vector3 posizioneIniziale;
    private bool isGuardandoDestra = true;
    private bool isInseguendo = false;
    private bool isHit = false;
    private bool isStopped = false;

    public Slider nemicoHealthBar;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        nemicoAnim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        giocatore = GameObject.FindGameObjectWithTag("Player").transform; // Assumendo che il giocatore abbia il tag "Player"
        dropCoin = GetComponent<DropCoin>();
        dropHeal = GetComponent<DropHeal>();

        if (rb == null)
            Debug.LogError("Rigidbody2D component is missing on " + gameObject.name);

        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        posizioneIniziale = transform.position;

        if (giocatore == null)
            Debug.LogError("Giocatore non assegnato nel nemico " + gameObject.name);
    }

    void Update()
    {
        if (isStopped)
            return;

        if (KBCounter > 0)
        {
            Vector2 knockbackDirection = KnockFromRight ? Vector2.left : Vector2.right;
            rb.velocity = new Vector2(knockbackDirection.x * KBForce, KBForce / 3);
            KBCounter -= Time.deltaTime;
        }
        else
        {
            UpdateMovement();
        }
    }

    void UpdateMovement()
    {
        float distanzaDalGiocatore = Vector3.Distance(transform.position, giocatore.position);
        isInseguendo = distanzaDalGiocatore <= distanzaRilevamentoGiocatore;

        if (isInseguendo)
        {
            HandleChasePlayer();
        }
        else
        {
            HandlePatrol();
        }
    }

    void HandleChasePlayer()
    {
        nemicoAnim.speed = fastAnimationSpeed;

        if (giocatore.position.x < transform.position.x && isGuardandoDestra)
            Flip();
        else if (giocatore.position.x > transform.position.x && !isGuardandoDestra)
            Flip();

        Vector3 direzioneVersoGiocatore = (giocatore.position - transform.position).normalized;
        rb.velocity = new Vector2(direzioneVersoGiocatore.x * velocitaInseguimento * Time.deltaTime, rb.velocity.y);
    }

    void HandlePatrol()
    {
        nemicoAnim.speed = normalAnimationSpeed;

        int moltiplicatoreDirezione = isGuardandoDestra ? 1 : -1;
        rb.velocity = new Vector2(velocita * Time.deltaTime * moltiplicatoreDirezione, rb.velocity.y);

        if (moltiplicatoreDirezione > 0 && !isGuardandoDestra || moltiplicatoreDirezione < 0 && isGuardandoDestra)
            Flip();

        if (Vector3.Distance(posizioneIniziale, transform.position) >= distanzaCambioDirezione)
            CambiaDirezione();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("NoJump"))
        {
            if (!isInseguendo)
                CambiaDirezione();
            else
                rb.velocity = new Vector2(rb.velocity.x, 35f);
        }

        if (collision.gameObject.CompareTag("Nemico"))
            CambiaDirezione();

        if (collision.gameObject.CompareTag("Freccia"))
        {
            Vector3 relativePosition = collision.transform.position - transform.position;
            KnockFromRight = relativePosition.x > 0;

            KBCounter = KBTotalTime;
        }
    }

    void CambiaDirezione()
    {
        isGuardandoDestra = !isGuardandoDestra;
        transform.Rotate(0f, 180f, 0f);
        posizioneIniziale = transform.position;
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

    public void TakeDamage(float amount)
    {
        if (isHit)
        {
            nemicoAnim.SetTrigger("hurt");
            gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }

        Debug.Log("Taking damage: " + amount);
        Debug.Log("Current health before damage: " + nemicoHealth);

        nemicoHealth -= amount;
        nemicoHealth = Mathf.Max(nemicoHealth, 0);
        nemicoHealthBar.value = nemicoHealth;

        Debug.Log("Current health after damage: " + nemicoHealth);

        if (nemicoHealth <= 0)
        {
            nemicoHealth = 0;
            nemicoHealthBar.value = nemicoHealth;

            if (dropCoin != null && dropHeal != null)
            {
                dropCoin.Drop(1);
                dropHeal.Drop(1);
            }

            nemicoAnim.SetTrigger("die");

            if (boxCollider2D != null)
            {
                boxCollider2D.enabled = false;
                Debug.Log("BoxCollider2D disattivato");
            }
            else
            {
                Debug.LogError("BoxCollider2D non trovato su questo GameObject");
            }

            StartCoroutine(PlayDeathAnimation());
        }
    }

    IEnumerator PlayDeathAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    public void setHit(bool value)
    {
        isHit = value;
    }
}
