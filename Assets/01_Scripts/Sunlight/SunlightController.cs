using UnityEngine;

public class SunlightController : MonoBehaviour
{
    [Header("조명 설정")]
    [Tooltip("제어할 태양광(Directional Light)을 연결하세요")]
    public Light sunLight;

    public void SetTimeOfDay(float time)
    {
        if (sunLight == null) return;

        // 실시간 그림자 회전 로직
        float sunAngleX = (time - 6f) * 15f;
        sunLight.transform.rotation = Quaternion.Euler(sunAngleX, -45f, 0f);

        // 주/야간 밝기 조절 로직
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
    }
}