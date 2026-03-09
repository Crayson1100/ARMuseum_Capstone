using UnityEngine;
using UnityEngine.UI;


public class ButtonUI : MonoBehaviour
{
    public GameObject textPanel;
    public void ShowUI()
    {
        textPanel.SetActive(true);
    }
    public void HideUI()
    {
        textPanel.SetActive(false);
    }
}
