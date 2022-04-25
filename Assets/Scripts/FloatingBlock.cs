using UnityEngine;

public class FloatingBlock : MonoBehaviour
{
    public GameObject waterObj;
    
    Bounds _waterBounds;
    Bounds _bounds;
    
    float _velocity;

    public float mass = 1.0F;
    public float waterDensity = 997.0F;

    void Start()
    {
        _waterBounds = waterObj.GetComponent<Renderer>().bounds;
        _bounds = GetComponent<Renderer>().bounds;
    }

    void FixedUpdate()
    {
        // get area of block
        float blockVol = _bounds.size.x * _bounds.size.y;

        float displacedVol = _bounds.max.y < _waterBounds.max.y ? 
            blockVol : _bounds.min.y > _waterBounds.max.y ? 
            0.0F : _bounds.size.x * Mathf.Abs(_waterBounds.max.y - _bounds.min.y);

        const float gravity = 9.81F;
        
        float down = blockVol * mass * -gravity;
        float up = displacedVol * waterDensity * gravity;
        float forceSum = up + down;

        _velocity += forceSum / mass * Time.deltaTime;
        float pos = transform.position.y + _velocity * Time.deltaTime;

        if (pos - _bounds.size.y / 2 < _waterBounds.min.y)
        {
            pos = _waterBounds.min.y + _bounds.size.y / 2;
            _velocity = 0.0f;
        }

        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
}
