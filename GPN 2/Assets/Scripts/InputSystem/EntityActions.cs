//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Scripts/EntityActions.inputactions
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

public partial class @EntityActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @EntityActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""EntityActions"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""f5980c1f-73e4-4032-88bd-09073a7f9438"",
            ""actions"": [
                {
                    ""name"": ""LEFT_CLICK"",
                    ""type"": ""Button"",
                    ""id"": ""bc75af0c-ec84-45ee-a4cc-b3e98916d9ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""POS"",
                    ""type"": ""Value"",
                    ""id"": ""6e140c60-ced3-40f2-9608-30bc04489cca"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""RIGHT_CLICK"",
                    ""type"": ""Button"",
                    ""id"": ""0d6d69d5-9482-41bc-ba11-0d5c7cab52e5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""08595cf7-defc-49c1-9e02-b3b3d12a98f2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LEFT_CLICK"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4699fa2e-c529-4253-9499-727b63b42a29"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""POS"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""08db62a4-db8e-4e7f-b5b2-d94aef2d1348"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RIGHT_CLICK"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_LEFT_CLICK = m_Main.FindAction("LEFT_CLICK", throwIfNotFound: true);
        m_Main_POS = m_Main.FindAction("POS", throwIfNotFound: true);
        m_Main_RIGHT_CLICK = m_Main.FindAction("RIGHT_CLICK", throwIfNotFound: true);
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

    // Main
    private readonly InputActionMap m_Main;
    private IMainActions m_MainActionsCallbackInterface;
    private readonly InputAction m_Main_LEFT_CLICK;
    private readonly InputAction m_Main_POS;
    private readonly InputAction m_Main_RIGHT_CLICK;
    public struct MainActions
    {
        private @EntityActions m_Wrapper;
        public MainActions(@EntityActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @LEFT_CLICK => m_Wrapper.m_Main_LEFT_CLICK;
        public InputAction @POS => m_Wrapper.m_Main_POS;
        public InputAction @RIGHT_CLICK => m_Wrapper.m_Main_RIGHT_CLICK;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @LEFT_CLICK.started -= m_Wrapper.m_MainActionsCallbackInterface.OnLEFT_CLICK;
                @LEFT_CLICK.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnLEFT_CLICK;
                @LEFT_CLICK.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnLEFT_CLICK;
                @POS.started -= m_Wrapper.m_MainActionsCallbackInterface.OnPOS;
                @POS.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnPOS;
                @POS.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnPOS;
                @RIGHT_CLICK.started -= m_Wrapper.m_MainActionsCallbackInterface.OnRIGHT_CLICK;
                @RIGHT_CLICK.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnRIGHT_CLICK;
                @RIGHT_CLICK.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnRIGHT_CLICK;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @LEFT_CLICK.started += instance.OnLEFT_CLICK;
                @LEFT_CLICK.performed += instance.OnLEFT_CLICK;
                @LEFT_CLICK.canceled += instance.OnLEFT_CLICK;
                @POS.started += instance.OnPOS;
                @POS.performed += instance.OnPOS;
                @POS.canceled += instance.OnPOS;
                @RIGHT_CLICK.started += instance.OnRIGHT_CLICK;
                @RIGHT_CLICK.performed += instance.OnRIGHT_CLICK;
                @RIGHT_CLICK.canceled += instance.OnRIGHT_CLICK;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    public interface IMainActions
    {
        void OnLEFT_CLICK(InputAction.CallbackContext context);
        void OnPOS(InputAction.CallbackContext context);
        void OnRIGHT_CLICK(InputAction.CallbackContext context);
    }
}