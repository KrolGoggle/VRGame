using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VignetteController : MonoBehaviour
{
    [Header("Vignette Image")]
    public Image vignetteImage;

    [Header("Animation")]
    public float fadeInDuration = 0.2f;
    public float holdDuration = 0.5f;
    public float fadeOutDuration = 0.8f;

    [Header("Intensity")]
    [Range(0f, 1f)] public float maxAlpha = 0.85f;

    private Coroutine vignetteCoroutine;

    void Start()
    {
        if (vignetteImage != null)
        {
            Color c = vignetteImage.color;
            c.a = 0f;
            vignetteImage.color = c;
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
        // Fade IN
        yield return StartCoroutine(FadeVignette(0f, maxAlpha, fadeInDuration));

        // Hold
        yield return new WaitForSeconds(holdDuration);

        // Fade OUT
        yield return StartCoroutine(FadeVignette(maxAlpha, 0f, fadeOutDuration));
    }

    private IEnumerator FadeVignette(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color c = vignetteImage.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            vignetteImage.color = c;
            yield return null;
        }

        c.a = to;
        vignetteImage.color = c;
    }
}