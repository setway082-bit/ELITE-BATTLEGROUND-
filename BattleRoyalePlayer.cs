using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BattleRoyalePlayer : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float sprintSpeed = 8f;
    public float jumpHeight = 1.4f;
    public float gravity = -20f;
    public Transform cameraTransform;
    public float fireRange = 120f;
    public float fireDamage = 25f;
    public float fireRate = 0.18f;

    CharacterController controller;
    Vector2 moveInput, lookInput;
    Vector3 verticalVelocity;
    float nextShot;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (!cameraTransform && Camera.main) cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        // Editor testing controls. UI can call the public input methods below.
        if (Application.isEditor)
        {
            moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            lookInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            if (Input.GetMouseButton(0)) Fire();
            if (Input.GetKeyDown(KeyCode.Space)) Jump();
        }

        Vector3 forward = cameraTransform ? cameraTransform.forward : transform.forward;
        Vector3 right = cameraTransform ? cameraTransform.right : transform.right;
        forward.y = 0; right.y = 0;
        forward.Normalize(); right.Normalize();

        Vector3 move = forward * moveInput.y + right * moveInput.x;
        if (move.sqrMagnitude > 1f) move.Normalize();
        float speed = (Application.isEditor && Input.GetKey(KeyCode.LeftShift)) ? sprintSpeed : moveSpeed;
        controller.Move(move * speed * Time.deltaTime);

        if (move.sqrMagnitude > 0.01f)
        {
            Quaternion wanted = Quaternion.LookRotation(move);
            transform.rotation = Quaternion.Slerp(transform.rotation, wanted, 12f * Time.deltaTime);
        }

        if (controller.isGrounded && verticalVelocity.y < 0) verticalVelocity.y = -2f;
        verticalVelocity.y += gravity * Time.deltaTime;
        controller.Move(verticalVelocity * Time.deltaTime);
    }

    public void SetMoveInput(Vector2 value) => moveInput = Vector2.ClampMagnitude(value, 1f);
    public void SetLookInput(Vector2 value) => lookInput = value;

    public void Jump()
    {
        if (controller.isGrounded)
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }

    public void Fire()
    {
        if (Time.time < nextShot) return;
        nextShot = Time.time + fireRate;
        if (!cameraTransform) return;

        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, fireRange))
        {
            Health health = hit.collider.GetComponentInParent<Health>();
            if (health && health.gameObject != gameObject) health.TakeDamage(fireDamage);
        }
    }

    public void StopFire() { }
}
