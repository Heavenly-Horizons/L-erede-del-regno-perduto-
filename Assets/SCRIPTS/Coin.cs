using UnityEngine;

public class Coin : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {   Debug.Log("Tag player trovato");
            // Aggiungi logica per incrementare il punteggio del giocatore o la quantità di monete
            PlayerStats playerS = other.GetComponent<PlayerStats>();
            if (playerS != null)
            {
                playerS.PlayerMoney++;
            }
            else{
                Debug.Log("PlayerStats non trovato");
            }

            // Distruggi la moneta
            Destroy(gameObject);
        }
        else{
            Debug.Log("Tag player non trovato");
        }
    }
}
