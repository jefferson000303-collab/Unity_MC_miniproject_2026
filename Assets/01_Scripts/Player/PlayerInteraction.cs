using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [Tooltip("레이캐스트 최대 탐지 거리")]
    public float raycastDistance = 3.0f;
    [Tooltip("상호작용에 사용할 키")]
    public KeyCode interactInput = KeyCode.E;
    [Tooltip("레이저가 유지되는 시간 (초)")]
    public float laserDuration = 1f;

    private Camera playerCamera;

    void Start()
    {
        // 카메라 컴포넌트 가져오기 (플레이어 자식에 카메라가 있는 구조 기준)
        playerCamera = GetComponentInChildren<Camera>();
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        // E 키를 누르는 순간 레이저 발사
        if (Input.GetKeyDown(interactInput))
        {
            TryInteract();
        }
    }

    // 조준(레이캐스트) 탐지 및 상호작용 시도
    void TryInteract()
    {
        // 화면 정중앙(화면 크기의 절반)에서 레이 생성
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        // 1. 레이저의 끝점을 기본값으로 최대 거리 지점 설정 (아무것도 안 부딪혔을 때용)
        Vector3 laserEndPoint = ray.origin + ray.direction * raycastDistance;


        // 레이캐스트 발사
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            // 2. 무언가에 부딪혔다면 끝점을 부딪힌 위치로 갱신
            laserEndPoint = hit.point;

            // 부딪힌 오브젝트가 IInteractable 인터페이스를 가지고 있는지 확인
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // TODO: 화면에 "E를 눌러 상호작용" 같은 UI 텍스트를 띄울 수 있는 자리입니다.

                // 상호작용 키를 누르면 Interact 실행
                if (Input.GetKeyDown(interactInput))
                {
                    interactable.Interact();
                }
            }
        }

        // Scene 뷰에서 눈으로 확인할 수 있도록 빨간색 레이저 선을 그림 (지속 시간 설정)
        Debug.DrawLine(ray.origin, laserEndPoint, Color.red, laserDuration);
    }
}