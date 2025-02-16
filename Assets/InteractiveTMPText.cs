using UnityEngine;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class InteractiveTMPText : MonoBehaviour
{
    [SerializeField] private TextMeshPro tmpText;
    private BoxCollider textCollider;
    private XRSimpleInteractable interactable;
    
    void Start()
    {
        // Add necessary components if they don't exist
        if (tmpText == null)
            tmpText = GetComponent<TextMeshPro>();
            
        // Add collider for interaction
        textCollider = gameObject.AddComponent<BoxCollider>();
        
        // Size the collider to match the text
        UpdateColliderSize();
        
        // Add XR interactable
        interactable = gameObject.AddComponent<XRSimpleInteractable>();
        
        // Setup interaction events
        interactable.selectEntered.AddListener(OnSelect);
        interactable.hoverEntered.AddListener(OnHoverEnter);
        interactable.hoverExited.AddListener(OnHoverExit);
    }
    
    private void UpdateColliderSize()
    {
        if (tmpText != null && textCollider != null)
        {
            // Get text bounds
            Vector3 bounds = tmpText.bounds.size;
            textCollider.size = bounds;
            
            // Center the collider on the text
            textCollider.center = tmpText.bounds.center - transform.position;
        }
    }
    
    private void OnSelect(SelectEnterEventArgs args)
    {
        // Called when text is selected (clicked)
        Debug.Log("Text selected: " + tmpText.text);
        
        // Example: Change text color when selected
        tmpText.color = Color.green;
    }
    
    private void OnHoverEnter(HoverEnterEventArgs args)
    {
        // Called when pointer hovers over text
        tmpText.fontSize += 2; // Make text slightly bigger
    }
    
    private void OnHoverExit(HoverExitEventArgs args)
    {
        // Called when pointer stops hovering
        tmpText.fontSize -= 2; // Return to original size
        tmpText.color = Color.white; // Reset color
    }
    
    // Call this when you change the text content
    public void UpdateText(string newText)
    {
        tmpText.text = newText;
        UpdateColliderSize(); // Update collider to match new text size
    }
}