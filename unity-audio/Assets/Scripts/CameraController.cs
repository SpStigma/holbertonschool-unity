using UnityEngine;


public static class GameSettings
{
    public static bool isInverted = false;
}

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    [SerializeField]
    private float _mouseSensitivity = 3.0f;

    private float _rotationY;
    private float _rotationX;

    
    public Transform target;

    private float distanceFromTarget = 6.25f;
    private Vector3 _currentRotation;
    private Vector3 _smoothVelocity = Vector3.zero;
    public float smoothTime = 0.2f;
    public bool isInverted;

    void Start()
    {
        instance = this;
        isInverted = GameSettings.isInverted;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity;
            
            if (isInverted == false)
            {
                _rotationY += mouseX;
                _rotationX -= mouseY;
            }
            else
            {
                _rotationY -= mouseX;
                _rotationX += mouseY;
            }

            Vector3 nextRotation = new Vector3(_rotationX, _rotationY, 0);
            _currentRotation = Vector3.SmoothDamp(_currentRotation, nextRotation, ref _smoothVelocity, smoothTime);

            transform.localEulerAngles = _currentRotation;
        }
        transform.position = target.position - transform.forward * distanceFromTarget;
    }
}
