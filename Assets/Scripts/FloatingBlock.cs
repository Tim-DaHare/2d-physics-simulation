using UnityEngine;

public class FloatingBlock : MonoBehaviour
{
    public GameObject waterObj;
    
    Bounds _waterBounds;
    Bounds _bounds;
    
    float speed = 0.0f;

    public float mass = 1.0F;
    public float mass_of_water = 997.0F;

    void Start()
    {
        _waterBounds = waterObj.GetComponent<Renderer>().bounds;
        _bounds = GetComponent<Renderer>().bounds;
    }

    public float test;

    void FixedUpdate()
    {
        // get area of block
        float block_area = _bounds.size.x * _bounds.size.y;

        float area_displaced = 0.0f;
        if (_bounds.max.y < _waterBounds.max.y)
        {
            // block fully under
            area_displaced = block_area;
        }
        else if (_bounds.min.y > _waterBounds.max.y)
        {
            // block fully out of water
            area_displaced = 0.0f;
        }
        else
        {
            // block halfway
            area_displaced = _bounds.size.x * Mathf.Abs(_waterBounds.max.y - _bounds.min.y);
        }

        const float gravity = 9.81F;
        
        float force_down = block_area * mass * -gravity;
        float force_up = area_displaced * mass_of_water * gravity;
        float total_force = force_up + force_down;

        test = force_up;

        float new_speed = speed + total_force / mass * Time.deltaTime;
        float new_position = transform.position.y + new_speed * Time.deltaTime;

        if (new_position - _bounds.size.y / 2 < _waterBounds.min.y)
        {
            new_position = _waterBounds.min.y + _bounds.size.y / 2;
            speed = 0.0f;
        }

        transform.position = new Vector3(transform.position.x, new_position, transform.position.z);
        speed = new_speed;
    }
}
