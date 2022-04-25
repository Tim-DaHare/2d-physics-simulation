using UnityEngine;

public class FloatingBlock : MonoBehaviour
{
    public GameObject waterObj; 
    
    Renderer _waterRenderer;
    Renderer _blockRenderer;
    float _velocity;

    public float mass = 1.0f;
    public float waterDensity = 997.0f;

    void Start()
    {
        _waterRenderer = waterObj.GetComponent<Renderer>();
        _blockRenderer = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        var blockVolume = _blockRenderer.bounds.size.x * _blockRenderer.bounds.size.y;

        var displacedVolume = _blockRenderer.bounds.max.y < _waterRenderer.bounds.max.y ? 
            blockVolume : _blockRenderer.bounds.min.y > _waterRenderer.bounds.max.y ?
            0.0F : _blockRenderer.bounds.size.x * Mathf.Abs(_waterRenderer.bounds.max.y - _blockRenderer.bounds.min.y);

        var down = blockVolume * mass * -9.81f;
        var up = displacedVolume * waterDensity * 9.81f;
        var combinedForce = up + down;

        _velocity += combinedForce / mass * Time.deltaTime;
        var pos = transform.position.y + _velocity * Time.deltaTime;

        if (pos - _blockRenderer.bounds.size.y / 2 < _waterRenderer.bounds.min.y)
        {
            pos = _waterRenderer.bounds.min.y + _blockRenderer.bounds.size.y / 2;
            _velocity = 0.0f;
        }

        transform.position = new Vector3(transform.position.x, pos, transform.position.z);
    }
}
