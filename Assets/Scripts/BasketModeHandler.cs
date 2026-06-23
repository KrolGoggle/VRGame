using UnityEngine;

public class BasketToggleHandler : MonoBehaviour
{
    [Header("Ustawienia Koszyka")]
    public GameObject basketObject;
    public Vector3 resetPosition;
    public Quaternion resetRotation;

    public void ResetPosition()
    {
            basketObject.transform.localPosition = resetPosition;
            basketObject.transform.localRotation = resetRotation;

        Rigidbody rb = basketObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
    }
}