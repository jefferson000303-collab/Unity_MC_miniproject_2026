using UnityEngine;

public class ToggleMeshObject : MonoBehaviour, MeshToggle
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

        if (targetMeshRenderer == null)
        {
            Debug.LogWarning($"{gameObject.name}에 MeshRenderer가 없습니다!");
        }
    }

    // IInteractable 인터페이스 구현 (E 키를 눌러 상호작용할 때 실행됨)
    public void MeshToggle()
    {
        if (targetMeshRenderer != null)
        {
            // 현재 켜져 있으면 끄고, 꺼져 있으면 켜기 (반전)
            targetMeshRenderer.enabled = !targetMeshRenderer.enabled;

            Debug.Log($"메쉬렌더러 상태: {(targetMeshRenderer.enabled ? "켜짐" : "꺼짐")}");
        }
    }
}