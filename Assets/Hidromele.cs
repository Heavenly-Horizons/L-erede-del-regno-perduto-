using UnityEngine;

public class Hidromele : MonoBehaviour
{
    public float hidromele = 10f;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats playerS = other.GetComponent<PlayerStats>();
            if (playerS != null)
            {
                playerS.HealPlayer(hidromele);
                Debug.Log("vita player: " + playerS.GetPlayerCurrentHealth());
            }

            // Distruggi l'idromela
            Destroy(gameObject);
        }
    }
}
