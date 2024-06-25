using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < FindObjectsOfType<DontDestroy>().Length; i++){
            FindObjectsOfType<DontDestroy>()[i].SelfDestroy();
        }
        
    }
}
