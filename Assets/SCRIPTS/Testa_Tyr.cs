using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testa_Tyr : MonoBehaviour
{
    
    [SerializeField] private GameObject player;
    private PlayerMovement playerMovement;
    public bool tyrHead =false;
    
    void Start() {
        if (player != null) {
            playerMovement = player.GetComponent<PlayerMovement>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")){
            tyrHead=true;
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = player.transform.position.x <= transform.position.x;
            StartCoroutine(TestaColpita(0.2f));
        }
    }

    public IEnumerator TestaColpita(float duration)
    {
        yield return new WaitForSeconds(duration);
        tyrHead = false;
    }
}
