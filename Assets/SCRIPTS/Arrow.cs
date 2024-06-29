using UnityEngine;

public class Arrow : MonoBehaviour {
    public float damage = 15f;
    
    public float knockbackForce = 40f; // Forza del knockback


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Nemico")) {
            var nemico = collision.gameObject.GetComponent<Nemico>();
            if (nemico != null) {
                nemico.setHit(true);
                // Applica il knockback al nemico
                nemico.ApplyKnockback(knockbackForce);
                nemico.TakeDamage(damage);
            }
        }

        if (collision.gameObject.CompareTag("Player")) {
            var player = collision.gameObject.GetComponent<PlayerStats>();

            if (player != null) player.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}