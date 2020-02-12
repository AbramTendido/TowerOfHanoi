using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour 
{ 

    public TextMeshProUGUI diskAmountText;
    public DiskAmount diskAmountSO;
    public CurrentScene currentScene;

    // Start is called before the first frame update
    void Start()
    {
        currentScene.scene = Scene.MainMenu;
        AudioManager.instance.PlayBG();
        DisplayDiskAmount();
    }

    private void DisplayDiskAmount()
    {
        diskAmountText.text = diskAmountSO.diskAmount.ToString();
    }

    public void AddDisk()
    {
        if(diskAmountSO.diskAmount < 18)
        {
            diskAmountSO.diskAmount++;
            DisplayDiskAmount();
        }
        
    }

    public void SubtractDisk()
    {
        if(diskAmountSO.diskAmount > 1)
        {
            diskAmountSO.diskAmount--;
            DisplayDiskAmount();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }
}
