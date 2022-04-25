using UnityEngine;

public class Spring : MonoBehaviour
{
    private float _velocity;
    
    public float restLength = 5;
    public float constant = 0.1F;

    // Update is called once per frame
    void Update()
    {
        var currX = transform.position.x;

        var delta = currX - restLength;
        var force = -constant * delta;

        _velocity += force;
        transform.position += new Vector3(_velocity * Time.deltaTime, 0, 0);
    }
}
