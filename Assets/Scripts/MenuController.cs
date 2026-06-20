using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    public void ChangeScene(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
    }    

    public void Quit()
    {
        Application.Quit();
    }

    public void HardRestartGame()
    {
        if (GameManager.Instance != null)
        {
            Destroy(GameManager.Instance.gameObject);
        }

        SceneManager.LoadScene("Level 1");
    }

}
