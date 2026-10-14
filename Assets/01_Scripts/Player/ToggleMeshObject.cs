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
                if (col is MeshCollider meshCollider)
                {
                    if (!isVisible)
                    {
                        // 💡 숨겨질 때: 트리거를 켜기 *전에* Convex를 먼저 true로 설정
                        meshCollider.convex = true;
                        meshCollider.isTrigger = true; // 통과 가능
                    }
                    else
                    {
                        // 💡 다시 보일 때: 
                        // 1. 트리거를 먼저 끕니다 (isTrigger = false)
                        meshCollider.isTrigger = false;
                        // 2. 그 다음 안전하게 컨벡스를 끕니다 (convex = false)
                        meshCollider.convex = false;
                    }
                }
                else
                {
                    // 일반 콜라이더(Box, Sphere 등)인 경우
                    col.isTrigger = !isVisible;
                }
            }
        }

        Debug.Log($"[{gameObject.name}] 상태 변경: 보임 = {isVisible}, 통과(Trigger) = {!isVisible}");
    }
}