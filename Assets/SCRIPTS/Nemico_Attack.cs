using System.Collections;
using UnityEngine;

public class Nemico_Attack : MonoBehaviour
{
    public int damage;
    [SerializeField] private GameObject player;
    private PlayerStats playerStats;
    public PlayerMovement playerMovement;
    
    private void Awake()
    {
        if (player != null)
        {
            playerStats = player.GetComponent<PlayerStats>();
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerStats != null && playerMovement != null)
        {
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = collision.transform.position.x <= transform.position.x;
            
            playerStats.TakeDamage(damage);

            // Stop enemy movement for 0.5 seconds
            Nemico nemico = GetComponent<Nemico>();
            if (nemico != null)
            {
                StartCoroutine(nemico.StopMovement(0.5f));
            }
        }
    }
}
