using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Nemico : MonoBehaviour {
    public float velocita = 300f;

    public float velocitaInseguimento = 500f;
    public bool direzioneInizialeSinistra;
    public float distanzaCambioDirezione = 30f;
    public float distanzaRilevamentoGiocatore = 30f;
    public Transform giocatore;

    public float normalAnimationSpeed = 1f; //velocità quando pattuglia
    public float FastAnimationSpeed = 2f; //velocità quando vede il player

    // Knockback fields
    public float KBForce = 40f;
    public float KBCounter;
    public float KBTotalTime = 0.2f;
    public bool KnockFromRight;

    public Slider nemicoHealthBar;

    public float nemicoHealth = 30;

    private DropCoin dropCoin;
    private DropHeal dropHeal;

    private bool isCambioDirezione;
    private bool isGuardandoDestra = true;

    private bool isHit;
    private bool isInseguendo;
    private bool isStopped;
    private Animator nemicoAnim;
    private Vector3 posizioneIniziale;
    private Rigidbody2D rb;

    private void Start() {
        //rb
        rb = GetComponent<Rigidbody2D>();
        Debug.Log(rb != null
            ? "GetComponent<Rigidbody2D>() in Nemico istanziato"
            : "GetComponent<Rigidbody2D>() in Nemico non istanziato");
        if (rb == null)
            Debug.LogError("Rigidbody2D component is missing on " + gameObject.name);
        else
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        isCambioDirezione = !direzioneInizialeSinistra;
        posizioneIniziale = transform.position;

        if (giocatore == null) Debug.LogError("Giocatore non assegnato nel nemico " + gameObject.name);

        //nemicoAnim
        nemicoAnim = GetComponent<Animator>();
        Debug.Log(nemicoAnim != null
            ? "GetComponent<Animator>() in Nemico istanziato"
            : "GetComponent<Animator>() in Nemico non istanziato");

        //dropCoin
        dropCoin = GetComponent<DropCoin>();
        Debug.Log(dropCoin != null
            ? "GetComponent<DropCoin>() in Nemico istanziato"
            : "GetComponent<DropCoin>() in Nemico non istanziato");

        //dropHeal
        dropHeal = GetComponent<DropHeal>();
        Debug.Log(dropHeal != null
            ? "GetComponent<DropHeal>() in Nemico istanziato"
            : "GetComponent<DropHeal>() in Nemico non istanziato");
    }

    private void Update() {
        if (isStopped) return;

        if (KBCounter > 0) {
            Vector2 knockbackDirection = KnockFromRight ? Vector2.left : Vector2.right;
            rb.velocity = new(knockbackDirection.x * KBForce, KBForce / 3);
            KBCounter -= Time.deltaTime;
        }
        else {
            if (giocatore != null) {
                float distanzaDalGiocatore = Vector3.Distance(transform.position, giocatore.position);
                isInseguendo = distanzaDalGiocatore <= distanzaRilevamentoGiocatore;

                if (isInseguendo) {
                    if (giocatore.position.x < transform.position.x && isGuardandoDestra)
                        Flip();
                    else if (giocatore.position.x > transform.position.x && !isGuardandoDestra) Flip();
                }
            }

            if (isInseguendo) {
                nemicoAnim.speed = FastAnimationSpeed;
                Vector3 direzioneVersoGiocatore = (giocatore.position - transform.position).normalized;
                rb.velocity = new(direzioneVersoGiocatore.x * velocitaInseguimento * Time.deltaTime, rb.velocity.y);
            }
            else {
                nemicoAnim.speed = normalAnimationSpeed;
                int moltiplicatoreDirezione = isCambioDirezione ? -1 : 1;
                rb.velocity = new(velocita * Time.deltaTime * moltiplicatoreDirezione, rb.velocity.y);

                if (moltiplicatoreDirezione > 0 && !isGuardandoDestra)
                    Flip();
                else if (moltiplicatoreDirezione < 0 && isGuardandoDestra) Flip();

                if (Vector3.Distance(posizioneIniziale, transform.position) >= distanzaCambioDirezione)
                    CambiaDirezione();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("NoJump")) {
            if (!isInseguendo)
                CambiaDirezione();
            else
                rb.velocity = new(rb.velocity.x, 35f);
        }

        if (collision.gameObject.CompareTag("Nemico")) CambiaDirezione();

        if (collision.gameObject.CompareTag("Freccia")) {
            Vector3 relativePosition = collision.transform.position - transform.position;
            KnockFromRight = relativePosition.x > 0;

            KBCounter = KBTotalTime;
        }
    }

    private void CambiaDirezione() {
        isCambioDirezione = !isCambioDirezione;
        transform.Rotate(0f, 180f, 0f);
        posizioneIniziale = transform.position;
        isGuardandoDestra = !isGuardandoDestra;
    }

    private void Flip() {
        isGuardandoDestra = !isGuardandoDestra;
        Vector3 scala = transform.localScale;
        scala.x *= -1;
        transform.localScale = scala;
    }

    public IEnumerator StopMovement(float duration) {
        isStopped = true;
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(duration);
        isStopped = false;
    }

    public void ApplyKnockback(float force) {
        KBForce = force;
    }

    public void TakeDamage(float amount) {
        GameObject gObject = gameObject.transform.GetChild(0).GetChild(0).gameObject;
        Debug.Log(gObject != null
            ? "gameObject.transform.GetChild(0).GetChild(0).gameObject in Nemico istanziato"
            : "gameObject.transform.GetChild(0).GetChild(0).gameObject in Nemico non istanziato");

        if (isHit) {
            nemicoAnim.SetTrigger("hurt");

            gObject.SetActive(true);
        }

        if (nemicoHealth - amount > 0) {
            nemicoHealth -= amount;
            nemicoHealthBar.value = Mathf.Floor(amount);
            Debug.Log("vita nemico arciere: " + nemicoHealthBar.value);
        }
        else {
            nemicoHealth = 0;
            nemicoHealthBar.value = nemicoHealth;

            gObject.SetActive(false);

            if (dropCoin != null && dropHeal != null) {
                dropCoin.Drop(1);
                dropHeal.Drop(1);
            }

            nemicoAnim.SetTrigger("die");
            StartCoroutine(PlayDeathAnimation());
        }
    }

    private IEnumerator PlayDeathAnimation() {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    public void setHit(bool value) {
        isHit = value;
    }
}