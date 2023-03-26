using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    [SerializeField] private float _turnSpeed = 360;

    [Header("Hole Size")]
    [SerializeField] private GameObject _parentHole;
    [SerializeField] private int _size = 1;

    [Header("Camera Settings")]
    [SerializeField] private Camera _camera;

    [Header("Audios")]
    [SerializeField] private AudioClip _Grow;
    // Variable to store input
    private Vector3 _input;
    private float targetSize;
    // Update is called once per frame
    private void Update()
    {
        
        GatherInput();
        Look();
        
    }

    // FixedUpdate is called at fixed intervals
    private void FixedUpdate()
    {
        Move();
        
    }

    // Gather input from the player
    private void GatherInput()
    {
        _input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }

    private IEnumerator UpdateHoleSize()
    {
        float newSize = _size * 0.5f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(4.4295f + newSize, originalScale.y, 4.4295f + newSize);
        float duration = 0.5f; // Duration of the smooth effect (in seconds)
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            
            yield return null;
            
        }
        _camera.orthographicSize += 1;
        AudioSource.PlayClipAtPoint(_Grow, transform.position);
        // Set the final scale to ensure accuracy
        transform.localScale = targetScale;
    }

    // Rotate the player to face the input direction
    private void Look()
    {
        if (_input == Vector3.zero) return;

        var rot = Quaternion.LookRotation(_input.ToIso(), Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rot, _turnSpeed * Time.deltaTime);

        
    }

    // Move the player based on input
    private void Move()
    {
        _rb.MovePosition(transform.position + transform.forward * _input.normalized.magnitude * _speed * Time.deltaTime);
    }



    public void IncreaseHoleSize()
    {
        _size+=5;
        StartCoroutine(UpdateHoleSize());
        targetSize = _camera.orthographicSize + 1;
       
    }

}
public static class Helpers
{
    // Matrix used to convert input to isometric direction
    private static Matrix4x4 _isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    // Extension method to convert input to isometric direction
    public static Vector3 ToIso(this Vector3 input) => _isoMatrix.MultiplyPoint3x4(input);
}