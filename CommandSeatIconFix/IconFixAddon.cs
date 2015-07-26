using UnityEngine;

namespace CommandSeatIconFix
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class IconFixAddon: MonoBehaviour
    {
        const string brokenShaderName = "KSP/Alpha/Cutoff";
        const string fixShaderName = "KSP/Alpha/Cutoff Bumped";

        private void fixSader (AvailablePart availablePart, string brokenShaderName, Shader fixShader)
        {
            // Debug.Log ("IconFixAddon : part: " + availablePart.name);
            foreach (Renderer r in availablePart.iconPrefab.GetComponentsInChildren<Renderer>(true)) {
                foreach (Material m in r.materials) {
                    if (m.shader.name == brokenShaderName) {
                        Debug.Log ("IconFixAddon: fixing icon of: " + availablePart.name);
                        m.shader = fixShader;
                    }
                }
            }
        }

        public void Awake()
        {
            // Debug.Log ("IconFixAddon: Awake()");
            if (!PartLoader.Instance || !PartLoader.Instance.IsReady ()) {
                Debug.LogError ("IconFixAddon: can't access part loader");
                return;
            }
            Shader fixShader = Shader.Find (fixShaderName);
            if (!fixShader) {
                Debug.LogError ("IconFixAddon: can't get the fix shader");
                return;
            }
            foreach (AvailablePart availablePart in PartLoader.LoadedPartsList) {
                fixSader (availablePart, brokenShaderName, fixShader);
            }
        }
    }
}

