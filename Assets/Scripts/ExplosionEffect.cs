using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float lifeTime = 0.5f;

    private Transform mainCameraTransform;

    void Start()
    {
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning("Brak kamery z tagiem 'MainCamera'. Billboard mo¿e nie dzia³aæ.");
        }

        Destroy(gameObject, lifeTime);
    }

    void LateUpdate()
    {
        if (mainCameraTransform != null)
        {
            transform.forward = mainCameraTransform.forward;
        }
    }
}