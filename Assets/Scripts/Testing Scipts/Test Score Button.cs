using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TestScoreButton : MonoBehaviour
{
    [Header("Ustawienia przycisku")]
    public float cooldownTime = 4f;
    public Material readyMaterial;
    public Material cooldownMaterial;
    public string handTag = "PlayerHand";

    [Header("Co ma siê staæ po wciœniêciu?")]
    public UnityEvent onPressed;

    private bool isOnCooldown = false;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null && readyMaterial != null)
        {
            meshRenderer.material = readyMaterial;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isOnCooldown && other.CompareTag(handTag))
        {
            StartCoroutine(PressButtonRoutine());
        }
    }

    private IEnumerator PressButtonRoutine()
    {
        isOnCooldown = true;

        if (meshRenderer != null && cooldownMaterial != null)
            meshRenderer.material = cooldownMaterial;

        transform.position -= new Vector3(0, 0.05f, 0);

        onPressed.Invoke();

        yield return new WaitForSeconds(0.2f);
        transform.position += new Vector3(0, 0.05f, 0);

        yield return new WaitForSeconds(cooldownTime - 0.2f);

        if (meshRenderer != null && readyMaterial != null)
            meshRenderer.material = readyMaterial;

        isOnCooldown = false;
    }
}