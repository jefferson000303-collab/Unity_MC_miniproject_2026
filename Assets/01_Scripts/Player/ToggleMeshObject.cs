using UnityEngine;

public class ToggleMeshObject : MonoBehaviour, IToggleable
{
    private MeshRenderer targetMeshRenderer;

    void Start()
    {
        // 내 자신이나 자식 오브젝트에서 MeshRenderer 찾아오기
        targetMeshRenderer = GetComponent<MeshRenderer>();
        if (targetMeshRenderer == null)
        {
            targetMeshRenderer = GetComponentInChildren<MeshRenderer>();
        }
    }

    // IToggleable 인터페이스 구현
    public void Toggle()
    {
        if (targetMeshRenderer != null)
        {
            // 메쉬 렌더러 켜기/끄기 반전
            targetMeshRenderer.enabled = !targetMeshRenderer.enabled;
            Debug.Log($"메쉬 렌더러 상태: {(targetMeshRenderer.enabled ? "켜짐" : "꺼짐")}");
        }
    }
}