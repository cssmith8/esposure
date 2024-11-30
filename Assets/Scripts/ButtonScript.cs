using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour {
    public Color normalColor = Color.white; // Default color of the button
    public Color hoverColor = Color.grey; // Color when hovered
    public Color clickColor = Color.green; // Color when clicked
    public UnityEvent onClick;

    [SerializeField] private List<SpriteRenderer> ButtonSprites;

    private void changeColor(Color color) {
        foreach (var sr in ButtonSprites) {
            sr.color = color; // Set the default color
        }
    }

    private void Start() {
        changeColor(normalColor);
    }
    
    private void OnMouseEnter() {
        changeColor(hoverColor);
    }
    
    private void OnMouseExit() {
        changeColor(normalColor);
    }
    
    private void OnMouseDown() {
        changeColor(clickColor);
        onClick?.Invoke();
    }
}