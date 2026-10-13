using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("UI & Menu Settings")]
    [Tooltip("시작할 때 켜져있어야 할 UI 메뉴 패널 오브젝트")]
    public GameObject uiMenuPanel;

    // 현재 게임 상태 (true: UI 메뉴 상태, false: 1인칭 플레이 상태)
    private bool isUIMode = true;

    void Start()
    {
        // 게임 시작 시 무조건 UI 상태로 세팅
        SetGameState(true);
    }

    void Update()
    {
        // 💡 ESC 키를 누를 때마다 UI 모드와 1인칭 모드가 양방향으로 전환되도록 토글 처리
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 현재 UI 모드면 -> 1인칭 모드로(!isUIMode = false), 
            // 현재 1인칭 모드면 -> UI 모드로(!isUIMode = true) 전환
            SetGameState(!isUIMode);
        }
    }

    /// <summary>
    /// UI 메뉴의 시작 버튼에 연결할 메서드 (인스펙터 OnClick에 등록)
    /// </summary>
    public void OnClickStartPlay()
    {
        // UI 버튼을 누르면 1인칭 플레이 상태로 전환
        SetGameState(false);
    }

    /// <summary>
    /// 게임 전체 상태(UI 모드 vs 1인칭 플레이 모드)를 일괄적으로 전환하는 핵심 메서드
    /// </summary>
    void SetGameState(bool uiMode)
    {
        isUIMode = uiMode;

        // 1. UI 패널 켜기 / 끄기
        if (uiMenuPanel != null)
        {
            uiMenuPanel.SetActive(isUIMode);
        }

        // 2. 마우스 커서 및 잠금 상태 제어
        if (isUIMode)
        {
            // UI 모드: 커서를 화면에 보이게 하고 자유롭게 풀기
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            // 1인칭 플레이 모드: 커서를 숨기고 화면 중앙에 고정
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        Debug.Log($"게임 상태 변경 -> UI 모드: {isUIMode}");
    }
}