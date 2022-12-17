using UnityEngine;

public class DragonEgg : MonoBehaviour
{
    public static float bottomY = -14f;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        var em = ps.emission;
        em.enabled = true;

        Renderer rend = GetComponent<Renderer>();
        rend.enabled = false;

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

    private void Update()
    {
        if (transform.position.y < bottomY)
        {
            Destroy(this.gameObject);
            DragonPicker apScript = Camera.main.GetComponent<DragonPicker>();
            apScript.DragonEggDestroyed();
        }
    }
}