using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Riferimento all'oggetto giocatore
    public Vector3 offset = new Vector3(0, 0, -10); // Regola la posizione della telecamera rispetto al giocatore
    public float minX = -10f; // Limite sinistro della telecamera
    public float maxX = 10f; // Limite destro della telecamera
    public float fixedY = 0f; // Posizione verticale fissa della telecamera

    void LateUpdate()
    {
        if (player != null)
        {
            // Aggiorna la posizione della telecamera per seguire il giocatore
            Vector3 targetPosition = player.position + offset;
            targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX); // Limita la posizione x tra minX e maxX
            targetPosition.y = fixedY; // Imposta la posizione y su un valore fisso
            transform.position = targetPosition;
        }
    }
}
