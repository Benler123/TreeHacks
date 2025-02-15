using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class ManageHUD : MonoBehaviour
{
    [Header("HUD Elements")]
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private float hudDistance = 1f;
    [SerializeField] private float hudScale = 0.001f;
    
    private Transform cameraTransform;
    private Vector3 originalHudPosition;
    private bool isHUDVisible = true;

    void Start()
    {
        // Get the VR camera reference
        cameraTransform = Camera.main.transform;
        
        // Initialize HUD
        SetupHUD();
        
        // Store original position
        originalHudPosition = hudCanvas.transform.localPosition;
        
    }

    void SetupHUD()
    {
        // Make sure the canvas is in world space
        hudCanvas.renderMode = RenderMode.WorldSpace;
        
        // Set the canvas scale
        hudCanvas.transform.localScale = Vector3.one * hudScale;
        
        // Configure canvas settings for VR
        var canvasSettings = hudCanvas.GetComponent<Canvas>();
        if (canvasSettings != null)
        {
            // canvasSettings.scaleFactor = 1000f;
            canvasSettings.referencePixelsPerUnit = 1000f;
        }
    }

    void Update()
    {
        if (isHUDVisible)
        {
            // Update HUD position to follow camera
            UpdateHUDPosition();
        }
    }

    void UpdateHUDPosition()
    {
        // Position HUD in front of camera
        Vector3 targetPosition = cameraTransform.position + (cameraTransform.forward * hudDistance);
        hudCanvas.transform.position = targetPosition;
        
        // Make HUD face the camera
        hudCanvas.transform.rotation = Quaternion.LookRotation(
            hudCanvas.transform.position - cameraTransform.position
        );
    }

}
