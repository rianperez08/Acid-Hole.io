using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{

    public string sceneToLoad;

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}