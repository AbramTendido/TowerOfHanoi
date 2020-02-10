using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public TextMeshProUGUI movesText;
    public TextMeshProUGUI bestMovesText;
    public GameObject ConfirmationMenu;

    public DiskAmount diskAmountSO;
    public MoveCount moveCountSO;
    

    // Start is called before the first frame update
    void Start()
    {
        MovesTextUpdate();
        BestMovesText();
    }
    // Update is called once per frame
    void Update()
    {
        MovesTextUpdate();
    }

    private void MovesTextUpdate()
    {
        movesText.text = moveCountSO.moveCount.ToString();
    }

    private void BestMovesText()
    {
        bestMovesText.text = (Mathf.Pow(2, diskAmountSO.diskAmount) - 1).ToString();
    }

    public void BackToMenuButton()
    {
        ConfirmationMenu.SetActive(true);
    }

    public void ConfirmYes()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ConfirmNo()
    {
        ConfirmationMenu.SetActive(false);
    }
}
