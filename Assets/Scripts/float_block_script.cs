using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class float_block_script : MonoBehaviour
{
    public GameObject waterObj;
    
    Renderer water;
    Renderer block;
    float speed = 0.0f;

    [SerializeField] public float mass = 1.0f; //Set in UNITY
    [SerializeField] public float mass_of_water = 997.0f; //Set in UNITY

    void Start()
    {
        water = waterObj.GetComponent<Renderer>();
        block = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        // get area of block
        float block_area = block.bounds.size.x * block.bounds.size.y;

        float area_displaced = 0.0f;
        if (block.bounds.max.y < water.bounds.max.y)
        {
            // block fully under
            area_displaced = block_area;
        }
        else if (block.bounds.min.y > water.bounds.max.y)
        {
            // block fully out of water
            area_displaced = 0.0f;
        }
        else
        {
            // block halfway
            area_displaced = block.bounds.size.x * Mathf.Abs(water.bounds.max.y - block.bounds.min.y);
        }

        float force_down = block_area * mass * -9.81f;
        float force_up = area_displaced * mass_of_water * 9.81f;
        float total_force = force_up + force_down;

        float new_speed = speed + total_force / mass * Time.deltaTime;
        float new_position = transform.position.y + new_speed * Time.deltaTime;

        if (new_position - block.bounds.size.y / 2 < water.bounds.min.y)
        {
            new_position = water.bounds.min.y + block.bounds.size.y / 2;
            speed = 0.0f;
        }

        transform.position = new Vector3(transform.position.x, new_position, transform.position.z);
        speed = new_speed;
    }
}
