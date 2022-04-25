using UnityEngine;

public class BottomRightSpring : MonoBehaviour
{
    public GameObject bottom_spring;

    public float speed;
    public float mass = 1.0f;
    public float spring_constant_top = 1.0f;
    public float rest_length_top = 5.0f;
    

    // Update is called once per frame
    void FixedUpdate()
    {
        float delta = (bottom_spring.transform.position.x + rest_length_top) - transform.position.x;
        float force = spring_constant_top * delta;
        float velocity = speed + force / mass * Time.deltaTime;
        
        transform.position = new Vector3(transform.position.x + velocity * Time.deltaTime, transform.position.y, transform.position.z);
        
        speed = velocity;
    }
}
