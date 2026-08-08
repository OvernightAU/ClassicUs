using System.Collections.Generic;
using Il2CppInterop.Runtime.InteropTypes.Fields;
using UnityEngine;

namespace ClassicUs.Components;

public class PopulateFreeplayPopover : MonoBehaviour
{
    private const float relativeX = 0f;
    private const float ySeparation = -0.65f;
    private const float startingYOffset = 2.5f;

    private void Start()
    {
        var manager = Object.FindAnyObjectByType<MainMenuManager>(FindObjectsInactive.Include);
        var mainUI = manager.transform.Find("MainUI").gameObject;
        var freeplayPopOver = GetComponent<FreeplayPopover>();
        var officialFreeplayPopOver = mainUI.transform.GetComponentInChildren<FreeplayPopover>(true);
        List<FreeplayPopoverButton> buttons = new List<FreeplayPopoverButton>();
        officialFreeplayPopOver.Awake(); // this allows mods like Submerged and Dleks to work, as long as they patch Awake, and not Show.
        
        int index = 0;
        foreach (var button in officialFreeplayPopOver.buttons)
        {
            var newButton = Instantiate(button, freeplayPopOver.content.transform);
            
            float targetY = startingYOffset + (index * ySeparation);
            newButton.transform.localPosition = new Vector3(relativeX, targetY, 0);
            newButton.OnPressEvent += new System.Action<FreeplayPopoverButton>((b) => freeplayPopOver.OnMapButtonPressed(b));

            buttons.Add(newButton);
            index++;
        }

        freeplayPopOver.buttons = buttons.ToArray();

        Debug.Log(officialFreeplayPopOver.name);
    }
}