using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace ModernJapaneseStudio {
// Review scenes only: capture once when entering a map. No per-frame camera or update.
public sealed class MJSMirrorCapture : MonoBehaviour {
    public bool Succeeded { get; private set; }
    public double CaptureMilliseconds { get; private set; }
    public int CaptureCount { get; private set; }
    public RenderTexture Texture { get; private set; }
    ReflectionProbe probe;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void Register() {
        SceneManager.sceneLoaded -= Install;
        SceneManager.sceneLoaded += Install;
    }
    static void Install(Scene scene, LoadSceneMode mode) {
        if (!scene.name.StartsWith("MJS_") || Array.IndexOf(Environment.GetCommandLineArgs(), "--mirror-audit") >= 0) return;
        foreach (var p in FindObjectsOfType<ReflectionProbe>())
            if (p.gameObject.scene == scene && !p.GetComponent<MJSMirrorCapture>()) p.gameObject.AddComponent<MJSMirrorCapture>();
    }
    IEnumerator Start() { yield return null; CaptureOnce(); }
    void CaptureOnce() {
        probe = GetComponent<ReflectionProbe>();
        var mirror = FindObjectsOfType<MJSAsset>().FirstOrDefault(a => a.asset == "Bath_Mirror");
        if (!probe || !mirror) { Debug.LogError("Review mirror capture: missing scene probe or mirror."); return; }
        var renderers = mirror.GetComponentsInChildren<Renderer>();
        var enabled = renderers.Select(r => r.enabled).ToArray();
        Texture = new RenderTexture(128, 128, 24, RenderTextureFormat.ARGBHalf) {
            dimension = TextureDimension.Cube, useMipMap = true, autoGenerateMips = false,
            name = "Review mirror scene capture"
        };
        Texture.Create();
        var cameraObject = new GameObject("One-time mirror capture camera");
        var camera = cameraObject.AddComponent<Camera>();
        camera.enabled = false;
        camera.transform.position = probe.transform.position;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = probe.backgroundColor;
        camera.nearClipPlane = probe.nearClipPlane;
        camera.farClipPlane = probe.farClipPlane;
        camera.cullingMask = probe.cullingMask;
        camera.allowHDR = true;
        var clock = System.Diagnostics.Stopwatch.StartNew();
        try {
            foreach (var renderer in renderers) renderer.enabled = false;
            Succeeded = camera.RenderToCubemap(Texture);
            if (Succeeded) {
                Texture.GenerateMips();
                probe.mode = ReflectionProbeMode.Custom;
                probe.customBakedTexture = Texture;
                CaptureCount++;
            } else Debug.LogError("Review mirror capture did not render a cubemap.");
        } finally {
            clock.Stop();
            CaptureMilliseconds = clock.Elapsed.TotalMilliseconds;
            for (int i = 0; i < renderers.Length; ++i) renderers[i].enabled = enabled[i];
            Destroy(cameraObject);
        }
        Debug.Log("REVIEW_MIRROR_CAPTURE " + gameObject.scene.name + " success=" + Succeeded + " ms=" + CaptureMilliseconds);
    }
    void OnDestroy() {
        if (probe && probe.customBakedTexture == Texture) probe.customBakedTexture = null;
        if (Texture) { Texture.Release(); Destroy(Texture); }
    }
}
}
