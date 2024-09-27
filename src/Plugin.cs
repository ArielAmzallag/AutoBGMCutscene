using Dalamud.Game;
using Dalamud.Game.Command;
using Dalamud.IoC;
using Dalamud.Plugin;
using System;
using System.Timers;

namespace AutoBGMCutscene
{
    public class Plugin : IDalamudPlugin
    {
        public string Name => "Auto BGM Cutscene";

        private const string CommandName = "/autobgm";

        private DalamudPluginInterface PluginInterface { get; init; }
        private CommandManager CommandManager { get; init; }
        private Configuration Configuration { get; init; }
        private PluginUI PluginUi { get; init; }

        private IntPtr bgmAddressPtr;
        private IntPtr cutsceneAddressPtr;
        private Timer checkTimer;

        public Plugin(
            [RequiredVersion("1.0")] DalamudPluginInterface pluginInterface,
            [RequiredVersion("1.0")] CommandManager commandManager)
        {
            this.PluginInterface = pluginInterface;
            this.CommandManager = commandManager;

            this.Configuration = this.PluginInterface.GetPluginConfig() as Configuration ?? new Configuration();
            this.Configuration.Initialize(this.PluginInterface);

            this.PluginUi = new PluginUI(this.Configuration);

            this.CommandManager.AddHandler(CommandName, new CommandInfo(OnCommand)
            {
                HelpMessage = "Open Auto BGM Cutscene settings"
            });

            this.PluginInterface.UiBuilder.Draw += DrawUI;
            this.PluginInterface.UiBuilder.OpenConfigUi += DrawConfigUI;

            Initialize();
        }

        public void Dispose()
        {
            this.PluginUi.Dispose();
            this.CommandManager.RemoveHandler(CommandName);
            this.checkTimer?.Dispose();
        }

        private void OnCommand(string command, string args)
        {
            this.PluginUi.SettingsVisible = true;
        }

        private void DrawUI()
        {
            this.PluginUi.Draw();
        }

        private void DrawConfigUI()
        {
            this.PluginUi.SettingsVisible = true;
        }

        private void Initialize()
        {
            // TODO: Find and set the memory addresses for BGM status and cutscene detection
            bgmAddressPtr = IntPtr.Zero; // Replace with actual address
            cutsceneAddressPtr = IntPtr.Zero; // Replace with actual address

            checkTimer = new Timer(1000); // Check every second
            checkTimer.Elapsed += CheckCutsceneAndBGM;
            checkTimer.Start();
        }

        private void CheckCutsceneAndBGM(object sender, ElapsedEventArgs e)
        {
            if (!Configuration.Enabled) return;

            bool inCutscene = IsInCutscene();
            bool bgmEnabled = IsBGMEnabled();

            if (inCutscene && !bgmEnabled)
            {
                EnableBGM();
            }
        }

        private bool IsInCutscene()
        {
            // TODO: Implement cutscene detection logic
            // This will involve reading memory at cutsceneAddressPtr
            return false;
        }

        private bool IsBGMEnabled()
        {
            // TODO: Implement BGM status check logic
            // This will involve reading memory at bgmAddressPtr
            return false;
        }

        private void EnableBGM()
        {
            // TODO: Implement BGM activation logic
            // This will involve writing to memory at bgmAddressPtr
        }
    }
}