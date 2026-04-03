# ClassicUs

> BepInEx IL2CPP plugin for Among Us that recreates the early-2021 "classic" look and feel.

![BepInEx](https://img.shields.io/badge/BepInEx-6_IL2CPP-7b6cf6)
![.NET](https://img.shields.io/badge/.NET-6.0%2B-blue)
![License](https://img.shields.io/badge/license-GPL--3.0-green)

---

## What it changes

| Area | Detail |
|------|--------|
| Body | Forces classic body mode |
| Main menu | Replaces with a classic-style menu |
| HUD buttons | Re-skins settings, map, chat, and friends list with classic assets |
| Lobby | Adds a panel mirroring current options |
| Fonts | Applies Arial font across major UI screens |
| Visuals | Tweaks dead-body animations, meeting backgrounds, and task sprites |

---

## Requirements

| Dependency | Version | Notes |
|-----------|---------|-------|
| Among Us | 17.3 | Latest |
| BepInEx | 6.x | BepInEx-Unity.IL2CPP |
| .NET SDK | 6.0+ | Build from source only |

---

## Build
```powershell
dotnet restore ClassicUs.sln
dotnet build ClassicUs.sln
```

Output: `bin/Debug/net6.0/ClassicUs.dll`

To build **and** deploy directly into your Among Us install:
```powershell
dotnet build ClassicUs.sln -p:AmongUs="C:\Path\To\Among Us"
# Places the DLL in <AmongUs>\BepInEx\plugins\
```

---

## Install manually

1. Build the project.
2. Copy `ClassicUs.dll` → `<AmongUs>\BepInEx\plugins\`
3. Launch the game with BepInEx enabled.

---

## License

[GPL-3.0](LICENSE)