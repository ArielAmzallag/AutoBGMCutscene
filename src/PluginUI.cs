using ImGuiNET;
using System;
using System.Numerics;

namespace AutoBGMCutscene
{
    public class PluginUI : IDisposable
    {
        private Configuration Configuration;

        public bool SettingsVisible = false;

        public PluginUI(Configuration configuration)
        {
            this.Configuration = configuration;
        }

        public void Dispose() { }

        public void Draw()
        {
            DrawSettingsWindow();
        }

        public void DrawSettingsWindow()
        {
            if (!SettingsVisible)
            {
                return;
            }

            ImGui.SetNextWindowSize(new Vector2(232, 75), ImGuiCond.Always);
            if (ImGui.Begin("Auto BGM Cutscene Settings", ref SettingsVisible,
                ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoScrollbar | ImGuiWindowFlags.NoScrollWithMouse))
            {
                var enabled = this.Configuration.Enabled;
                if (ImGui.Checkbox("Enable Auto BGM Cutscene", ref enabled))
                {
                    this.Configuration.Enabled = enabled;
                    this.Configuration.Save();
                }
            }
            ImGui.End();
        }
    }
}