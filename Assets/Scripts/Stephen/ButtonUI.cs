using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> textPanels = new List<GameObject>();

    public void ShowUI(int index)
    {
        if (index >= 0 && index < textPanels.Count)
        {
            textPanels[index].SetActive(true);
        }
    }

    public void HideUI(int index)
    {
        if (index >= 0 && index < textPanels.Count)
        {
            textPanels[index].SetActive(false);
        }
    }
}
