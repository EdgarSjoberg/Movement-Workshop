using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{

    [SerializeField] float speed = 1;
    [SerializeField]float jumpHeight = 25;
    float jump;
    [SerializeField]float gravity = 0.98f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (jump > 0)
        {
            jump -= gravity;
        }
        transform.position += new Vector3(Input.GetAxis("Horizontal") * speed, jump, Input.GetAxis("Vertical") * speed) * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump += jumpHeight;
        }

    }
}
