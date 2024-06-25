using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public string objectID;
    
    private void Awake() {
        objectID = name + transform.position.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < FindObjectsOfType<DontDestroy>().Length; i++){
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
