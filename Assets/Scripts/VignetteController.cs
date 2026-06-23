using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using System.Collections;

public class VignetteController : MonoBehaviour
{
    [Header("Volume Reference")]
    public Volume volume;

    [Header("Animation")]
    public float fadeInDuration = 0.2f;
    public float holdDuration = 0.5f;
    public float fadeOutDuration = 0.8f;

    [Header("Intensity")]
    [Range(0f, 1f)] public float maxIntensity = 0.85f;

    private Vignette vignette;
    private Coroutine vignetteCoroutine;

    void Start()
    {
        if (volume != null && volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0f;
        }
        else
        {
            Debug.LogError("Vignette not found in Volume Profile!");
        }
    }

    public void TriggerVignette()
    {
        if (vignetteCoroutine != null)
            StopCoroutine(vignetteCoroutine);

        vignetteCoroutine = StartCoroutine(VignetteSequence());
    }

    private IEnumerator VignetteSequence()
    {
        yield return StartCoroutine(FadeVignette(0f, maxIntensity, fadeInDuration));

        yield return new WaitForSeconds(holdDuration);

        yield return StartCoroutine(FadeVignette(maxIntensity, 0f, fadeOutDuration));
    }

    private IEnumerator FadeVignette(float from, float to, float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float t = elapsed / duration;
            vignette.intensity.value = Mathf.Lerp(from, to, t);

            yield return null;
        }

        vignette.intensity.value = to;
    }
}