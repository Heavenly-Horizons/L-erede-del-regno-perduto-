using UnityEngine;

public class DropHeal : MonoBehaviour
{
    public GameObject healing_hidromele;  // Il prefab della moneta
    public float dropForce = 5f;   // Forza verso l'alto
    public float randomForce = 10f; // Forza random laterale

    public void Drop(int healingsToDrop)
    {
        for(int i = 0; i < healingsToDrop; i++){
            GameObject hidromele = Instantiate(healing_hidromele, transform.position, Quaternion.identity);

            Rigidbody2D rb = hidromele.GetComponent<Rigidbody2D>();

            float randomDirection = Random.Range(-1f, 1f) * randomForce;

            rb.AddForce(new Vector2(randomDirection, dropForce), ForceMode2D.Impulse);
        }
    }
}
