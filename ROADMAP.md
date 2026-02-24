Phase 1: Core Infrastructure & Safety (The Foundation)
Since AoiUtils modifies system settings, safety is the highest priority.

- [ ] Robust Backup System: Implement a mandatory "Snapshot" feature in BackupService.cs that creates a System Restore Point or exports specific registry keys before any tweak is applied.
- [ ] Elevation Handling: Create a utility to detect if the app is running as Administrator and show a professional "Restart as Admin" overlay if not.
- [ ] SystemRunner Hardening: Add strict argument sanitization and validation to SystemRunner.cs to prevent command injection.
- [ ] Logging Engine: Integrate a lightweight logging provider (e.g., Serilog) to record all system changes to %AppData%/AoiUtils/logs/ for troubleshooting.
- [ ] Global Error Boundary: Implement a global exception handler in App.axaml.cs that catches crashes and offers to send a (sanitized) report.

Phase 2: System Optimization & Tweaks

- [ ] Categorized Tweak Engine: Expand TweakService.cs to support JSON-based tweak definitions (Gaming, Privacy, Visuals, System).
- [ ] Real-time Status Detection: Implement logic to check if a tweak is already applied when the UI loads (e.g., checking registry values).
- [ ] Windows Service Manager: Create a service inside .Core to safely disable/enable non-essential Windows services (e.g., Print Spooler, SysMain).
- [ ] Context Menu Editor: Add a feature to clean up the right-click menu in Windows Explorer.

Phase 3: Package Management & Debloat

- [ ] WinGet/Choco Integration: Fully implement PackageManagerService.cs to list, search, and bulk-install apps using SystemRunner.
- [ ] Interactive Debloater:
    - [ ] Create a "Safe-to-Remove" list for UWP apps.
    - [ ] Add a "Custom" mode where users can check/uncheck specific bloatware.
- [ ] Parallel Processing: Ensure multiple app installations or debloat tasks run in an async queue with progress reporting in InstallViewModel.

Phase 4: Modern UI/UX (Avalonia Specific)

- [ ] Custom Acrylic/Mica: Enhance MainWindow.axaml with true Windows 11 Mica/Acrylic effects using Avalonia's TransparencyLevelHint.
- [ ] View Transitions: Add smooth cross-fade or slide animations when switching between Dashboard, Tweaks, and Install views.
- [ ] Theme Personalization: Implement a settings page to toggle between "Deep Dark," "Amoled," and "System" themes.
- [ ] Localization Completion:
    - [ ] Complete Resources.vi.resx (Vietnamese).
    - [ ] Add Resources.en.resx as the default fallback.
    - [ ] Implement a runtime language switcher in LocalizationManager.

Phase 5: Polish & DevOps

- [ ] Unit Tests: Create a test project for AoiUtils.Core to verify registry logic and string sanitization without actually modifying a system.
- [ ] GitHub Actions: Set up a CI pipeline to build the .slnx and run tests on every push.
- [ ] Self-Updater: Implement a simple version check against GitHub Releases to notify users of new versions.
- [ ] Documentation: Write a CONTRIBUTING.md and a user-facing WIKI explaining what each tweak actually does.

### Phase 6: Advanced System Management & Utilities

- [ ] Startup App Manager: Manage applications that launch automatically with Windows.
- [ ] Driver Management: Features for updating or managing system drivers.
- [ ] Disk Cleanup/Optimization: Tools for clearing temporary files, managing duplicate files, or performing disk defragmentation.
- [ ] Network Optimization: Options for changing DNS settings or performing network resets.
- [ ] Windows Update Control: Functionality to pause updates, manage update history, or clear the update cache.
- [ ] Software Activation Management: Windows and Office activation via calling to other APIs.
