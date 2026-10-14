using UnityEngine;

public class InteractLightSwitch : MonoBehaviour, IInteractable
{
    [Header("Light Control Settings")]
    [Tooltip("켜고 끌 대상이 되는 Light 오브젝트 (또는 Light가 포함된 부모/자식 오브젝트)")]
    public GameObject targetLightObject;

    [Tooltip("시작할 때 Light의 기본 켜짐/꺼짐 상태 (true: 켜짐, false: 꺼짐)")]
    public bool isOn = true;

    void Start()
    {
        // 1. 초기 상태에 맞춰 Light 오브젝트 활성화 여부 세팅
        if (targetLightObject != null)
        {
            targetLightObject.SetActive(isOn);
        }
    }

    // E 키를 눌렀을 때 PlayerInteraction에서 호출되는 상호작용 메서드 (IInteractable 인터페이스 구현)
    public void Interact()
    {
        // 2. 현재 상태를 반전시킴 (켜져있으면 끄고, 꺼져있으면 켬)
        isOn = !isOn;

        // 3. Light 오브젝트 켜기 / 끄기 적용
        if (targetLightObject != null)
        {
            targetLightObject.SetActive(isOn);
        }

        Debug.Log($"[{gameObject.name}] 전구 상태 변경: {(isOn ? "켜짐" : "꺼짐")}");
    }
}