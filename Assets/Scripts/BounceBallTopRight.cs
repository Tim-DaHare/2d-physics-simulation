using UnityEngine;

public class BounceBallTopRight : MonoBehaviour
{
    public GameObject area;
    public GameObject bottomBall;

    private Bounds _areaBounds;
    private Bounds _ballBounds;
    private Bounds _bottomBallBounds;
        
    public float velocity;
    public float mass = 1.0f;

    private BounceBallBottomRight _botRightBall;

    void Start()
    {
        _areaBounds = area.GetComponent<Renderer>().bounds;
        _ballBounds = GetComponent<Renderer>().bounds;
        _bottomBallBounds = bottomBall.GetComponent<Renderer>().bounds;

        _botRightBall = bottomBall.GetComponent<BounceBallBottomRight>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        velocity += -9.81f * Time.deltaTime;
        float pos = transform.position.y + velocity * Time.deltaTime;

        if (pos + _ballBounds.size.y / 2 > _areaBounds.max.y)
        {
            pos = _areaBounds.max.y - _ballBounds.size.y / 2;
            velocity = -velocity;
        }

        if (pos - _ballBounds.size.y / 2 < bottomBall.transform.position.y + _bottomBallBounds.size.y / 2)
        {
            float botBallmass = _botRightBall.mass;
            float botBallVelocity = _botRightBall.velocity;

            pos = bottomBall.transform.position.y + _bottomBallBounds.size.y / 2 + _ballBounds.size.y / 2;

            _botRightBall.velocity = (mass * velocity * (1 + 1) + botBallVelocity * (botBallmass - mass * 1))/(mass + botBallmass);
            velocity = (botBallmass * botBallVelocity * (1 + 1) + velocity * (mass - botBallmass * 1))/(mass + botBallmass);
        }

        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
}
