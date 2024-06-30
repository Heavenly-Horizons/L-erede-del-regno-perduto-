using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NemicoHealth : MonoBehaviour
{
    public float health = 30f;
    [SerializeField] private Slider healthBar;
    private DropCoin dropCoin;
    private DropHeal dropHeal;
    private Animator animator;
    private BoxCollider2D boxCollider2D;
    private bool isHit = false;


    void Start(){
        dropCoin = gameObject.GetComponent<DropCoin>();
        dropHeal = gameObject.GetComponent<DropHeal>();
        animator = gameObject.GetComponent<Animator>();
        boxCollider2D = gameObject.GetComponent<BoxCollider2D>();
    }

    public void TakeDamage(float amount)
    {
        if (isHit)
        {
            animator.SetTrigger("hurt");
            healthBar.gameObject.SetActive(true);
        }

        if (health - amount > 0)
        {
            health -= amount;
            healthBar.value = health;
            Debug.Log("vita nemico: " + health);
        }
        else
        {
            health = 0;
            healthBar.value = health;
            Debug.Log("vita nemico: " + health);

            if (dropCoin != null && dropHeal != null)
            {
                dropCoin.Drop(1);
                dropHeal.Drop(1);
            }

            animator.SetTrigger("die");
            if (boxCollider2D != null)
            {
                // Disattiva il BoxCollider2D
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
