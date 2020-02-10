using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour 
{ 

    public TextMeshProUGUI diskAmountText;
    public DiskAmount diskAmountSO;

    // Start is called before the first frame update
    void Start()
    {
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
