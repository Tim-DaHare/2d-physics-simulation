using System;
using UnityEngine;

public class BottomLeftSpring : MonoBehaviour
{
    public GameObject top_spring;

    public float velocity;
    public float mass = 1.0f; 
    public float bottomConst = 1.0f;
    public float bottomRestLen = 5.0f;

    private BottomRightSpring _botRightSpring;
    
    private void Start()
    {
        _botRightSpring = top_spring.GetComponent<BottomRightSpring>();
    }

    void FixedUpdate()
    {
        var botDelta = bottomRestLen - transform.position.x;
        var bottomForce = bottomConst * botDelta;

        var topDelta = (top_spring.transform.position.x - _botRightSpring.rest_length_top) - transform.position.x;
        var topForce = _botRightSpring.spring_constant_top * topDelta;

        var forceSum = bottomForce + topForce;

        var newVelocity = velocity + forceSum / mass * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + newVelocity * Time.deltaTime, transform.position.y, transform.position.z);
        
        velocity = newVelocity;
    }
}
