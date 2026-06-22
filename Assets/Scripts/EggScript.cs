using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RollingEggSound : MonoBehaviour
{
    [Range(0f, 1f)]
    public float masterVolume = 0.5f;
    public float maxSpeed = 5f;
    public float maxPitch = 1.3f;
    public float minPitch = 0.8f;

    private AudioSource audioSource;
    private Vector3 lastPosition;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.Play();

        lastPosition = transform.position;
    }

    void Update()
    {
        float distanceMoved = Vector3.Distance(transform.position, lastPosition);
        float currentSpeed = distanceMoved / Time.deltaTime;
        lastPosition = transform.position;

        if (currentSpeed > 0.05f)
        {
            float targetVolume = Mathf.Clamp01(currentSpeed / maxSpeed) * masterVolume;
            audioSource.volume = Mathf.Lerp(audioSource.volume, targetVolume, Time.deltaTime * 10f);

            float targetPitch = Mathf.Lerp(minPitch, maxPitch, currentSpeed / maxSpeed);
            audioSource.pitch = Mathf.Lerp(audioSource.pitch, targetPitch, Time.deltaTime * 10f);
        }
        else
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 15f);
        }
    }
}