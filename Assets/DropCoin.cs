using UnityEngine;

public class DropCoin : MonoBehaviour
{
    public GameObject coinPrefab;  // Il prefab della moneta
    public float dropForce = 5f;   // Forza verso l'alto
    public float randomForce = 10f; // Forza random laterale

    public void Drop()
    {
        // Istanzia la moneta nella posizione del nemico
        GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);

        // Ottieni il Rigidbody2D della moneta per applicare le forze
        Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();

        // Calcola una forza laterale random
        float randomDirection = Random.Range(-1f, 1f) * randomForce;

        // Applica la forza verso l'alto e la forza laterale random
        rb.AddForce(new Vector2(randomDirection, dropForce), ForceMode2D.Impulse);
    }
}
