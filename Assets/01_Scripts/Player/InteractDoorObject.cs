using System.Collections;
using UnityEngine;

public class InteractDoorObject : MonoBehaviour, IInteractable
{
    // 경첩(회전 축)의 위치를 선택하기 위한 열거형(Enum) 정의
    public enum HingeSide
    {
        Left,  // 왼쪽 축 기준 (오른쪽 손잡이를 당겨서 여는 형태 등)
        Right  // 오른쪽 축 기준 (왼쪽 손잡이를 당겨서 여는 형태 등)
    }
    [Header("Hinge Settings")]
    [Tooltip("어느 쪽 축(경첩)을 기준으로 문을 회전시킬지 선택합니다.")]
    public HingeSide hingeSide = HingeSide.Left;

    [Header("Door Settings")]
    [Tooltip("회전시킬 문 오브젝트 (비워두면 본인 또는 자식 오브젝트의 Transform을 사용)")]
    public Transform doorTransform;

    [Tooltip("문이 열릴 목표 각도 (예: Y축 기준 90도)")]
    public Vector3 openAngle = new Vector3(0f, 90f, 0f);

    [Tooltip("문이 열리고 닫히는 속도 (값이 클수록 빠름)")]
    public float smoothSpeed = 5.0f;

    [Header("Collision & Mesh Settings")]
    private MeshRenderer[] targetMeshRenderers; // 문에 포함된 모든 렌더러 배열
    private Collider[] targetColliders;         // 문에 포함된 모든 콜라이더 배열

    private bool isOpen = false;                // 현재 문이 열려있는지 여부 (닫힘: false, 열림: true)
    private Vector3 closedRotation;             // 닫혀있을 때의 기본 로컬 회전값
    private Vector3 targetRotation;             // 이번에 도달해야 할 목표 로컬 회전값
    private Coroutine animationCoroutine;       // 중복 실행 방지를 위한 회전 애니메이션 코루틴 변수

    void Start()
    {
        // 1. 문 트랜스폼 지정 (할당되지 않았다면 자기 자신을 기본값으로 사용)
        if (doorTransform == null)
        {
            doorTransform = transform;
        }

        // 2. 게임 시작 시점의 기본 회전값을 고정 저장
        closedRotation = doorTransform.localEulerAngles;
        targetRotation = closedRotation;

        // 3. 비활성화된 자식 오브젝트를 포함하여 모든 렌더러와 콜라이더를 미리 수집
        targetMeshRenderers = GetComponentsInChildren<MeshRenderer>(true);
        targetColliders = GetComponentsInChildren<Collider>(true);
    }

    // E 키를 눌렀을 때 PlayerInteraction에서 호출되는 상호작용 메서드 (IInteractable 인터페이스 구현)
    public void Interact()
    {
        // 문이 열려있으면 닫고, 닫혀있으면 여는 상태 반전
        isOpen = !isOpen;

        // 4. 선택한 경첩(HingeSide) 방향에 따라 회전 각도 부호(방향)를 자동 전환
        // - Left 경첩 선택 시: 정방향(+) 회전
        // - Right 경첩 선택 시: 역방향(-) 회전으로 반전
        Vector3 directionMultiplier = (hingeSide == HingeSide.Right) ? -openAngle : openAngle;
        targetRotation = isOpen ? closedRotation + directionMultiplier : closedRotation;

        // 5. 이미 문이 움직이는 중이라면 기존 애니메이션을 중단하고 새로운 목표로 부드럽게 갱신
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(SmoothRotateDoor());

        // 6. 문이 열리거나 닫힐 때 플레이어 통과 여부(Is Trigger) 동적 제어
        // - 열림(isOpen = true): Is Trigger = true (플레이어가 통과 가능)
        // - 닫힘(isOpen = false): Is Trigger = false (플레이어가 막힘)
        foreach (var col in targetColliders)
        {
            if (col != null)
            {
                col.isTrigger = isOpen;
            }
        }

        Debug.Log($"[{gameObject.name}] 문 상태: {(isOpen ? "열림" : "닫힘")} (선택된 경첩 축: {hingeSide})");
    }

    // 문을 부드럽게 목표 각도까지 회전시키는 코루틴 메서드
    IEnumerator SmoothRotateDoor()
    {
        Quaternion startRotation = doorTransform.localRotation;
        Quaternion endRotation = Quaternion.Euler(targetRotation);
        float timeElapsed = 0f; // 경과 시간 초기화

        // 시간이 흐름에 따라 Slerp(구면 선형 보간)를 이용해 각도를 부드럽게 변화시킴
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * smoothSpeed;
            doorTransform.localRotation = Quaternion.Slerp(startRotation, endRotation, timeElapsed);
            yield return null; // 다음 프레임까지 대기
        }

        // 오차 없이 정확한 목표 각도로 최종 고정
        doorTransform.localRotation = endRotation;
    }
}