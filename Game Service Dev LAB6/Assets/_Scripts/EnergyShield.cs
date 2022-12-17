using UnityEngine;
using TMPro;
using System.Collections;

public class EnergyShield : MonoBehaviour
{
    public TextMeshProUGUI scoreGT;
    public AudioSource audioSource;

    private RectTransform rectTransform; // new

    private void Start()
    {
        GameObject scoreGO = GameObject.Find("Score");
        scoreGT = scoreGO.GetComponent<TextMeshProUGUI>();
        scoreGT.text = "0";

        rectTransform = GetComponent<RectTransform>(); // new
    }

    void Update()
    {
        //Vector3 mousePos2D = Input.mousePosition;
        //mousePos2D.z = -Camera.main.transform.position.z;


        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        //this.transform.position = pos;

        if(this.transform.position != pos)
        {
            StartCoroutine(DoMove(3f, Vector2.zero));
        }
    }

    private IEnumerator DoMove(float time, Vector2 targetPosition)
    {
        Vector2 startPosition = rectTransform.anchoredPosition; 
        float startTime = Time.realtimeSinceStartup; 
        float fraction = 0f;

        while (fraction < 1f) 
        { 
            fraction = Mathf.Clamp01((Time.realtimeSinceStartup - startTime) / time); 
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, fraction); 
            yield return null; 
        }
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
