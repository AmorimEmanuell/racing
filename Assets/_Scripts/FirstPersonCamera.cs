#pragma warning disable 0649
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    private void Update()
    {
        var mouseX = Input.GetAxis("Mouse X");
        var mouseY = Input.GetAxis("Mouse Y");

        var rotation = _camera.rotation.eulerAngles;
        rotation.x -= mouseY;
        rotation.y += mouseX;

        _camera.rotation = Quaternion.Euler(rotation);
    }
}
