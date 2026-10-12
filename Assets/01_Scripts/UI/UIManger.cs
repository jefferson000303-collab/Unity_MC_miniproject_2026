using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // ==================================================
    // [Inspector에서 직접 드래그해서 연결할 UI 요소들]
    // ==================================================
    [Header("UI Panels (UML 호환)")]
    public GameObject UI;                 // Canvas 전체
    public GameObject MiniMap;
    public GameObject spaceStylingPanel;  // 사용자 지정 믹스 패널
    public GameObject estimatePanel;      // 견적 패널

    [Header("UI Elements (마우스로 끌어다 넣으세요)")]
    public Text totalCostText;
    public Text costLivingText, costBedText, costBathText, timeText;
    public Text mixLivingText, mixBedText, mixBathText;
    public Toggle mixModeToggle;
    public Slider sunlightSlider;
    public Image[] personaBtnImages; // 인스펙터에서 5개로 맞추고 넣으신 버튼들

    [Header("3D Environment (씬에 있는 오브젝트 할당)")]
    public Light sunLight;
    public GameObject livingRoomModel, bedroomModel, bathroomModel;

    // ==================================================
    // [데이터 세팅]
    // ==================================================
    private const int BaseHouseCost = 150000000;

    // 5종 페르소나 데이터 (사운드랩 믹서 포함)
    private readonly int[] costLiving = { 0, 5000000, 7000000, 2000000, 8500000 };
    private readonly int[] costBedroom = { 0, 1500000, 3000000, 2500000, 5000000 };
    private readonly int[] costBath = { 0, 2000000, 2000000, 2000000, 2000000 };
    private readonly string[] personaNames = { "기본 모던", "홈호스트", "테크 노마드", "웰니스 휴식", "사운드랩 믹서" };

    // 5종 3D 방 색상
    private readonly Color[] livingColors = { Color.gray, new Color(1f, 0.5f, 0f), Color.blue, Color.green, new Color(0.2f, 0.1f, 0.3f) };
    private readonly Color[] bedColors = { Color.gray, new Color(1f, 0.7f, 0.4f), new Color(0.2f, 0.5f, 1f), new Color(0.5f, 0.8f, 0.5f), new Color(0.3f, 0.2f, 0.5f) };
    private readonly Color[] bathColors = { Color.gray, new Color(1f, 0.8f, 0.6f), new Color(0.4f, 0.6f, 1f), new Color(0.7f, 0.9f, 0.7f), new Color(0.4f, 0.4f, 0.4f) };

    // 상태 변수
    private int curLiving = 0, curBed = 0, curBath = 0;
    private bool isMixMode = false;

    private void Start()
    {
        // UI 자동 생성 코드 완전 삭제됨 (에러 및 클릭 먹통 원인 제거)

        // 유니티 씬의 기본 환경광 어둡게 (그림자 강조)
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
        RenderSettings.ambientLight = new Color(0.3f, 0.3f, 0.35f);

        // UI 이벤트 자동 연결
        if (mixModeToggle != null)
            mixModeToggle.onValueChanged.AddListener(OnMixToggleChanged);

        if (sunlightSlider != null)
            sunlightSlider.onValueChanged.AddListener(OnTimeSliderChanged);

        // 초기화
        OnPersonaSelected(0);
        if (sunlightSlider != null) OnTimeSliderChanged(sunlightSlider.value);
    }

    // ==================================================
    // [외부 노출 이벤트 및 갱신 로직]
    // ==================================================

    public void OnPersonaSelected(int typeIndex)
    {
        if (isMixMode && mixModeToggle != null) { mixModeToggle.isOn = false; }
        curLiving = typeIndex; curBed = typeIndex; curBath = typeIndex;
        UpdateSceneAndCost();
    }

    // 믹스 모드 드롭다운(버튼)이 눌렸을 때 호출할 함수들 (UI 버튼 OnClick에 연결)
    public void CycleLivingMix() { curLiving = (curLiving + 1) % 5; UpdateSceneAndCost(); }
    public void CycleBedMix() { curBed = (curBed + 1) % 5; UpdateSceneAndCost(); }
    public void CycleBathMix() { curBath = (curBath + 1) % 5; UpdateSceneAndCost(); }

    public void OnTimeSliderChanged(float time)
    {
        if (sunLight == null || timeText == null) return;

        // 실시간 그림자 회전 로직
        float sunAngleX = (time - 6f) * 15f;
        sunLight.transform.rotation = Quaternion.Euler(sunAngleX, -45f, 0f);

        bool isDay = (time > 5f && time < 19f);
        if (isDay)
        {
            float intensityMultiplier = Mathf.Sin((time - 6f) * Mathf.PI / 12f);
            sunLight.intensity = Mathf.Max(0.1f, intensityMultiplier * 1.5f);
        }
        else
        {
            sunLight.intensity = 0.05f;
        }

        timeText.text = $"{Mathf.FloorToInt(time)}시 ({(isDay ? "주간" : "야간")})";
    }

    public void UpdateBudgetDisplay(int currentTotal)
    {
        if (totalCostText != null) totalCostText.text = $"{currentTotal:N0} 원";
    }

    private void UpdateSceneAndCost()
    {
        // 3D 모델 색상 갱신
        if (livingRoomModel != null) livingRoomModel.GetComponent<Renderer>().material.color = livingColors[curLiving];
        if (bedroomModel != null) bedroomModel.GetComponent<Renderer>().material.color = bedColors[curBed];
        if (bathroomModel != null) bathroomModel.GetComponent<Renderer>().material.color = bathColors[curBath];

        // 5개 버튼 액티브 하이라이트 처리
        Color activeBlue = new Color(0.86f, 0.94f, 1f);
        for (int i = 0; i < personaBtnImages.Length; i++)
        {
            if (personaBtnImages[i] == null) continue;
            if (!isMixMode && i == curLiving) personaBtnImages[i].color = activeBlue;
            else personaBtnImages[i].color = Color.white;
        }

        // 텍스트 갱신
        if (mixLivingText != null) mixLivingText.text = $"{personaNames[curLiving]} (+{costLiving[curLiving] / 10000}만원)   ▼ 클릭";
        if (mixBedText != null) mixBedText.text = $"{personaNames[curBed]} (+{costBedroom[curBed] / 10000}만원)   ▼ 클릭";
        if (mixBathText != null) mixBathText.text = $"{personaNames[curBath]} (+{costBath[curBath] / 10000}만원)   ▼ 클릭";

        int cLiv = costLiving[curLiving], cBed = costBedroom[curBed], cBath = costBath[curBath];

        if (costLivingText != null) costLivingText.text = $"+{cLiv:N0} 원";
        if (costBedText != null) costBedText.text = $"+{cBed:N0} 원";
        if (costBathText != null) costBathText.text = $"+{cBath:N0} 원";

        UpdateBudgetDisplay(BaseHouseCost + cLiv + cBed + cBath);
    }

    private void OnMixToggleChanged(bool isOn)
    {
        isMixMode = isOn;
        if (spaceStylingPanel != null) spaceStylingPanel.SetActive(isOn);
        UpdateSceneAndCost();
    }
}