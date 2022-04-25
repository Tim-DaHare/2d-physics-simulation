using UnityEngine;

public class BounceBallTopRight : MonoBehaviour
{
    public GameObject area;
    public GameObject bottomBall;

    private Bounds _areaBounds;
    private Bounds _ballBounds;
    private Bounds _bottomBallBounds;
        
    public float speed;
    public float mass = 1.0f;

    void Start()
    {
        _areaBounds = area.GetComponent<Renderer>().bounds;
        _ballBounds = GetComponent<Renderer>().bounds;
        _bottomBallBounds = bottomBall.GetComponent<Renderer>().bounds; 
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get location bottom_ball
        // float bottom_ball_x = bottomBall.transform.position.x;

        float velocity = speed - 9.81f * Time.deltaTime;
        float pos = transform.position.y + velocity * Time.deltaTime;

        if (pos + _ballBounds.size.y / 2 > _areaBounds.max.y)
        {
            pos = _areaBounds.max.y - _ballBounds.size.y / 2;
            velocity = -velocity;
        }

        if (pos - _ballBounds.size.y / 2 < bottomBall.transform.position.y + _bottomBallBounds.size.y / 2)
        {
            float bottom_ball_mass = bottomBall.GetComponent<BounceBallBottomRight>().mass;
            float bottom_ball_speed = bottomBall.GetComponent<BounceBallBottomRight>().velocity;

            pos = bottomBall.transform.position.y + _bottomBallBounds.size.y / 2 + _ballBounds.size.y / 2;

            float updated_speed = (bottom_ball_mass * bottom_ball_speed * (1 + 1) + velocity * (mass - bottom_ball_mass * 1))/(mass + bottom_ball_mass);
            bottomBall.GetComponent<BounceBallBottomRight>().velocity = (mass * velocity * (1 + 1) + bottom_ball_speed * (bottom_ball_mass - mass * 1))/(mass + bottom_ball_mass);
            
            velocity = updated_speed;
        }

        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
        speed = velocity;
    }
}
