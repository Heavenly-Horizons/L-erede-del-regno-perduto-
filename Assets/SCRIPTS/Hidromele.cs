using UnityEngine;

public class Hidromele : MonoBehaviour {
    public float hidromele = 10f;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            var playerS = other.GetComponent<PlayerStats>();
            Debug.Log(playerS != null
                ? "other.GetComponent<PlayerStats>() in Hidromele istanziato"
                : "other.GetComponent<PlayerStats>() in Hidromele non istanziato");
            if (playerS != null) playerS.HealPlayer(hidromele);

            // Distruggi l'idromela
            Destroy(gameObject);
        }
    }
}