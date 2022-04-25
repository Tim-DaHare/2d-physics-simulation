using UnityEngine;

public class BounceBallLeft : MonoBehaviour
{
    float speed = 0.0f;

    public GameObject area;
    private Bounds areaBounds;

    private Bounds ballBounds;
    
    void Start()
    {
        areaBounds = area.GetComponent<Renderer>().bounds;
        ballBounds = GetComponent<Renderer>().bounds;
    }

    void FixedUpdate()
    {
        
        float new_speed = speed - 9.81f * Time.deltaTime;
        float new_position = transform.position.y + new_speed * Time.deltaTime;
        if (new_position - ballBounds.size.y / 2 < areaBounds.min.y)
        {
            new_position = areaBounds.min.y + ballBounds.size.y / 2;
            new_speed = -new_speed;
            new_speed *= 0.85f;
        }

        transform.position = new Vector3(transform.position.x, new_position, transform.position.z);
        speed = new_speed;
    }
}