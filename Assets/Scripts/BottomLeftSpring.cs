using UnityEngine;

public class BottomLeftSpring : MonoBehaviour
{
    public GameObject topSpring;

    public float velocity;
    public float mass = 1.0f; 
    public float bottomConst = 1.0f;
    public float bottomRestLen = 5.0f;

    private BottomRightSpring _botRightSpring;
    
    private void Start()
    {
        _botRightSpring = topSpring.GetComponent<BottomRightSpring>();
    }

    void FixedUpdate()
    {
        var botDelta = bottomRestLen - transform.position.x;
        var bottomForce = bottomConst * botDelta;

        var topDelta = (topSpring.transform.position.x - _botRightSpring.rest_length_top) - transform.position.x;
        var topForce = _botRightSpring.spring_constant_top * topDelta;

        var forceSum = bottomForce + topForce;

        velocity += forceSum / mass * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + velocity * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
