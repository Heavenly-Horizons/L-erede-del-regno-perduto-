using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Aggiungi logica per incrementare il punteggio del giocatore o la quantità di monete
            PlayerStats playerS = other.GetComponent<PlayerStats>();
            if (playerS != null)
            {
                playerS.PlayerMoney++;
            }

            // Distruggi la moneta
            Destroy(gameObject);
        }
    }
}
