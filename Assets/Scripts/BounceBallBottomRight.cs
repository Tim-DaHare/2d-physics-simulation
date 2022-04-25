using UnityEngine;

public class BounceBallBottomRight : MonoBehaviour
{
    public GameObject area;

    private Bounds _areaBounds;
    private Bounds _ballBounds;

    public float speed = 0.0f;
    public float mass = 1.0f;

    void Start()
    {
        _areaBounds = area.GetComponent<Renderer>().bounds;
        _ballBounds = GetComponent<Renderer>().bounds;
    }

    void FixedUpdate()
    {
        float new_speed = speed - 9.81f * Time.deltaTime;
        float new_position = transform.position.y + new_speed * Time.deltaTime;

        if (new_position - _ballBounds.size.y / 2 < _areaBounds.min.y)
        {
            new_position = _areaBounds.min.y + _ballBounds.size.y / 2;
            new_speed = -new_speed;
        }

        transform.position = new Vector3(transform.position.x, new_position, transform.position.z);
        speed = new_speed;
    }
}
