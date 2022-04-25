using UnityEngine;

public class BilliardBall : MonoBehaviour
{
    public Vector2 velocity = new Vector2(0.0f, 0.0f);
    public float mass = 1.0f;

    public GameObject areaObj;
    
    Renderer area;
    Renderer this_ball;
    
    public string last_collided_with = "";

    void Start()
    {
        velocity = new Vector2(Random.Range(-2.0f, 2.0f), Random.Range(-2.0f, 2.0f));

        area = areaObj.GetComponent<Renderer>();
        this_ball = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update position based on velocity
        // Check if ball is in bounds
        // Check for collision
        // Find rotation direction and degrees
        // Rotate vectors
        // Solve collision on X-axis
        // Rotate back vectors
        // Update both targets

        // Update position based on velocity
        transform.position = new Vector3(transform.position.x + velocity.x * Time.deltaTime, transform.position.y + velocity.y * Time.deltaTime, transform.position.z);

        // Check if ball is in bounds for y
        if (transform.position.y + this_ball.bounds.size.y / 2 > area.bounds.max.y)
        {
            transform.position = new Vector3(transform.position.x, area.bounds.max.y - this_ball.bounds.size.y / 2, transform.position.z);
            velocity = new Vector2(velocity.x, -velocity.y);
            last_collided_with = "top";
        }
        if (transform.position.y - this_ball.bounds.size.y / 2 < area.bounds.min.y)
        {
            transform.position = new Vector3(transform.position.x, area.bounds.min.y + this_ball.bounds.size.y / 2, transform.position.z);
            velocity = new Vector2(velocity.x, -velocity.y);
            last_collided_with = "bottom";
        }

        // Check if ball is in bounds for x
        if (transform.position.x + this_ball.bounds.size.x / 2 > area.bounds.max.x)
        {
            transform.position = new Vector3(area.bounds.max.x - this_ball.bounds.size.x / 2, transform.position.y, transform.position.z);
            velocity = new Vector2(-velocity.x, velocity.y);
            last_collided_with = "right";
        }
        if (transform.position.x - this_ball.bounds.size.x / 2 < area.bounds.min.x)
        {
            transform.position = new Vector3(area.bounds.min.x + this_ball.bounds.size.x / 2, transform.position.y, transform.position.z);
            velocity = new Vector2(-velocity.x, velocity.y);
            last_collided_with = "left";
        }

        // Get all balls
        GameObject[] all_balls;
        all_balls = GameObject.FindGameObjectsWithTag("BilliardBall");
        foreach (GameObject ball in all_balls)
        {
            // if ball is current ball, continue
            if (ball == this.gameObject)
            {
                continue;
            }

            // Check for collision
            if (check_collision(ball))
            {
                // To prevent balls from clipping inside one another, they can't collide more than once before hitting something else
                // I chose not to move the balls out of each other because sometimes that just pushes them into another ball which makes things worse
                // Onse the collision is resolved, the balls, if prohibited from interacting, will move out of each other
                if(last_collided_with == ball.name)
                {
                    continue;
                }
                
                last_collided_with = ball.name;
                ball.GetComponent<BilliardBall>().last_collided_with = gameObject.name;

                // Find the angle that rotates the balls to the x-axis.
                float angle_between_balls = get_angle(ball);
                
                Vector2 this_rotated_velocity = rotate_vector(velocity, angle_between_balls);
                Vector2 ball_rotated_velocity = rotate_vector(ball.GetComponent<BilliardBall>().velocity, angle_between_balls);

                float v1_x = this_rotated_velocity.x;
                float v2_x = ball_rotated_velocity.x;
                float m1 = mass;
                float m2 = ball.GetComponent<BilliardBall>().mass;
                float k = 1.0f;

                float u1 = (m2 * v2_x * (k + 1) + v1_x * (m1 - m2 * k)) / (m1 + m2);
                float u2 = (m1 * v1_x * (k + 1) + v2_x * (m2 - m1 * k)) / (m1 + m2);

                this_rotated_velocity.x = u1;
                ball_rotated_velocity.x = u2;

                Vector2 this_unrotated_velocity = rotate_vector(this_rotated_velocity, -angle_between_balls);
                Vector2 ball_unrotated_velocity = rotate_vector(ball_rotated_velocity, -angle_between_balls);

                velocity = this_unrotated_velocity;
                ball.GetComponent<BilliardBall>().velocity = ball_unrotated_velocity;
            }
        }
    }

    bool check_collision(GameObject ball)
    {
        float distance = Mathf.Sqrt(Mathf.Pow(ball.transform.position.x - this.transform.position.x, 2) + Mathf.Pow(ball.transform.position.y - this.transform.position.y, 2));
        float radius = ball.GetComponent<Renderer>().bounds.size.x / 2;
        return (distance < radius * 2);
    }

    float get_angle(GameObject ball)
    {
        float x_diff = ball.transform.position.x - this.transform.position.x;
        float y_diff = ball.transform.position.y - this.transform.position.y;
        float angle = Mathf.Atan2(y_diff, x_diff) * Mathf.Rad2Deg;
        angle = Mathf.Abs(angle) > 90 ? angle < 0 ? -(angle + 180) : -(angle - 180) : -angle;
        return angle;
    }

    Vector2 rotate_vector(Vector2 vector, float angle)
    {
        float x = vector.x;
        float y = vector.y;
        float new_x = x * Mathf.Cos(rad_angle(angle)) - y * Mathf.Sin(rad_angle(angle));
        float new_y = x * Mathf.Sin(rad_angle(angle)) + y * Mathf.Cos(rad_angle(angle));
        return new Vector2(new_x, new_y);
    }

    float rad_angle(float angle)
    {
        return angle * Mathf.PI / 180;
    }
}
