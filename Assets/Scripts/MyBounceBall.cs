using UnityEngine;

public class MyBounceBall : MonoBehaviour
{
    private float _gravity = 9.81F;
    
    public float k = 1F;
    public float _velocity;
    
    void FixedUpdate()
    {
        var force = -_gravity * Time.deltaTime;
        
        _velocity += force;

        if (transform.position.y - 0.5F <= 0)
        {
            transform.position = new Vector3(transform.position.x, 0.5F, transform.position.z);
            _velocity = -_velocity * k;
            // _velocity *= k;
        }

        transform.position += new Vector3(0, _velocity * Time.deltaTime, 0);
    }
}
