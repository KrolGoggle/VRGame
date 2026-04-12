using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class TeleportationActivator : MonoBehaviour
{

    public XRRayInteractor teleportInteractor;
    public XRRayInteractor rayInteractor;
    public InputActionProperty teleportActivatorAction;

    public 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        teleportInteractor.gameObject.SetActive(false);

        teleportActivatorAction.action.performed += ActionPerformed;

        rayInteractor.uiHoverEntered.AddListener(x => DisableTeleportRay());
    }

    private void ActionPerformed(InputAction.CallbackContext context)
    {   
        if(rayInteractor && rayInteractor.IsOverUIGameObject())
        {
            return;
        }

        teleportInteractor.gameObject.SetActive(true);
    }

    public void DisableTeleportRay()
    {
        teleportInteractor.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (teleportActivatorAction.action.WasReleasedThisFrame()) { 
            teleportInteractor.gameObject.SetActive(false);
        }
    }
}
