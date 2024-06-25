using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aggiungi logica per incrementare il punteggio del giocatore o la quantità di monete
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            if (player != null)
            {
                player.AddCoin();
            }

            // Distruggi la moneta
            Destroy(gameObject);
        }
    }
}
