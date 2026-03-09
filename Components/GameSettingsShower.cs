using Il2CppInterop.Runtime.InteropTypes.Fields;
using TMPro;
using UnityEngine;

namespace ClassicUs.Components;

public sealed class GameSettingsShower(nint cppPtr) : MonoBehaviour(cppPtr)
{
    public Il2CppReferenceField<TextMeshPro> Target;

    public void FixedUpdate()
    {
        if (GameOptionsManager.Instance == null) return;
        if (AmongUsClient.Instance.GameState == InnerNet.InnerNetClient.GameStates.Started || AmongUsClient.Instance.NetworkMode == NetworkModes.FreePlay)
        {
            Target.Value.text = "";
            return;
        }

        Target.Value.text = GameOptionsManager.Instance.CurrentGameOptions.ToHudString(GameData.Instance ? GameData.Instance.PlayerCount : 10);
    }
}
