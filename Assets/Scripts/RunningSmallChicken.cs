using UnityEngine;

public class RunningSmallChicken : MonoBehaviour
{
    public float speed = 2f;
    public float changeDirectionTime = 2f;
    public float moveRange = 1f;

    private Vector3 direction;
    private float timer;

    public AudioClip spawnSound;
    public AudioClip[] cluckSounds;
    public AudioSource audioSource;

    void Start()
    {
        SetRandomDirection();
        SetRandomDirection();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= changeDirectionTime)
        {
            SetRandomDirection();
        }

        transform.position += direction * speed * Time.deltaTime;

        // opcjonalnie: żeby kurczak patrzył w stronę ruchu
        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * 5f
            );
        }

        HandleAudio();
    }

    void SetRandomDirection()
    {
        timer = 0f;

        float randomX = Random.Range(-1f, 1f);
        float randomZ = Random.Range(-1f, 1f);

        direction = new Vector3(randomX, 0f, randomZ).normalized;
    }

    private void HandleAudio()
    {
        if (cluckSounds.Length == 0) return;

        if (!audioSource.isPlaying)
        {
            PlayRandomCluck();
        }
    }

    void PlayRandomCluck()
    {
        int randomIndex = Random.Range(0, cluckSounds.Length);

        audioSource.clip = cluckSounds[randomIndex];
        audioSource.Play();
    }
}