using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using ModernJapaneseStudio;

// Optional example-scene controls. Only installs for scenes named MJS_*.
namespace ModernJapaneseStudio {
public sealed class MJSHost : MonoBehaviour
{
    public static MJSHost Instance => FindObjectOfType<MJSHost>();
    public MJSWalker Walker { get; private set; }
    public CharacterController Body { get; private set; }
    public bool Testing { get; private set; } public bool SuppressOverlay;
    public bool MenuOpen { get; private set; } = true;
    public Vector3 Spawn { get; private set; }
    public Quaternion SpawnRotation { get; private set; }
    float yaw, pitch;
    Font font;
    GUIStyle title, text, small, button;
    string notice = "";
    float noticeUntil;
    static readonly string[] ids = { "Studio", "TwoRoom", "LivedIn" };
    static readonly string[] labels = { "Work Studio (1K)", "One-bedroom (1LDK)", "Lived-in Studio (1R)" };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Install()
    {
        if (Instance || !SceneManager.GetActiveScene().name.StartsWith("MJS_")) return;
        if (Application.isEditor && Array.IndexOf(ids, SceneManager.GetActiveScene().name.Replace("MJS_", "")) < 0) return;
        new GameObject("Apartment demo controls").AddComponent<MJSHost>();
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        Testing = Array.IndexOf(Environment.GetCommandLineArgs(), "--mjs-internal-test") >= 0 || Array.IndexOf(Environment.GetCommandLineArgs(), "--mirror-audit") >= 0 || Array.IndexOf(Environment.GetCommandLineArgs(), "--mirror-fixed-check") >= 0;
        SceneManager.sceneLoaded += Loaded;
        Bind();
    }

    IEnumerator Start()
    {
        font = Font.CreateDynamicFontFromOSFont(new[] { "Malgun Gothic", "맑은 고딕", "Arial" }, 22);
        yield return null;
        SetMenu(true);
        
    }

    void OnDestroy() { SceneManager.sceneLoaded -= Loaded; }

    void Loaded(Scene scene, LoadSceneMode mode) { Bind(); SetMenu(false); }
    void Bind()
    {
        Walker = FindObjectOfType<MJSWalker>();
        if (!Walker) throw new InvalidOperationException("The demo scene has no review player.");

        Body = Walker.GetComponent<CharacterController>();
        Spawn = Walker.transform.position;
        SpawnRotation = Walker.transform.rotation;
        yaw = Walker.transform.eulerAngles.y;
        pitch = 0;
        Physics.SyncTransforms();
    }

    public void SetMenu(bool open)
    {
        MenuOpen = open;
        Cursor.lockState = open || Testing ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = open || Testing;
    }

    public void SelectMap(int index)
    {
        if (index < 0 || index >= ids.Length) return;
        SceneManager.LoadScene("MJS_" + ids[index]);
    }

    public void ResetStart()
    {
        Body.enabled = false;
        Walker.transform.SetPositionAndRotation(Spawn, SpawnRotation);
        Walker.eye.transform.localPosition = new Vector3(0,1.59f,0);
        Walker.eye.transform.localRotation = Quaternion.identity;
        Body.enabled = true;
        yaw = SpawnRotation.eulerAngles.y; pitch = 0;
        Physics.SyncTransforms();
        SetMenu(false);
    }

    public MJSPart AimedPart()
    {
        if (Physics.Raycast(Walker.eye.transform.position, Walker.eye.transform.forward,
            out RaycastHit hit, 2f, ~(1 << 2), QueryTriggerInteraction.Ignore))
            return hit.collider.GetComponentInParent<MJSPart>();
        return null;
    }

    public MJSSwitch AimedSwitch() { if (Physics.Raycast(Walker.eye.transform.position, Walker.eye.transform.forward, out RaycastHit hit, 2f, ~(1<<2), QueryTriggerInteraction.Ignore)) return hit.collider.GetComponentInParent<MJSSwitch>(); return null; }

    public bool Interact()
    {
        var sw = AimedSwitch(); if (sw) { sw.Toggle(); return true; }
        var part = AimedPart(); if (!part) return false; part.Toggle(); return true;
    }

    void Update()
    {
        if (!Walker || Testing) return;
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab)) SetMenu(!MenuOpen);
        for (int i = 0; i < 3; ++i)
            if (Input.GetKeyDown((KeyCode)((int)KeyCode.F1 + i))) { SelectMap(i); return; }
        if (MenuOpen) return;
        if (Input.GetKeyDown(KeyCode.R)) { ResetStart(); return; }
        if (Cursor.lockState != CursorLockMode.Locked)
        {
            if (Input.GetMouseButtonDown(0)) SetMenu(false);
            return;
        }
        yaw += Input.GetAxis("Mouse X") * 1.8f;
        pitch = Mathf.Clamp(pitch - Input.GetAxis("Mouse Y") * 1.8f, -75, 75);
        Walker.transform.rotation = Quaternion.Euler(0, yaw, 0);
        Walker.eye.transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        float h = (Input.GetKey(KeyCode.D) ? 1 : 0) - (Input.GetKey(KeyCode.A) ? 1 : 0);
        float v = (Input.GetKey(KeyCode.W) ? 1 : 0) - (Input.GetKey(KeyCode.S) ? 1 : 0);
        Vector3 movement = (Walker.transform.right * h + Walker.transform.forward * v).normalized * 2f;
        movement.y = -2f;
        Body.Move(movement * Time.deltaTime);
        if (Walker.transform.position.y < -2f) ResetStart();
        if (Input.GetKeyDown(KeyCode.E) && !Interact())
        { notice = "Aim at the moving part of a door or drawer within 2 metres."; noticeUntil = Time.unscaledTime + 2; }
    }

    void OnApplicationFocus(bool focused) { if (!focused && !Testing) SetMenu(true); }
    public void Quit() { Cursor.lockState = CursorLockMode.None; Application.Quit(); }

    void Styles()
    {
        if (title != null) return;
        title = new GUIStyle(GUI.skin.label) { font = font, fontSize = 32, fontStyle = FontStyle.Bold };
        text = new GUIStyle(GUI.skin.label) { font = font, fontSize = 21, wordWrap = true };
        small = new GUIStyle(text) { fontSize = 17 };
        button = new GUIStyle(GUI.skin.button) { font = font, fontSize = 21 };
    }

    void OnGUI()
    {
        if (!Walker || SuppressOverlay) return;
        Styles();
        float scale = Mathf.Min(Screen.width / 1280f, Screen.height / 720f);
        var original = GUI.matrix;
        GUI.matrix = Matrix4x4.TRS(new Vector3((Screen.width - 1280 * scale) / 2, (Screen.height - 720 * scale) / 2), Quaternion.identity, Vector3.one * scale);
        if (MenuOpen)
        {
            GUI.color = new Color(.045f, .06f, .065f, .96f);
            GUI.DrawTexture(new Rect(100, 48, 1080, 624), Texture2D.whiteTexture);
            GUI.color = Color.white;
            GUI.Label(new Rect(140, 80, 980, 50), "Modern Japanese Apartment | Example Scenes", title);
            GUI.Label(new Rect(140, 137, 980, 40), "Explore three examples of compact single-resident living.", text);
            for (int i = 0; i < 3; ++i)
                if (GUI.Button(new Rect(140 + i * 337, 200, 317, 62), labels[i], button)) SelectMap(i);
            GUI.Label(new Rect(140, 290, 970, 35), "W A S D  Move     |     Mouse  Look     |     E  Interact / Lights", text);
            GUI.Label(new Rect(140, 336, 970, 35), "F1 / F2 / F3  Change Scene     |     R  Reset to Entrance", text);
            GUI.Label(new Rect(140, 382, 970, 35), "Esc / Tab  Menu / Release Mouse     |     Alt + F4  Quit", text);
            GUI.Label(new Rect(140, 436, 970, 62), "Aim at a door, drawer, lid or switch within 2 metres and press E.\nMove aside if furniture or the player blocks a moving part.", small);
            if (GUI.Button(new Rect(140, 542, 470,  sixty), "Continue Current Scene", button)) SetMenu(false);
            if (GUI.Button(new Rect(642, 542, 470, sixty), "Quit Example", button)) Quit();
            GUI.Label(new Rect(140, 621, 970, 28), "Current scene: " + SceneManager.GetActiveScene().name.Replace("MJS_", ""), small);
        }
        else
        {
            GUI.color = new Color(0, 0, 0, .7f);
            GUI.DrawTexture(new Rect(16, 16, 1248, 78), Texture2D.whiteTexture);
            GUI.color = Color.white;
            GUI.Label(new Rect(32, 24, 1220, 30), "WASD Move   Mouse Look   E Interact   F1/F2/F3 Scenes   Esc Menu", small);
            GUI.Label(new Rect(32, 54, 1220, 30), SceneManager.GetActiveScene().name.Replace("MJS_", "") + "  |  R Reset", small);
            GUI.Label(new Rect(632, 344, 24, 30), "+", text);
            if (AimedPart() || AimedSwitch()) GUI.Label(new Rect(500, 395, 400, 40), "E  Interact / Light", text);
            if (noticeUntil > Time.unscaledTime) GUI.Label(new Rect(245, 630, 900, 40), notice, small);
        }
        GUI.matrix = original;
    }
    const float sixty = 60f;
}

}
