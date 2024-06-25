using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float damage = 15f;
    public float lifeTime = 5f; // Tempo dopo cui la freccia viene distrutta
    public float knockbackForce = 40f; // Forza del knockback

    

    void OnCollisionEnter2D(Collision2D collision)
    {
        

        if (collision.gameObject.CompareTag("Nemico"))
        {
            Nemico nemico = collision.gameObject.GetComponent<Nemico>();
            if (nemico != null)
            {
                nemico.setHit(true);
                // Applica il knockback al nemico
                nemico.ApplyKnockback(knockbackForce);
                nemico.TakeDamage(damage);
            }
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
           
            if (player != null )
            {
                player.TakeDamage(damage);
            }
        }
        
        Destroy(gameObject); 
    }
}