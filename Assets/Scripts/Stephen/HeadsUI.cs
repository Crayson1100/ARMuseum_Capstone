using UnityEngine;

public class HeadsUI : MonoBehaviour
{
    [SerializeField] GameObject headPanel;

    private void Awake()
    {
        headPanel.SetActive(false);
    }

    public void DisplayUI()
    {
        //click displayButtton and opens headPanel
        headPanel.SetActive(true);
    }
    public void ExitPanel()
    {
        //click xButton to exit from headPanel
        headPanel.SetActive(false);
    }
}
