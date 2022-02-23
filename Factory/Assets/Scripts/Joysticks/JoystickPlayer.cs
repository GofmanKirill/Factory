
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class JoystickPlayer : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private VariableJoystick _variableJoystick;
    [SerializeField] private Transform _transformRatation;


    private Rigidbody _rigidbody;


    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }



    public void FixedUpdate()
    {
        Vector3 direction = Vector3.forward * _variableJoystick.Vertical + Vector3.right * _variableJoystick.Horizontal;
        
        _rigidbody.AddForce(direction * _speed * Time.fixedDeltaTime, ForceMode.VelocityChange);

        if(direction!=Vector3.zero)_transformRatation.rotation = Quaternion.Slerp(_transformRatation.rotation, Quaternion.LookRotation(direction), 30 * Time.deltaTime);
    }
}