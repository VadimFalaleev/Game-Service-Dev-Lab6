using UnityEngine;

public class EnemyDragon : MonoBehaviour
{
    public GameObject dragonEggPrefab;
    public float speed;
    public float timeBetweenEggDrops;
    public float leftRightDistance = 10f;
    public float chanceDirection = 0.01f;

    private float time = 0;
    private float gravityScale = -9.81f;
    private int count = 0;

    private void Start()
    {
        Invoke("DropEgg", 2f);
    }

    void DropEgg()
    {
        Vector3 myVector = new (0f, 5f, 0f);
        GameObject egg = Instantiate(dragonEggPrefab);
        egg.transform.position = transform.position + myVector;
        Invoke("DropEgg", timeBetweenEggDrops);
    } 

    private void Update()
    {
        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (pos.x < -leftRightDistance)
            speed = Mathf.Abs(speed);
        else if (pos.x > leftRightDistance)
            speed = -Mathf.Abs(speed);

        Pause pause = Camera.main.GetComponent<Pause>();
        if(pause.paused == false) time += Time.deltaTime;
        if(time >= 30 && count < 10)
        {
            if (speed < 0) speed -= 1;
            if (speed > 0) speed += 1;

            timeBetweenEggDrops -= 0.1f;

            gravityScale -= 2;
            Physics.gravity = new Vector3(0, gravityScale, 0);

            count++;
            time = 0;
        }
    }

    private void FixedUpdate()
    {
        if (Random.value < chanceDirection)
            speed *= -1;
    }
}