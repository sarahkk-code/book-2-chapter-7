using UnityEngine;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    public TMP_Dropdown colorDropdown;
    public GameObject colorCube;

    void Start()
    {
        // Set initial cube color to blue
        SetCubeColor(Color.blue);

        // Add listener for dropdown changes
        colorDropdown.onValueChanged.AddListener(ChangeColor);
    }

    void ChangeColor(int index)
    {
        // Ignore index 0 ("Select the color for the box")
        if (index == 0) return;

        Color selectedColor = Color.blue; // Default fallback

        switch (index)
        {
            case 1: selectedColor = Color.red; break;
            case 2: selectedColor = Color.white; break;
            case 3: selectedColor = Color.blue; break;
        }

        SetCubeColor(selectedColor);
    }

    void SetCubeColor(Color color)
    {
        Renderer cubeRenderer = colorCube.GetComponent<Renderer>();
        cubeRenderer.material.color = color;
    }
}
