using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNewSceneOnSpacePress : MonoBehaviour
{
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex < 10)
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        }    
        else if (Input.GetKeyDown(KeyCode.Space) && SceneManager.GetActiveScene().buildIndex == 10)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }
}
