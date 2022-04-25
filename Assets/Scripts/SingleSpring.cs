using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSpring : MonoBehaviour
{
    Renderer area;
    Renderer spring;
    public float speed = 0.0f;
    public float mass = 1.0f; //Set in UNITY
    public float spring_constant = 1.0f;
    public float rest_length = 0.0f; //Set in UNITY


    void Start()
    {
        area = GameObject.Find("springs_area").GetComponent<Renderer>();
        spring = GameObject.Find("single_spring_spring").GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta_x = (area.bounds.min.x + rest_length) - transform.position.x;
        float spring_force = spring_constant * delta_x;
        float new_speed = speed + spring_force / mass * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + new_speed * Time.deltaTime, transform.position.y, transform.position.z);
        speed = new_speed;

        // update the scale and position of the spring sprite
        // float new_scale = transform.position.x - area.bounds.min.x;
        // float new_position = area.bounds.min.x + new_scale / 2;
        // spring.transform.localScale = new Vector3(new_scale, spring.transform.localScale.y, spring.transform.localScale.z);
        // spring.transform.position = new Vector3(new_position, spring.transform.position.y, spring.transform.position.z);
    }
}
