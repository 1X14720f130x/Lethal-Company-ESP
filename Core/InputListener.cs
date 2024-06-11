using UnityEngine.InputSystem;
using System;
using UnityEngine;
using LC_Internal;
using System.Diagnostics.Tracing;


internal class InputListener : MonoBehaviour
{
#nullable enable
    internal static event Action? OnEndPress;
    internal static event Action? OnInsertPress;
#nullable disable
    (Func<bool>, Action)[] InputActions { get; } = [
       (() => Keyboard.current[Key.End].wasPressedThisFrame, () => InputListener.OnEndPress?.Invoke()),
       (() => Keyboard.current[Key.Insert].wasPressedThisFrame, () => InputListener.OnInsertPress?.Invoke()),

    ];

    private void ToggleMenu()
    {
        Settings.MENU.ShowMenu = !Settings.MENU.ShowMenu;

        if (GameListener.InGame)
        {
            Helper.LocalPlayer.disableLookInput = Settings.MENU.ShowMenu;
            Cursor.visible = Settings.MENU.ShowMenu;
            Cursor.lockState = (Settings.MENU.ShowMenu) ? CursorLockMode.None : CursorLockMode.Locked;
        }
    }
    private void UnloadMenu()
    {
        if (Settings.MENU.ShowMenu) ToggleMenu();
        Loader.Unload();

    }
    void Awake()
    {
        InputListener.OnEndPress += UnloadMenu;
        InputListener.OnInsertPress += ToggleMenu;

    }

    void Update()
    {
        foreach ((Func<bool> keyPressed, Action eventAction) in this.InputActions)
        {
            if (!keyPressed()) continue;
            eventAction();
        }
    }

    void OnDestroy()
    {

        InputListener.OnEndPress -= UnloadMenu;
        InputListener.OnInsertPress -= ToggleMenu;
    }
}

