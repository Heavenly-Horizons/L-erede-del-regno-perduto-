using System.Collections;
using UnityEngine;

public class activatePenWritingSound : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake() {
        if(!gameObject.activeSelf) {
            StartCoroutine(activateSound());
        }
    }

    IEnumerator activateSound(){
        yield return new WaitForSeconds(2);
        gameObject.SetActive(true);
    }
}
