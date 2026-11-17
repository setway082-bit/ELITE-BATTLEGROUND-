# Battle Royale – Unity Starter Project

A starter framework for an original third-person battle-royale-style Android game.
It is **not a complete PUBG clone** and does not include commercial 3D models, maps,
animations, matchmaking, servers, or anti-cheat. Those require additional assets and
backend work.

## Requirements
- Unity Hub + Unity 2022.3 LTS or newer
- Android Build Support (SDK, NDK, OpenJDK) installed through Unity Hub
- A 3D (Built-in or URP) Unity project

## Import
1. In Unity Hub create a new **3D** project.
2. Close Unity.
3. Copy this ZIP's `Assets/Scripts` folder into your Unity project's `Assets` folder.
4. Reopen Unity and let scripts compile.
5. Create a Plane for the ground, add a Capsule for the player, and add a Camera.
6. Add `BattleRoyalePlayer.cs` to the player Capsule. Add a CharacterController component.
7. Put the Main Camera as a child of the player and position it around `(0, 2, -4)`.
8. Add `ThirdPersonCamera.cs` to the camera and drag the player into its `target` field.
9. Add `Health.cs` to player and bot objects.
10. Create an empty object named `GameManager` and add `BattleRoyaleGameManager.cs`.
11. Create a bot Capsule, add `CharacterController`, `Health`, and `BotAI`; assign the player.
12. Create an empty object named `SafeZone` and add `SafeZone.cs`.

## Mobile controls
The player script exposes public methods `SetMoveInput(Vector2)`, `SetLookInput(Vector2)`,
`Fire()`, and `StopFire()`. Connect these to on-screen joystick/buttons using Unity UI
Event Triggers or a mobile joystick package. Keyboard controls are included for editor testing:
WASD move, mouse look, left mouse fire, Space jump, Left Shift sprint.

## Android build
1. File > Build Settings (or Build Profiles) > Android > Switch Platform.
2. Player Settings: set a unique Package Name, orientation Landscape, and suitable minimum API.
3. Add your scene to the build list.
4. Connect an Android phone with USB debugging enabled, then Build And Run.

## Online multiplayer
This starter is offline with AI bots. Real online multiplayer needs a networking stack
(such as Unity Netcode for GameObjects or Photon Fusion), authoritative game servers,
lobbies/matchmaking, persistence, and anti-cheat. Do not expose trusted game logic to clients.
