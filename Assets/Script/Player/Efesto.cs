using UnityEngine;

namespace Script.Player {
    public class Efesto : MonoBehaviour {
        // static attributes for animator
        private static readonly int Run = Animator.StringToHash("Run");

        [SerializeField] private GameObject powerUpPanel;
        [SerializeField] private PlayerMovement playerMovement;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private CanvasGroup interactionPanel;

        private bool _powerUpClosedBtn = true;

        private void Awake() {
            ResetScene();
            interactionPanel.alpha = 0;
            playerAnimator.SetBool(Run, false);
        }

        private void Update() {
            if (_powerUpClosedBtn) ResetScene();
        }

        private void OnCollisionEnter2D(Collision2D other) {
            interactionPanel.alpha = 1;
        }

        private void OnCollisionExit2D(Collision2D other) {
            interactionPanel.alpha = 0;
        }

        private void OnCollisionStay2D(Collision2D other) {
            if (other.gameObject.CompareTag("Player") &&
                (Input.GetKeyDown(KeyCode.F) || (Input.anyKey && Input.GetKey(KeyCode.F)))) {
                powerUpPanel.SetActive(true);
                interactionPanel.alpha = 0; // Nascondere il messaggio
                _powerUpClosedBtn = false;
                playerMovement.CanNotMove();
                playerAnimator.SetBool(Run, false);
                playerMovement.StopMovement();
            }
        }

        private void ResetScene() {
            powerUpPanel.SetActive(false);
            playerMovement.CanMove();
        }

        public void TooglePowerUpCloseBtn() {
            _powerUpClosedBtn = !_powerUpClosedBtn;
        }
    }
}
