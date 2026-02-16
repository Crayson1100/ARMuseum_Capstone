using UnityEngine;

public class PrefabUI : MonoBehaviour
{
    [SerializeField] private GameObject[] displays;

    private int currentDisplay = 0;

    void Start()
    {
        if (displays.Length > 0)
            ActivateDisplay(0);
    }

    public void Next()
    {
        if (displays.Length == 0) return;

        int nextIndex = (currentDisplay + 1) % displays.Length;
        ActivateDisplay(nextIndex);
    }

    public void Previous()
    {
        if (displays.Length == 0) return;

        int prevIndex = (currentDisplay - 1 + displays.Length) % displays.Length;
        ActivateDisplay(prevIndex);
    }

    private void ActivateDisplay(int index)
    {
        currentDisplay = index;

        for (int i = 0; i < displays.Length; i++)
            displays[i].SetActive(i == currentDisplay);
    }
}
