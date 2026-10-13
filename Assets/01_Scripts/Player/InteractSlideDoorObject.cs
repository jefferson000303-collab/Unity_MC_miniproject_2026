using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class InteractSlideDoorObject : MonoBehaviour, IInteractable
{
    [Header("Slide Door Settings")]
    [Tooltip("이동시킬 문 오브젝트 (비워두면 본인 또는 자식 오브젝트의 Transform을 사용)")]
    public Transform doorTransform;

    [Tooltip("닫혀있을 때의 위치를 기준으로, 열릴 때 이동할 방향과 거리 (예: X축으로 1.5미터)")]
    public Vector3 slideOffset = new Vector3(1.5f, 0f, 0f);

    [Tooltip("문이 열리고 닫히는 속도 배율")]
    public float smoothSpeed = 5.0f;

    [Header("Collision & Mesh Settings")]
    private MeshRenderer[] targetMeshRenderers; // 문에 포함된 모든 렌더러 배열
    private Collider[] targetColliders;         // 문에 포함된 모든 콜라이더 배열

    private bool isOpen = false;                // 현재 문이 열려있는지 여부 (닫힘: false, 열림: true)
    private Vector3 originalLocalPosition;      // 💡 게임 시작 시점의 고정된 원래 로컬 위치
    private Coroutine animationCoroutine;       // 중복 실행 방지를 위한 슬라이딩 코루틴 변수

    void Start()
    {
        // 1. 문 트랜스폼 지정 (할당되지 않았다면 자기 자신을 기본값으로 사용)
        if (doorTransform == null)
        {
            doorTransform = transform;
        }

        // 2. 게임이 시작하는 순간의 로컬 위치를 완벽하게 고정 저장합니다.
        originalLocalPosition = doorTransform.localPosition;

        // 3. 비활성화된 자식 오브젝트를 포함하여 모든 렌더러와 콜라이더를 미리 수집합니다.
        targetMeshRenderers = GetComponentsInChildren<MeshRenderer>(true);
        targetColliders = GetComponentsInChildren<Collider>(true);
    }

    // E 키를 눌렀을 때 PlayerInteraction에서 호출되는 상호작용 메서드 (IInteractable 인터페이스 구현)
    public void Interact()
    {
        // 문이 열려있으면 닫고, 닫혀있으면 여는 상태 반전
        isOpen = !isOpen;

        // 4. 열릴 때는 '원래 위치 + 오프셋', 닫힐 때는 '원래 위치'로 목표 위치를 정확히 설정합니다.
        Vector3 targetPosition = isOpen ? (originalLocalPosition + slideOffset) : originalLocalPosition;

        // 5. 문이 이미 움직이는 중이라면 기존 코루틴을 멈추고 새로운 목표 지점으로 부드럽게 갱신합니다.
        if (animationCoroutine != null)
        {
            StopCoroutine(animationCoroutine);
        }
        animationCoroutine = StartCoroutine(SmoothSlideDoor(targetPosition));

        // 6. 통과 처리 (Is Trigger 및 Convex 제어)
        foreach (var col in targetColliders)
        {
            if (col != null)
            {
                // 💡 만약 콜라이더가 Mesh Collider 타입이라면,
                // Is Trigger를 켜기 *전에* 반드시 Convex가 true여야 유니티 물리 엔진 오류가 나지 않습니다!
                if (col is MeshCollider meshCollider)
                {
                    // 문이 열릴 때(isOpen = true)는 Convex를 먼저 true로 켜줍니다.
                    if (isOpen)
                    {
                        meshCollider.convex = true;
                    }
                }

                // 7. 문이 열리면 통과 가능(Trigger = true), 닫히면 막힘(Trigger = false)으로 설정합니다.
                col.isTrigger = isOpen;
            }
        }

        Debug.Log($"[{gameObject.name}] 슬라이드 도어 상태: {(isOpen ? "열림" : "닫힘")}");
    }

    // 문을 목표 위치까지 부드럽게 미끄러지듯 이동시키는 코루틴 메서드
    IEnumerator SmoothSlideDoor(Vector3 targetPos)
    {
        Vector3 startPos = doorTransform.localPosition;
        float timeElapsed = 0f; // 경과 시간 초기화

        // 시간이 흐름에 따라 Lerp(선형 보간)를 이용해 위치를 부드럽게 이동시킵니다.
        while (timeElapsed < 1f)
        {
            timeElapsed += Time.deltaTime * smoothSpeed;
            doorTransform.localPosition = Vector3.Lerp(startPos, targetPos, timeElapsed);
            yield return null; // 다음 프레임까지 대기
        }

        // 오차 없이 정확한 목표 위치로 최종 고정합니다.
        doorTransform.localPosition = targetPos;
    }
}