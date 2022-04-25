using UnityEngine;

public class BounceBallLeft : MonoBehaviour
{
    private float _velocity;
    
    private float k = 0.85F;
    
    public GameObject area;
    
    private Bounds areaBounds;
    private Bounds ballBounds;
    
    void Start()
    {
        areaBounds = area.GetComponent<Renderer>().bounds;
        ballBounds = GetComponent<Renderer>().bounds;
    }

    void FixedUpdate()
    {
        _velocity -= 9.81f * Time.deltaTime;
        
        float new_position = transform.position.y + _velocity * Time.deltaTime;
        
        if (new_position - ballBounds.size.y / 2 < areaBounds.min.y)
        {
            transform.position = new Vector3(transform.position.x, 0.5F, transform.position.z);
            
            _velocity = -_velocity;
            _velocity *= k;
        }
        
        transform.position += new Vector3(0, _velocity * Time.deltaTime, 0);
    }
}