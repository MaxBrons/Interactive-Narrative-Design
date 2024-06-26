//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/_DialogueSystem/Scripts/Input/InputActionsCore.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @InputActionsCore: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActionsCore()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActionsCore"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""af5cc02a-eadc-4056-985d-01bf7c79ffcf"",
            ""actions"": [
                {
                    ""name"": ""Space"",
                    ""type"": ""Button"",
                    ""id"": ""2cac6032-8237-484e-bf88-080a292f76b7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b669c072-8c05-4180-ab2a-9f1335f0664f"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Space"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_Space = m_Default.FindAction("Space", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Default
    private readonly InputActionMap m_Default;
    private List<IDefaultActions> m_DefaultActionsCallbackInterfaces = new List<IDefaultActions>();
    private readonly InputAction m_Default_Space;
    public struct DefaultActions
    {
        private @InputActionsCore m_Wrapper;
        public DefaultActions(@InputActionsCore wrapper) { m_Wrapper = wrapper; }
        public InputAction @Space => m_Wrapper.m_Default_Space;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void AddCallbacks(IDefaultActions instance)
        {
            if (instance == null || m_Wrapper.m_DefaultActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_DefaultActionsCallbackInterfaces.Add(instance);
            @Space.started += instance.OnSpace;
            @Space.performed += instance.OnSpace;
            @Space.canceled += instance.OnSpace;
        }

        private void UnregisterCallbacks(IDefaultActions instance)
        {
            @Space.started -= instance.OnSpace;
            @Space.performed -= instance.OnSpace;
            @Space.canceled -= instance.OnSpace;
        }

        public void RemoveCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IDefaultActions instance)
        {
            foreach (var item in m_Wrapper.m_DefaultActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_DefaultActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public DefaultActions @Default => new DefaultActions(this);
    public interface IDefaultActions
    {
        void OnSpace(InputAction.CallbackContext context);
    }
}
