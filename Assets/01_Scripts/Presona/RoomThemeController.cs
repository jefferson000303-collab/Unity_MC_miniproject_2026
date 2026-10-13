using UnityEngine;

public class RoomThemeController : MonoBehaviour
{
    [Header("해당 방의 페르소나별 가구 묶음")]
    [Tooltip("순서: [0]기본, [1]홈호스트, [2]테크노마드, [3]웰니스, [4]사운드랩")]
    public GameObject[] themeFurnitureGroups = new GameObject[5];

    [Header("옵션: 방별 바닥/벽 재질 변경 (필요시 사용)")]
    public Renderer floorRenderer;
    public Material[] themeFloorMaterials = new Material[5];

    public void SetTheme(int themeIndex)
    {
        // 1. 선택된 번호의 가구 그룹만 켜고 나머지는 끈다.
        for (int i = 0; i < themeFurnitureGroups.Length; i++)
        {
            if (themeFurnitureGroups[i] != null)
            {
                themeFurnitureGroups[i].SetActive(i == themeIndex);
            }
        }

        // 2. 바닥 재질도 교체 (세팅된 경우만)
        if (floorRenderer != null && themeIndex < themeFloorMaterials.Length)
        {
            if (themeFloorMaterials[themeIndex] != null)
                floorRenderer.material = themeFloorMaterials[themeIndex];
        }
    }
}