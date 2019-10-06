using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public float MaxSpeed = 5f;
    public float JumpHeight = 3f;
    public float GroundDistance = 0.2f;
    public float ForceMult = 5f;
    public LayerMask Ground;

    public GameObject CameraObject;
    private Rigidbody _body;
    private Rigidbody _camerabody;
    private Vector3 _inputs = Vector3.zero;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    // Start is called before the first frame update
    void Start()
    {
         _body = GetComponent<Rigidbody>();
         _camerabody = CameraObject.GetComponent<Rigidbody>();
        _groundChecker = transform;
    }

    // Update is called once per frame
    void Update()
    {

        Collider[] hitColliders = Physics.OverlapSphere(_groundChecker.position, GroundDistance, Ground);
        _isGrounded =  hitColliders.Length > 0;   
    
       
        Vector3 forwardDirection = new Vector3(_camerabody.transform.forward.x, 0.0f, _camerabody.transform.forward.z).normalized;
        Vector3 rightDirection = new Vector3(_camerabody.transform.right.x, 0.0f, _camerabody.transform.right.z).normalized;
        if (_body.velocity.magnitude < MaxSpeed)
        {
            
            _body.AddForce( ForceMult * (Input.GetAxis("Vertical") * forwardDirection + Input.GetAxis("Horizontal") * rightDirection));
        }

        if (Input.GetButtonDown("Jump") && _isGrounded)
        {
            _body.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }
       

    }

}

