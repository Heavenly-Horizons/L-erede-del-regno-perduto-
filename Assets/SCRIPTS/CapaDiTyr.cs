using UnityEngine;

public class CapaDiTyr : MonoBehaviour
{
    PlayerMovement playerMovement;
    Rigidbody2D playerRB2D;
    Vector2 headKnockDirection;
     void Awake(){
        playerRB2D = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collider){
        if(collider.gameObject.CompareTag("Player")){
            Debug.Log("Ho colpito il player");
            Vector3 direction = (transform.position - collider.transform.position).normalized;
            playerRB2D.AddForce(direction * playerMovement.KBForce);
        }
    }
}
