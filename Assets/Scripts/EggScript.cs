using UnityEngine;

public class EggScript : MonoBehaviour
{
    public AudioClip clip;
    private AudioSource source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        source.PlayOneShot(clip);
    }
}
