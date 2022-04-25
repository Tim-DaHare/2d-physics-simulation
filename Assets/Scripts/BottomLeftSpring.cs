using UnityEngine;

public class BottomLeftSpring : MonoBehaviour
{
    public GameObject top_spring;
    
    public float speed = 0.0f;
    public float mass = 1.0f; //Set in UNITY
    public float spring_constant_bottom = 1.0f;
    public float rest_length_bottom = 5.0f; //Set in UNITY
    

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta_x_bottom = rest_length_bottom - transform.position.x;
        float spring_force_bottom = spring_constant_bottom * delta_x_bottom;

        float delta_x_top = (top_spring.transform.position.x - top_spring.GetComponent<BottomRightSpring>().rest_length_top) - transform.position.x;
        float spring_force_top = top_spring.GetComponent<BottomRightSpring>().spring_constant_top * delta_x_top;

        float combined_spring_force = spring_force_bottom + spring_force_top;

        float new_speed = speed + combined_spring_force / mass * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + new_speed * Time.deltaTime, transform.position.y, transform.position.z);
        speed = new_speed;
    }
}
