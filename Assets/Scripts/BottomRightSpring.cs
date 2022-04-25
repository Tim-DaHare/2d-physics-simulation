using UnityEngine;

public class BottomRightSpring : MonoBehaviour
{
    public GameObject bottom_spring;
    
    public float speed = 0.0f;
    public float mass = 1.0f; //Set in UNITY
    public float spring_constant_top = 1.0f;
    public float rest_length_top = 5.0f; //Set in UNITY
    

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta_x = (bottom_spring.transform.position.x + rest_length_top) - transform.position.x;
        float spring_force = spring_constant_top * delta_x;
        float new_speed = speed + spring_force / mass * Time.deltaTime;
        transform.position = new Vector3(transform.position.x + new_speed * Time.deltaTime, transform.position.y, transform.position.z);
        speed = new_speed;
    }
}
