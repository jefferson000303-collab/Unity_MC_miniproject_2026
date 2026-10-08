using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float mouseSensitivity = 2.0f;

    private float cameraPitch = 0.0f;
    private Camera playerCamera;
    private bool isCursorLocked = true;
    private CharacterController controller; // Character Controller 변수 추가

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        controller = GetComponent<CharacterController>(); // 컴포넌트 가져오기

        UpdateCursorState();
    }

    void Update()
    {
        // ESC 키를 누를 때마다 커서 상태 토글
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

        // 플레이어가 바라보는 방향 기준 이동 벡터 계산
        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        // Character Controller를 이용한 이동 (벽 충돌 처리 포함)
        controller.Move(move * moveSpeed * Time.deltaTime);

        // (선택사항) 만약 중력을 적용하고 싶다면 아래 코드를 활성화하세요 (바닥으로 떨어지게 함)
        // move.y += Physics.gravity.y * Time.deltaTime;
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // 좌우 회전 (플레이어 몸체 회전)
        transform.Rotate(Vector3.up * mouseX);

        // 상하 회전 (카메라만 고개 숙이기/들기)
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        playerCamera.transform.localEulerAngles = new Vector3(cameraPitch, 0, 0);
    }
}