using UnityEngine;

/// <summary>
/// I've got to admit i've had some help with this script x)
/// </summary>
public class BilliardBall : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public float mass = 1.0f;
    
    public GameObject areaObj;
    
    Renderer area;
    Renderer _ballRenderer;
    
    public BilliardBall lastCollidedBall;
    
    public BilliardBall[] billiards;
    
    void Start()
    {
        velocity = new Vector2(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f));
    
        area = areaObj.GetComponent<Renderer>();
        _ballRenderer = GetComponent<Renderer>();
    }
    
    void FixedUpdate()
    {
        // Update position based on velocity
        transform.position = new Vector3(transform.position.x + velocity.x * Time.deltaTime, transform.position.y + velocity.y * Time.deltaTime, transform.position.z);

        // Check if _ballRenderer is in bounds for y
        if (transform.position.y + _ballRenderer.bounds.size.y / 2 > area.bounds.max.y)
        {
            transform.position = new Vector3(transform.position.x, area.bounds.max.y - _ballRenderer.bounds.size.y / 2, transform.position.z);
            velocity = new Vector2(velocity.x, -velocity.y);
        }
        if (transform.position.y - _ballRenderer.bounds.size.y / 2 < area.bounds.min.y)
        {
            transform.position = new Vector3(transform.position.x, area.bounds.min.y + _ballRenderer.bounds.size.y / 2, transform.position.z);
            velocity = new Vector2(velocity.x, -velocity.y);
        }
        
        // Check if _ballRenderer is in bounds for x
        if (transform.position.x + _ballRenderer.bounds.size.x / 2 > area.bounds.max.x)
        {
            transform.position = new Vector3(area.bounds.max.x - _ballRenderer.bounds.size.x / 2, transform.position.y, transform.position.z);
            velocity = new Vector2(-velocity.x, velocity.y);
        }
        if (transform.position.x - _ballRenderer.bounds.size.x / 2 < area.bounds.min.x)
        {
            transform.position = new Vector3(area.bounds.min.x + _ballRenderer.bounds.size.x / 2, transform.position.y, transform.position.z);
            velocity = new Vector2(-velocity.x, velocity.y);
        }
        
        // Not great for performance i know...
        foreach (BilliardBall ball in billiards)
        {
            // if _ballRenderer is current _ballRenderer, continue
            if (ball.gameObject == gameObject)
                continue;

            // Check for collision
            if (CheckIntersect(ball))
            {
                // To prevent balls from clipping inside one another, they can't collide more than once before hitting something else
                // I chose not to move the balls out of each other because sometimes that just pushes them into another ball which makes things worse
                // once the collision is resolved, the balls, if prohibited from interacting, will move out of each other
                if(lastCollidedBall == ball)
                    continue;
                
                lastCollidedBall = ball;
                ball.lastCollidedBall = this;

                // Find the angle that rotates the balls to the x-axis.
                var diffAngle = GetAngle(ball.gameObject);
                
                Vector2 transformedVelocity = RotateVector(velocity, diffAngle);
                Vector2 otherTransformedVelocity = RotateVector(ball.velocity, diffAngle);

                var v1 = transformedVelocity.x;
                var v2 = otherTransformedVelocity.x;
                var m1 = mass;
                var m2 = ball.mass;
                var k = 1.0f;

                var u1 = (m2 * v2 * (k + 1) + v1 * (m1 - m2 * k)) / (m1 + m2);
                var u2 = (m1 * v1 * (k + 1) + v2 * (m2 - m1 * k)) / (m1 + m2);

                transformedVelocity.x = u1;
                otherTransformedVelocity.x = u2;

                Vector2 baseVelocity = RotateVector(transformedVelocity, -diffAngle);
                Vector2 ballBaseVelocity = RotateVector(otherTransformedVelocity, -diffAngle);

                velocity = baseVelocity;
                ball.velocity = ballBaseVelocity;
            }
        }
    }
    
    bool CheckIntersect(BilliardBall ball)
    {
        var distance = Mathf.Sqrt(Mathf.Pow(ball.transform.position.x - transform.position.x, 2) + Mathf.Pow(ball.transform.position.y - transform.position.y, 2));
        var radius = ball._ballRenderer.bounds.size.x / 2;
        return (distance < radius * 2);
    }

    float GetAngle(GameObject ball)
    {
        var xDiff = ball.transform.position.x - transform.position.x;
        var yDiff = ball.transform.position.y - transform.position.y;
        var angle = Mathf.Atan2(yDiff, xDiff) * Mathf.Rad2Deg;
        
        return Mathf.Abs(angle) > 90 ? angle < 0 ? -(angle + 180) : -(angle - 180) : -angle;
    }

    Vector2 RotateVector(Vector2 vector, float angle)
    {
        var x = vector.x;
        var y = vector.y;
        
        var newX = x * Mathf.Cos(RadAngle(angle)) - y * Mathf.Sin(RadAngle(angle));
        var newY = x * Mathf.Sin(RadAngle(angle)) + y * Mathf.Cos(RadAngle(angle));
        
        return new Vector2(newX, newY);
    }

    float RadAngle(float angle)
    {
        return angle * Mathf.PI / 180;
    }
}
