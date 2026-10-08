using UnityEngine;

public class rayTest : MonoBehaviour
{
    [Header("Laser Settings")]
    [Tooltip("레이저 최대 발사 거리")]
    public float raycastDistance = 5.0f;
    [Tooltip("상호작용(레이저 발사) 키")]
    public KeyCode interactInput = KeyCode.E;
    [Tooltip("레이저가 유지되는 시간 (초)")]
    public float laserDuration = 1f;

    private Camera playerCamera;

    void Start()
    {
        // 카메라 컴포넌트 가져오기
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
            ShootLaser();
        }
    }

    void ShootLaser()
    {
        // 화면 정중앙에서 레이 생성
        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;

        Vector3 laserEndPoint;

        // 레이캐스트 발사 및 충돌 확인
        if (Physics.Raycast(ray, out hit, raycastDistance))
        {
            // 무언가에 부딪혔다면 부딪힌 지점이 레이저의 끝점
            laserEndPoint = hit.point;
            Debug.Log($"레이저 명중: {hit.collider.name}");
        }
        else
        {
            // 부딪힌게 없다면 최대 거리 지점이 레이저의 끝점
            laserEndPoint = ray.origin + ray.direction * raycastDistance;
            Debug.Log("레이저 허공 발사");
        }

        // Scene 뷰에서 눈으로 확인할 수 있도록 빨간색 레이저 선을 그림 (지속 시간 설정)
        Debug.DrawLine(ray.origin, laserEndPoint, Color.red, laserDuration);
    }
}