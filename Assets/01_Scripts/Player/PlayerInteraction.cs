using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("레이캐스트 최대 탐지 거리")]
    public float raycastDistance = 3.0f;

    [Header("Key Settings")]
    [Tooltip("일반 상호작용 키 (문 열기 등)")]
    public KeyCode interactKey = KeyCode.E;
    [Tooltip("메쉬 토글 키 (끄기/켜기)")]
    public KeyCode toggleKey = KeyCode.Q;

    [Tooltip("레이저가 유지되는 시간 (초)")]
    public float laserDuration = 1f;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        TryInteract();
    }

    void TryInteract()
    {
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // 레이저 끝점 기본값 (최대 거리)
        Vector3 laserEndPoint = ray.origin + ray.direction * raycastDistance;

        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            laserEndPoint = hit.point;

            // 1. E 키 상호작용 체크 (IInteractable)
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                if (Input.GetKeyDown(interactKey))
                {
                    Debug.Log(hit + "상호작용됨");
                    interactable.Interact();
                }
            }

            // 2. Q 키 토글 체크 (IToggleable)
            IToggleable toggleable = hit.collider.GetComponent<IToggleable>();
            if (toggleable != null)
            {
                if (Input.GetKeyDown(toggleKey))
                {
                    toggleable.Toggle();
                }
            }
        }

        // Scene 뷰에서 확인용 레이저 선
        Debug.DrawLine(ray.origin, laserEndPoint, Color.red, laserDuration);
    }
}