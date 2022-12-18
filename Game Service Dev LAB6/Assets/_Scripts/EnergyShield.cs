using UnityEngine;
using TMPro;

public class EnergyShield : MonoBehaviour
{
    public TextMeshProUGUI scoreGT;
    public AudioSource audioSource;

    public float _velocity = 10;

    private void Start()
    {
        GameObject scoreGO = GameObject.Find("Score");
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        DragonPicker DP = Camera.main.GetComponent<DragonPicker>();

        if (DP.died) 
        {
            TextMeshProUGUI score = DP.GetScore();
            scoreGT.text = score.text;
        }
        else
        {
            scoreGT.text = "0";
        }
    }

    void Update()
    {
        Vector2 mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = transform.position;
        pos.x = mousePos3D.x;
        transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * _velocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collided = collision.gameObject;
        if (collided.tag == "Dragon Egg")
            Destroy(collided);

        int score = int.Parse(scoreGT.text);
        score += 1;
        scoreGT.text = score.ToString();

        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }
}
