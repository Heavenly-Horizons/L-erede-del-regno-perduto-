using UnityEngine;

public class Arrow : MonoBehaviour {
    public float damage = 15f;
    public float lifeTime = 5f; // Tempo dopo cui la freccia viene distrutta
    public float knockbackForce = 40f; // Forza del knockback


private void OnCollisionEnter2D(Collision2D collision) {
    // Controllo se il GameObject ha il tag "Nemico"
    if (collision.gameObject.CompareTag("Nemico")) {
        // Ottenere il componente Nemico dal GameObject colliduto
        var nemico = collision.gameObject.GetComponent<Nemico>();
        if (nemico != null) {
            nemico.setHit(true);
            // Applica il knockback al nemico
            nemico.ApplyKnockback(knockbackForce);
            nemico.TakeDamage(damage);
            Debug.Log("Danno applicato al nemico");
        } else {
            Debug.LogWarning("Componente Nemico non trovato sul GameObject con tag 'Nemico'.");
        }
    }

    // Controllo se il GameObject ha il tag "Player"
    if (collision.gameObject.CompareTag("Player")) {
        // Ottenere il componente PlayerStats dal GameObject colliduto
        var player = collision.gameObject.GetComponent<PlayerStats>();
        if (player != null) {
            player.TakeDamage(damage);
            Debug.Log("Danno applicato al player");
        } else {
            Debug.LogWarning("Componente PlayerStats non trovato sul GameObject con tag 'Player'.");
        }
    }

    Destroy(gameObject);
}

}