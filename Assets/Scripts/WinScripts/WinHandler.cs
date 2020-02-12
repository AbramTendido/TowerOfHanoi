using UnityEngine;
using UnityEngine.SceneManagement;

public class WinHandler : MonoBehaviour
{
    public CurrentScene currentScene;
    private void Start()
    {
        currentScene.scene = Scene.Win;
        AudioManager.instance.PlayBG();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
