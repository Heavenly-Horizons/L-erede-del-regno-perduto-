using System.Collections;
using UnityEngine;

public class Testa_Tyr : MonoBehaviour {
    [SerializeField] private GameObject player;
    public bool tyrHead;
    private PlayerMovement playerMovement;

    private void Awake() {
        if (player != null) {
            playerMovement = player.GetComponent<PlayerMovement>();
            Debug.Log(playerMovement != null
                ? "player.GetComponent<PlayerMovement>() in Testa_Tyr istanziato"
                : "player.GetComponent<PlayerMovement>() in Testa_Tyr non istanziato");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            tyrHead = true;
            playerMovement.KBCounter = playerMovement.KBTotalTime;
            playerMovement.KnockFromRight = player.transform.position.x <= transform.position.x;
            StartCoroutine(TestaColpita(0.2f));
        }
    }

    public IEnumerator TestaColpita(float duration) {
        yield return new WaitForSeconds(duration);
        tyrHead = false;
    }
}