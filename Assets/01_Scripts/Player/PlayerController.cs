using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;

    private float cameraPitch = 0.0f;
    private Camera playerCamera;
    private bool isCursorLocked = true;
    private CharacterController controller;

    // 중력 계산을 위한 변수 추가
    private float verticalVelocity = 0f;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>();

        UpdateCursorState();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCursorLocked = !isCursorLocked;
            UpdateCursorState();
        }

        if (isCursorLocked)
        {
            HandleMovement();
            HandleMouseLook();
        }
    }

    void UpdateCursorState()
    {
        if (isCursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // A, D
        float moveVertical = Input.GetAxis("Vertical");     // W, S

        // 1. [핵심] 수평으로만 걷도록 Y축(위아래) 벡터 강제 제거
        Vector3 forward = transform.forward;
        forward.y = 0f;
        forward.Normalize(); // 길이를 1로 다시 맞춤

        Vector3 right = transform.right;
        right.y = 0f;
        right.Normalize();

        // 2. 평면 이동 방향 계산
        Vector3 move = right * moveHorizontal + forward * moveVertical;
        move *= moveSpeed;

        // 3. [핵심] 중력 적용 (공중에 뜨지 않고 바닥에 착 붙도록)
        if (controller.isGrounded)
        {
            verticalVelocity = -2f; // 바닥에 닿아있을 때는 바닥을 살짝 누르는 힘 유지
        }
        else
        {
            verticalVelocity += Physics.gravity.y * Time.deltaTime; // 공중에 있으면 떨어짐 (-9.81)
        }

        move.y = verticalVelocity;

        // 4. 최종 이동 실행
        controller.Move(move * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 좌우 회전 (플레이어 몸체 전체가 회전)
        transform.Rotate(Vector3.up * mouseX);

        // 상하 회전 (카메라만 고개 숙이기/들기)
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraPitch, 0, 0);
    }
}