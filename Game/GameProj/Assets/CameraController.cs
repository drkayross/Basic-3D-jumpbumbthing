using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target;
    public float HeightOffset;
    public float RearOffset;

    public float SpringConstant;

    private Vector3 _currentDisplacement;
    private Vector3 _targetPosition;
    private Rigidbody _body;
    private Rigidbody _myRigidBody;

    private Renderer _renderer;
    void Start()
    {
        _myRigidBody = GetComponent<Rigidbody>();
        _body = target.GetComponent<Rigidbody>();
        _currentDisplacement = _body.transform.position - transform.position;
        _targetPosition = _body.transform.position + new Vector3(0.0f,HeightOffset,0.0f);
        _renderer = target.GetComponent<Renderer>();
    }

    void Update()
    {
        bool isMoving = _body.velocity.sqrMagnitude > 0.01f;
        _renderer.material.SetColor("_Color", isMoving ? Color.green : Color.red);
        transform.LookAt(_body.transform);

        Vector3 readOffsetDir = isMoving ? new Vector3(-_body.velocity.x, 0.0f, -_body.velocity.z).normalized : new Vector3(-transform.forward.x, 0.0f, -transform.forward.z).normalized;
        _targetPosition = _body.transform.position + new Vector3(0.0f,HeightOffset,0.0f) + RearOffset*( readOffsetDir ); 
        _currentDisplacement = _targetPosition - transform.position;
    }

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _targetPosition, SpringConstant * Time.fixedDeltaTime);
    }
}
