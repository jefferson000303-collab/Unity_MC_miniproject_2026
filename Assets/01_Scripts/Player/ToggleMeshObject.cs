using UnityEngine;

public class ToggleMeshObject : MonoBehaviour, IToggleable
{
    private MeshRenderer[] targetMeshRenderers;
    private Collider[] targetColliders;
    private bool isVisible = true; // 기본적으로 보이고 단단한 상태

    void Start()
    {
        // 자기 자신 및 하위의 모든 렌더러와 콜라이더 수집 (비활성화된 자식 포함)
        targetMeshRenderers = GetComponentsInChildren<MeshRenderer>(true);
        targetColliders = GetComponentsInChildren<Collider>(true);
    }

    public void Toggle()
    {
        isVisible = !isVisible; // 보이기/숨기기 상태 반전

        // 1. 렌더러 켜기 / 끄기 (시각적 처리)
        foreach (var renderer in targetMeshRenderers)
        {
            if (renderer != null)
            {
                renderer.enabled = isVisible;
            }
        }

        // 2. 콜라이더의 상태 제어 (물리적 처리)
        foreach (var col in targetColliders)
        {
            if (col != null)
            {
                // 만약 콜라이더가 Mesh Collider 타입이라면,
                // Is Trigger를 켜기 *전에* 반드시 Convex가 true여야 유니티 오류가 나지 않습니다!
                if (col is MeshCollider meshCollider)
                {
                    // 오브젝트가 숨겨질 때(!isVisible) 트리거를 켜야 하므로, 미리 Convex를 true로 설정
                    if (!isVisible)
                    {
                        meshCollider.convex = true;
                    }
                }

                // 💡 핵심 로직:
                // - 보일 때(isVisible = true) -> isTrigger = false (단단하게 막힘, 부딪힘)
                // - 숨겨질 때(isVisible = false) -> isTrigger = true (통과 가능)
                col.isTrigger = !isVisible;
            }
        }

        Debug.Log($"[{gameObject.name}] 상태 변경: 보임 = {isVisible}, 통과(Trigger) = {!isVisible}");
    }
}