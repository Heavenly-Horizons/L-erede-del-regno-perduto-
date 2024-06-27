using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public Transform player;
    public bool isFlipped = false;

    void Awake(){
        gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;  // Blocca la rotazione sull'asse Z
        gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    public void StopMovementCall(){
        StartCoroutine(StopMovement());
    }

    private IEnumerator StopMovement()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        yield return new WaitForSeconds(0.5f);
    }
}
