using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public string objectID;
    
    // Start is called before the first frame update
    void Start()
    {
        objectID = name + transform.position.ToString();
        for (int i = 0; i < FindObjectsOfType<DontDestroy>().Length; i++){
            if(FindObjectsOfType<DontDestroy>()[i] != this && FindObjectsOfType<DontDestroy>()[i].objectID == objectID){
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }

    public void SelfDestroy(){
        Destroy(gameObject);
    }
}
