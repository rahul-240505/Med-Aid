using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolSelectionMenu : MonoBehaviour
{
    private Button btn;
    public int toolIndex;
    private bool isPressed = false; // To track if the button is in the "pressed" state
    public Color normalColor = Color.white; // Default color
    public Color pressedColor = Color.gray; // Color when pressed

    void Start()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnToolSelectionBtnClick);

        // Set the initial color of the button
        UpdateButtonColor(normalColor);
    }

    void OnToolSelectionBtnClick()
    {
        // If the button is already pressed, don't allow it to change back
        if (isPressed)
        {
            return; // Do nothing if already pressed
        }

        // Mark the button as pressed
        isPressed = true;

        // Set the button to the pressed state and update its appearance
        UpdateButtonColor(pressedColor);

        // Call the tool selection logic
        FirstAidToolManager.Instance.SelectTool(toolIndex);
    }

    // Method to update button colors immediately
    void UpdateButtonColor(Color color)
    {
        var colors = btn.colors;
        colors.normalColor = color;
        colors.selectedColor = color; // Ensures the color is applied when selected
        btn.colors = colors;

        // Force the button to refresh its visual state immediately
        btn.image.color = color;
    }
}
