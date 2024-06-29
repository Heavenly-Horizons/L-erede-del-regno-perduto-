using System.Collections;
using UnityEngine;

public class PlayerBubble : MonoBehaviour {
    [SerializeField] private SpriteRenderer playerBubble;
    public float bubbleCooldown = 2f;
    private Color _playerBubbleColor;
    private float bubbleCooldownTimer;
    private bool bubbleShow;
    private PlayerStats playerStats;

    private void Awake() {
        _playerBubbleColor = playerBubble.color;
        _playerBubbleColor.a = 0f;
        bubbleCooldownTimer = bubbleCooldown;

        //playerStats
        playerStats = GetComponent<PlayerStats>();
        Debug.Log(playerStats != null
            ? "GetComponent<PlayerStats>() in PlayerBubble istanziato"
            : "GetComponent<PlayerStats>() in PlayerBubble non istanziato");
    }

    private void Update() {
        //appare la bolla
        if (Input.GetKeyDown(KeyCode.K) && !bubbleShow && bubbleCooldownTimer >= bubbleCooldown &&
            playerStats.GetPlayerCurrentStamina() >= 50) {
            //mostra bolla
            playerStats.UseStamina(50);
            StartCoroutine(ShowBubble());
            bubbleCooldownTimer = 0f;
        }

        playerStats.isInvulnerable = bubbleShow;
        bubbleCooldownTimer += Time.deltaTime;
    }

    private IEnumerator NotShowBubble() {
        // Calcola quanto decrementare alpha in ogni frame basandosi sulla durata del fade
        float fadeStep = 1f * Time.deltaTime;

        // Continua a eseguire finché alpha è maggiore di 0
        while (_playerBubbleColor.a > 0) {
            // Decrementa il valore di alpha
            _playerBubbleColor.a -= fadeStep;
            playerBubble.color = _playerBubbleColor;

            // Aspetta il prossimo frame
            yield return null;
        }

        // Assicurati che alpha sia esattamente 0 alla fine
        _playerBubbleColor.a = 0;
        playerBubble.color = _playerBubbleColor;
        bubbleShow = false;
    }

    private IEnumerator ShowBubble() {
        // Calcola quanto aumentare alpha in ogni frame basandosi sulla durata del fade
        float fadeStep = 1f * Time.deltaTime;

        // Continua a eseguire finché alpha è maggiore di 0
        while (_playerBubbleColor.a <= 0.5f) {
            // aumenta il valore di alpha
            _playerBubbleColor.a += fadeStep;
            playerBubble.color = _playerBubbleColor;

            // Aspetta il prossimo frame
            yield return null;
        }

        // Assicurati che alpha sia esattamente 100 alla fine
        _playerBubbleColor.a = 0.5f;
        playerBubble.color = _playerBubbleColor;
        bubbleShow = true;
        // aspetta prima di togliere la bubble
        yield return new WaitForSeconds(bubbleCooldown);
        // togli la bubble
        StartCoroutine(NotShowBubble());
    }
}