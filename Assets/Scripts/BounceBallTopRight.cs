using UnityEngine;

public class BounceBallTopRight : MonoBehaviour
{
    public GameObject area;
    public GameObject bottomBall;

    private Bounds _areaBounds;
    private Bounds _ballBounds;
    private Bounds _bottomBallBounds;
        
    public float speed = 0F;
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
        float bottom_ball_x = bottomBall.transform.position.x;

        float new_speed = speed - 9.81f * Time.deltaTime;
        float new_position = transform.position.y + new_speed * Time.deltaTime;

        if (new_position + _ballBounds.size.y / 2 > _areaBounds.max.y)
        {
            new_position = _areaBounds.max.y - _ballBounds.size.y / 2;
            new_speed = -new_speed;
        }

        if (new_position - _ballBounds.size.y / 2 < bottomBall.transform.position.y + _bottomBallBounds.size.y / 2)
        {
            float bottom_ball_mass = bottomBall.GetComponent<BounceBallBottomRight>().mass;
            float bottom_ball_speed = bottomBall.GetComponent<BounceBallBottomRight>().speed;

            new_position = bottomBall.transform.position.y + _bottomBallBounds.size.y / 2 + _ballBounds.size.y / 2;

            float updated_speed = (bottom_ball_mass * bottom_ball_speed * (1 + 1) + new_speed * (mass - bottom_ball_mass * 1))/(mass + bottom_ball_mass);
            bottomBall.GetComponent<BounceBallBottomRight>().speed = (mass * new_speed * (1 + 1) + bottom_ball_speed * (bottom_ball_mass - mass * 1))/(mass + bottom_ball_mass);
            new_speed = updated_speed;
        }

        transform.position = new Vector3(transform.position.x, new_position, transform.position.z);
        speed = new_speed;
    }
}
