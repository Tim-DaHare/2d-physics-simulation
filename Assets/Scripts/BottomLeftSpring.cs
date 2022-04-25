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

    // Update is called once per frame
    void FixedUpdate()
    {
        var delta_x_bottom = bottomRestLen - transform.position.x;
        var spring_force_bottom = bottomConst * delta_x_bottom;

        var delta_x_top = (top_spring.transform.position.x - _botRightSpring.rest_length_top) - transform.position.x;
        var spring_force_top = _botRightSpring.spring_constant_top * delta_x_top;

        var combined_spring_force = spring_force_bottom + spring_force_top;

        var newVelocity = velocity + combined_spring_force / mass * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + newVelocity * Time.deltaTime, transform.position.y, transform.position.z);
        
        velocity = newVelocity;
    }
}
