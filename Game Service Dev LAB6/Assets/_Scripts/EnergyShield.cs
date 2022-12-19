using UnityEngine;
using TMPro;

public class EnergyShield : MonoBehaviour
{
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI teleportText;
    public TextMeshProUGUI fastShieldText;
    //public TextMeshProUGUI AddShieldText;
    public TextMeshProUGUI scoreGT;
    public AudioSource audioSource;

    public float _velocity = 10;

    public int slowDownTime = 5;
    public int teleportShield = 5;
    public int fastShield = 5;
    //public int addShield = 3;
    public bool slowDownTimeActive = false;
    public bool fastShieldActive = false;
    private float timeSlowDowm = 0;
    private float timefastShield = 0;

    private void Start()
    {
        GameObject timeTextObject = GameObject.Find("Time");
        GameObject teleportTextObject = GameObject.Find("Teleport");
        GameObject fastShieldObject = GameObject.Find("FastShield");
        //GameObject AddShieldObject = GameObject.Find("AddShield");
        GameObject scoreGO = GameObject.Find("Score");

        timeText = timeTextObject.GetComponent<TextMeshProUGUI>();
        teleportText = teleportTextObject.GetComponent<TextMeshProUGUI>();
        fastShieldText = fastShieldObject.GetComponent<TextMeshProUGUI>();
        //AddShieldText = AddShieldObject.GetComponent<TextMeshProUGUI>();
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

        if (slowDownTimeActive && !fastShieldActive) transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * _velocity * 5);
        else if (fastShieldActive) transform.position = pos;
        else transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime * _velocity);

        Pause pause = Camera.main.GetComponent<Pause>();
        if (pause.paused == false && slowDownTimeActive) timeSlowDowm += Time.fixedDeltaTime;
        if(timeSlowDowm > 10)
        {
            Time.timeScale = 1;
            slowDownTimeActive = false;
            timeSlowDowm = 0;
        }

        if (pause.paused == false && fastShieldActive) timefastShield += Time.fixedDeltaTime;
        if(timefastShield > 10)
        {
            fastShieldActive = false;
            timefastShield = 0;
        }

        if (Input.GetKeyDown(KeyCode.Q) && slowDownTime > 0 && !slowDownTimeActive)
        {
            SlowDownTime();
        }

        if (Input.GetKeyDown(KeyCode.W) && teleportShield > 0 && !fastShieldActive)
        {
            TeleportShield(pos);
        }

        if (Input.GetKeyDown(KeyCode.E) && fastShield > 0 && !fastShieldActive)
        {
            FastShield();
        }

        //DragonPicker DP = Camera.main.GetComponent<DragonPicker>();
        //if (Input.GetKeyDown(KeyCode.E) && addShield > 0 && DP.shieldList.Count < 3)
        //{
        //    AddShield();
        //}
    }

    public void SlowDownTime()
    {
        slowDownTime--;
        Time.timeScale = 0.2f;
        slowDownTimeActive = true;

        timeText.text = string.Format("Slow Down Time: {0}", slowDownTime);
    }

    public void TeleportShield(Vector2 pos)
    {
        teleportShield--;
        transform.position = pos;

        teleportText.text = string.Format("Teleport: {0}", teleportShield);
    }

    public void FastShield()
    {
        fastShield--;
        fastShieldActive = true;

        fastShieldText.text = string.Format("Fast Shield: {0}", fastShield);
    }

    //public void AddShield()
    //{
    //    addShield--;
    //    DragonPicker DP = Camera.main.GetComponent<DragonPicker>();
    //    GameObject tShieldGo = Instantiate(DP.energyShieldPrefab);
    //    tShieldGo.transform.position = new(transform.position.x, DP.energyShieldBottomY, 0);
    //    tShieldGo.transform.localScale = new(1 + DP.shieldList.Count, 1 + DP.shieldList.Count, 1 + DP.shieldList.Count);
    //    DP.shieldList.Add(tShieldGo);
    //    tShieldGo.GetComponent<EnergyShield>().addShield = DP.shieldList[0].GetComponent<EnergyShield>().addShield;

    //    AddShieldText.text = string.Format("Add shield: {0}", addShield);
    //}

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
