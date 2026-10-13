using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Roof Object Control")]
    public GameObject roofObject;
    // ==================================================
    // 1. UI 패널 및 요소
    // ==================================================
    [Header("UI Panels (UML 호환)")]
    public GameObject UI;                 // Canvas 전체 (VR 진입 시 꺼짐)
    public GameObject spaceStylingPanel;  // 사용자 지정 믹스 패널
    public GameObject estimatePanel;      // 견적 패널

    [Header("UI Elements")]
    public Text totalCostText, timeText;
    public Text costLivingText, costBedText, costBathText;
    public Text mixLivingText, mixBedText, mixBathText;
    public Toggle mixModeToggle;
    public Image[] personaBtnImages;      // 5종 버튼 이미지

    // ==================================================
    // 2. 3D 환경 및 방 관리자
    // ==================================================
    [Header("3D Environment (실제 방 컨트롤러 할당)")]
    [Tooltip("각 방의 최상위 오브젝트에 RoomThemeController를 붙이고 여기에 할당하세요")]
    public RoomThemeController livingRoom;
    public RoomThemeController bedroom;
    public RoomThemeController bathroom;

    [Header("외부 컨트롤러 참조")]
    public SunlightController sunlightController;

    // ==================================================
    // 3. UI ↔ VR 모드 전환 제어
    // ==================================================
    [Header("VR Transition (모드 전환)")]
    [Tooltip("UI 화면을 비출 때 사용할 고정 카메라 (보통 Main Camera)")]
    public GameObject uiCamera;

    [Tooltip("PlayerController 스크립트가 붙어있는 1인칭 플레이어 오브젝트")]
    public GameObject vrPlayer;

    [Tooltip("UI 캔버스에 있는 'VR 모드 입장' 버튼을 할당하세요")]
    public Button enterVRButton;

    // ==================================================
    // 4. 데이터 세팅
    // ==================================================
    private const int BaseHouseCost = 150000000;
    private readonly int[] costLiving = { 0, 5000000, 7000000, 2000000, 8500000 };
    private readonly int[] costBedroom = { 0, 1500000, 3000000, 2500000, 5000000 };
    private readonly int[] costBath = { 0, 2000000, 2000000, 2000000, 2000000 };
    private readonly string[] personaNames = { "기본 모던", "홈호스트", "테크 노마드", "웰니스 휴식", "사운드랩 믹서" };

    private int curLiving = 0, curBed = 0, curBath = 0;
    private bool isMixMode = false;

    private void Start()
    {
        if (roofObject != null) roofObject.SetActive(false);
        // 이벤트 자동 연결
        if (mixModeToggle != null) mixModeToggle.onValueChanged.AddListener(OnMixToggleChanged);
        if (enterVRButton != null) enterVRButton.onClick.AddListener(EnterVRMode);

        // ==================================================
        // [핵심] 시작 시 무조건 UI 모드로 강제 세팅
        // ==================================================
        SetUIModeActive();

        OnPersonaSelected(0); // 기본 모던(0번)으로 초기 렌더링
    }

    // ==================================================
    // UI 조작 이벤트
    // ==================================================
    public void OnPersonaSelected(int typeIndex)
    {
        if (isMixMode && mixModeToggle != null) { mixModeToggle.isOn = false; }
        curLiving = typeIndex; curBed = typeIndex; curBath = typeIndex;
        UpdateSceneAndCost();
    }

    public void CycleLivingMix() { curLiving = (curLiving + 1) % 5; UpdateSceneAndCost(); }
    public void CycleBedMix() { curBed = (curBed + 1) % 5; UpdateSceneAndCost(); }
    public void CycleBathMix() { curBath = (curBath + 1) % 5; UpdateSceneAndCost(); }

    public void OnTimeSliderChanged(float time)
    {
        if (sunlightController != null) sunlightController.SetTimeOfDay(time);

        if (timeText != null)
        {
            bool isDay = (time > 5f && time < 19f);
            timeText.text = $"{Mathf.FloorToInt(time)}시 ({(isDay ? "주간" : "야간")})";
        }
    }

    // ==================================================
    // 각 방(Room) 가구 갱신 및 UI 텍스트 업데이트
    // ==================================================
    private void UpdateSceneAndCost()
    {
        // 1. 방별 컨트롤러에 테마 번호 전달
        if (livingRoom != null) livingRoom.SetTheme(curLiving);
        if (bedroom != null) bedroom.SetTheme(curBed);
        if (bathroom != null) bathroom.SetTheme(curBath);

        // 2. UI 텍스트 및 하이라이트 갱신
        Color activeBlue = new Color(0.86f, 0.94f, 1f);
        for (int i = 0; i < personaBtnImages.Length; i++)
        {
            if (personaBtnImages[i] == null) continue;
            personaBtnImages[i].color = (!isMixMode && i == curLiving) ? activeBlue : Color.white;
        }

        if (mixLivingText != null) mixLivingText.text = $"{personaNames[curLiving]} (+{costLiving[curLiving] / 10000}만원) ▼ 변경";
        if (mixBedText != null) mixBedText.text = $"{personaNames[curBed]} (+{costBedroom[curBed] / 10000}만원) ▼ 변경";
        if (mixBathText != null) mixBathText.text = $"{personaNames[curBath]} (+{costBath[curBath] / 10000}만원) ▼ 변경";

        int totalAdditional = costLiving[curLiving] + costBedroom[curBed] + costBath[curBath];
        if (costLivingText != null) costLivingText.text = $"+{costLiving[curLiving]:N0} 원";
        if (costBedText != null) costBedText.text = $"+{costBedroom[curBed]:N0} 원";
        if (costBathText != null) costBathText.text = $"+{costBath[curBath]:N0} 원";
        if (totalCostText != null) totalCostText.text = $"{BaseHouseCost + totalAdditional:N0} 원";
    }

    private void OnMixToggleChanged(bool isOn)
    {
        isMixMode = isOn;
        if (spaceStylingPanel != null) spaceStylingPanel.SetActive(isOn);
        UpdateSceneAndCost();
    }

    // ==================================================
    // 모드 전환 로직 (UI ↔ VR)
    // ==================================================

    // 처음에 UI를 조작하기 위한 세팅
    private void SetUIModeActive()
    {
        // 1. 1인칭 플레이어 끄기
        if (vrPlayer != null) vrPlayer.SetActive(false);

        // 2. 고정 카메라와 UI 켜기
        if (uiCamera != null) uiCamera.SetActive(true);
        if (UI != null) UI.SetActive(true);

        if (roofObject != null) roofObject.SetActive(false);

        // 3. 마우스 커서 잠금 해제 (UI 클릭을 위해)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // "VR 모드 입장" 버튼을 눌렀을 때 실행되는 함수
    public void EnterVRMode()
    {
        // 1. 고정 카메라와 UI 끄기
        if (uiCamera != null) uiCamera.SetActive(false);
        if (UI != null) UI.SetActive(false);

        // 2. 1인칭 플레이어 켜기 (켜지는 순간 PlayerController의 Start()가 실행됨)
        if (vrPlayer != null) vrPlayer.SetActive(true);

        if (roofObject != null) roofObject.SetActive(true);

        // PlayerController.cs 내부에서 마우스 커서를 잠그도록 되어있으므로,
        // 여기서는 플레이어 오브젝트만 켜주면 알아서 1인칭 조작으로 넘어갑니다.

        Debug.Log("VR 1인칭 모드로 진입했습니다! (ESC를 눌러 마우스를 켤 수 있습니다)");
    }

    public void SetTopViewMode()
    {
        // ... (기존 탑뷰 카메라 전환 코드) ...

        if (roofObject != null)
        {
            roofObject.SetActive(false); // 지붕 숨기기
        }
    }

    // 2. VR 모드 진입 시 (지붕 켜기)
    public void SetVRMode()
    {
        // ... (기존 VR/Player 카메라 전환 코드) ...

        if (roofObject != null)
        {
            roofObject.SetActive(true); // 지붕 다시 보이기
        }
    }
}