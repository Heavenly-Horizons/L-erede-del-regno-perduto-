using System.Collections;
using UnityEngine;

public class activatePenWritingSound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        if(!gameObject.activeSelf) {
            StartCoroutine(activateSound());
        }
    }

    IEnumerator activateSound(){
        yield return new WaitForSeconds(2);
        gameObject.SetActive(true);
    }
}
