using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    private SpriteRenderer sr;
    public Color normalColor = Color.white;  // Default color of the button
    public Color hoverColor = Color.grey;    // Color when hovered
    public Color clickColor = Color.green;   // Color when clicked
    public UnityEvent onClick;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = normalColor;  // Set the default color
    }

    // This is called when the mouse enters the collider
    private void OnMouseEnter()
    {
        sr.color = hoverColor;  // Change color when hovering
    }

    // This is called when the mouse exits the collider
    private void OnMouseExit()
    {
        sr.color = normalColor;  // Revert back to the normal color
    }

    // This is called when the mouse clicks on the collider
    private void OnMouseDown() {
        sr.color = clickColor; // Change color on click
        Debug.Log("Button Clicked!"); // Perform your button action here
        onClick?.Invoke();
    }
}