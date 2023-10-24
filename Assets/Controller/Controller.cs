//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/Controller/Controller.inputactions
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

public partial class @Controller : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controller"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""04028198-fdb0-4677-af62-72bebc69e753"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""5390de40-abc5-4442-8a37-e054f861acdb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""abbf76cb-ec3e-4798-bb70-a39fcc903d11"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""8976523f-677a-4b70-97c0-e4dfb7fbee5e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""edf78022-1309-47cc-bac8-10f521e5a65e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AddWeaponItem"",
                    ""type"": ""Button"",
                    ""id"": ""6eef82f5-858d-49c9-bc64-7dd8fe2acebc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AddWeaponItem1"",
                    ""type"": ""Button"",
                    ""id"": ""898e2e2e-d67b-4152-bf0b-07b09b1b7192"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AddArtefactItem"",
                    ""type"": ""Button"",
                    ""id"": ""635a8679-9b44-487a-80f3-5af2c6befbeb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AddArtefactItem1"",
                    ""type"": ""Button"",
                    ""id"": ""c08a2ec8-aeb9-4d04-872d-bd2e25da5833"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AddSpellItem"",
                    ""type"": ""Button"",
                    ""id"": ""ccf7093a-57e3-4d1b-8dbb-6b30571156ad"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""AddSpellItem1"",
                    ""type"": ""Button"",
                    ""id"": ""78e3dbc4-881c-4b64-93f5-c8756eea0d51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LeftMouse"",
                    ""type"": ""Button"",
                    ""id"": ""28e33b74-aa1e-4d8f-87fc-7eff7919178f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""701f79c0-7d54-4294-90b0-4a5bffd28e95"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""8b895826-68d3-452c-a53a-35baad6e49c7"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d46b37e3-2312-4e2c-81be-1996c8b4715f"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""4f47c39b-2e21-4ac3-ae81-e3d7e7bb7fdb"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""18cd6615-dd64-4155-b728-5eb4acc7e2af"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""f4b6c203-badd-4fe0-8242-6ecba09ea89e"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""1f4509be-bdf1-438d-b95c-3208c233bdd6"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""be964877-5e77-4ef0-99f3-360451971767"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddWeaponItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d0d62bb4-1102-48dd-bf88-ed3ab410eaa8"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddArtefactItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94173293-50e8-4829-afbe-afed036a7404"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddSpellItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d644a430-0b43-4f81-b0a2-9e5e6b137cb9"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftMouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""10c72034-a898-45d3-afe6-07679b217bda"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddWeaponItem1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30126b4f-4c85-49b1-b05a-2cf0a56cf41d"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddArtefactItem1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""bfe3f769-8cdc-48c4-864a-abcb11ae4a1f"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""AddSpellItem1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Main
        m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
        m_Main_Jump = m_Main.FindAction("Jump", throwIfNotFound: true);
        m_Main_Move = m_Main.FindAction("Move", throwIfNotFound: true);
        m_Main_Dash = m_Main.FindAction("Dash", throwIfNotFound: true);
        m_Main_Inventory = m_Main.FindAction("Inventory", throwIfNotFound: true);
        m_Main_AddWeaponItem = m_Main.FindAction("AddWeaponItem", throwIfNotFound: true);
        m_Main_AddWeaponItem1 = m_Main.FindAction("AddWeaponItem1", throwIfNotFound: true);
        m_Main_AddArtefactItem = m_Main.FindAction("AddArtefactItem", throwIfNotFound: true);
        m_Main_AddArtefactItem1 = m_Main.FindAction("AddArtefactItem1", throwIfNotFound: true);
        m_Main_AddSpellItem = m_Main.FindAction("AddSpellItem", throwIfNotFound: true);
        m_Main_AddSpellItem1 = m_Main.FindAction("AddSpellItem1", throwIfNotFound: true);
        m_Main_LeftMouse = m_Main.FindAction("LeftMouse", throwIfNotFound: true);
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
    private readonly InputAction m_Main_Jump;
    private readonly InputAction m_Main_Move;
    private readonly InputAction m_Main_Dash;
    private readonly InputAction m_Main_Inventory;
    private readonly InputAction m_Main_AddWeaponItem;
    private readonly InputAction m_Main_AddWeaponItem1;
    private readonly InputAction m_Main_AddArtefactItem;
    private readonly InputAction m_Main_AddArtefactItem1;
    private readonly InputAction m_Main_AddSpellItem;
    private readonly InputAction m_Main_AddSpellItem1;
    private readonly InputAction m_Main_LeftMouse;
    public struct MainActions
    {
        private @Controller m_Wrapper;
        public MainActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_Main_Jump;
        public InputAction @Move => m_Wrapper.m_Main_Move;
        public InputAction @Dash => m_Wrapper.m_Main_Dash;
        public InputAction @Inventory => m_Wrapper.m_Main_Inventory;
        public InputAction @AddWeaponItem => m_Wrapper.m_Main_AddWeaponItem;
        public InputAction @AddWeaponItem1 => m_Wrapper.m_Main_AddWeaponItem1;
        public InputAction @AddArtefactItem => m_Wrapper.m_Main_AddArtefactItem;
        public InputAction @AddArtefactItem1 => m_Wrapper.m_Main_AddArtefactItem1;
        public InputAction @AddSpellItem => m_Wrapper.m_Main_AddSpellItem;
        public InputAction @AddSpellItem1 => m_Wrapper.m_Main_AddSpellItem1;
        public InputAction @LeftMouse => m_Wrapper.m_Main_LeftMouse;
        public InputActionMap Get() { return m_Wrapper.m_Main; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
        public void SetCallbacks(IMainActions instance)
        {
            if (m_Wrapper.m_MainActionsCallbackInterface != null)
            {
                @Jump.started -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnJump;
                @Move.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMove;
                @Dash.started -= m_Wrapper.m_MainActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnDash;
                @Inventory.started -= m_Wrapper.m_MainActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnInventory;
                @AddWeaponItem.started -= m_Wrapper.m_MainActionsCallbackInterface.OnAddWeaponItem;
                @AddWeaponItem.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnAddWeaponItem;
                @AddWeaponItem.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnAddWeaponItem;
                @AddWeaponItem1.started -= m_Wrapper.m_MainActionsCallbackInterface.OnAddWeaponItem1;
                @AddWeaponItem1.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnAddWeaponItem1;
                @AddWeaponItem1.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnAddWeaponItem1;
                @AddArtefactItem.started -= m_Wrapper.m_MainActionsCallbackInterface.OnAddArtefactItem;
                @AddArtefactItem.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnAddArtefactItem;
                @AddArtefactItem.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnAddArtefactItem;
                @AddArtefactItem1.started -= m_Wrapper.m_MainActionsCallbackInterface.OnAddArtefactItem1;
                @AddArtefactItem1.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnAddArtefactItem1;
                @AddArtefactItem1.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnAddArtefactItem1;
                @AddSpellItem.started -= m_Wrapper.m_MainActionsCallbackInterface.OnAddSpellItem;
                @AddSpellItem.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnAddSpellItem;
                @AddSpellItem.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnAddSpellItem;
                @AddSpellItem1.started -= m_Wrapper.m_MainActionsCallbackInterface.OnAddSpellItem1;
                @AddSpellItem1.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnAddSpellItem1;
                @AddSpellItem1.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnAddSpellItem1;
                @LeftMouse.started -= m_Wrapper.m_MainActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnLeftMouse;
                @LeftMouse.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnLeftMouse;
            }
            m_Wrapper.m_MainActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @AddWeaponItem.started += instance.OnAddWeaponItem;
                @AddWeaponItem.performed += instance.OnAddWeaponItem;
                @AddWeaponItem.canceled += instance.OnAddWeaponItem;
                @AddWeaponItem1.started += instance.OnAddWeaponItem1;
                @AddWeaponItem1.performed += instance.OnAddWeaponItem1;
                @AddWeaponItem1.canceled += instance.OnAddWeaponItem1;
                @AddArtefactItem.started += instance.OnAddArtefactItem;
                @AddArtefactItem.performed += instance.OnAddArtefactItem;
                @AddArtefactItem.canceled += instance.OnAddArtefactItem;
                @AddArtefactItem1.started += instance.OnAddArtefactItem1;
                @AddArtefactItem1.performed += instance.OnAddArtefactItem1;
                @AddArtefactItem1.canceled += instance.OnAddArtefactItem1;
                @AddSpellItem.started += instance.OnAddSpellItem;
                @AddSpellItem.performed += instance.OnAddSpellItem;
                @AddSpellItem.canceled += instance.OnAddSpellItem;
                @AddSpellItem1.started += instance.OnAddSpellItem1;
                @AddSpellItem1.performed += instance.OnAddSpellItem1;
                @AddSpellItem1.canceled += instance.OnAddSpellItem1;
                @LeftMouse.started += instance.OnLeftMouse;
                @LeftMouse.performed += instance.OnLeftMouse;
                @LeftMouse.canceled += instance.OnLeftMouse;
            }
        }
    }
    public MainActions @Main => new MainActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IMainActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnAddWeaponItem(InputAction.CallbackContext context);
        void OnAddWeaponItem1(InputAction.CallbackContext context);
        void OnAddArtefactItem(InputAction.CallbackContext context);
        void OnAddArtefactItem1(InputAction.CallbackContext context);
        void OnAddSpellItem(InputAction.CallbackContext context);
        void OnAddSpellItem1(InputAction.CallbackContext context);
        void OnLeftMouse(InputAction.CallbackContext context);
    }
}
