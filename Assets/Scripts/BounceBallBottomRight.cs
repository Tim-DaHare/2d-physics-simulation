using UnityEngine;

public class BounceBallBottomRight : MonoBehaviour
{
    public GameObject area;

    private Bounds _areaBounds;
    private Bounds _ballBounds;

    public float velocity;
    public float mass = 1.0f;

    void Start()
    {
        _areaBounds = area.GetComponent<Renderer>().bounds;
        _ballBounds = GetComponent<Renderer>().bounds;
    }

    void FixedUpdate()
    {
        velocity += -9.81f * Time.deltaTime;
        float pos = transform.position.y + velocity * Time.deltaTime;

        if (pos - _ballBounds.size.y / 2 < _areaBounds.min.y)
        {
            pos = _areaBounds.min.y + _ballBounds.size.y / 2;
            velocity = -velocity;
        }

        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
}
