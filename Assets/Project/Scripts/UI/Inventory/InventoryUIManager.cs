using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryUIManager : MonoBehaviour, IInitialize<InventoryUIManager>, IActivate<InventoryUIManager>
{
    [Inject] private InventoryManager _inventoryManager;
    private Controller _controller;
    [Space]
    [SerializeField] public Transform inventoryPanel;
    [SerializeField] public Transform equipmentPanel;
    [SerializeField] private Transform inventoryMainPanel;
    [SerializeField] private Transform strengthPanel;
    [SerializeField] private Transform itemDescriptionPanel;
    [SerializeField] private Transform itemStrengthPanel;
    [SerializeField] private TMP_Text itemDescriptionText;
    [SerializeField] private TMP_Text itemStrengthText;
    [SerializeField] private TMP_Text itemNameText;
    [SerializeField] private Image itemIcon;
    [Space]
    [SerializeField] private Transform equipmentSpellPanel;
    [SerializeField] private Transform equipmentWeaponPanel;
    [SerializeField] private Transform equipmentArtefactPanel;
    [Space]
    [SerializeField] private Transform inventorySpellPanel;
    [SerializeField] private Transform inventoryWeaponPanel;
    [SerializeField] private Transform inventoryArtefactPanel;
    [Space]
    [SerializeField] private bool isOpen = false;
    [SerializeField] private bool isWeaponInventoryPanel;
    [Space]
    [SerializeField] private TMP_Text inventoryStrength;
    [Space] 
    [SerializeField] private Button goToSpellPanel;
    [SerializeField] private Button goToWeaponPanel;
    [SerializeField] private Button goToArtefactPanel;
    [SerializeField] private Button itemDescriptionPanelExitButton;
    [Space] 
    [SerializeField] private Button closeInventory;
    public event Action<EItemType> OnChangeInventorySection;
    
    public void Activate()
    {
        goToSpellPanel.onClick.AddListener(SetSpellCurrentSection);
        goToWeaponPanel.onClick.AddListener(SetWeaponCurrentSection);
        goToArtefactPanel.onClick.AddListener(SetArtefactCurrentSection);
        closeInventory.onClick.AddListener(CloseInventoryWithButton);
        itemDescriptionPanelExitButton.onClick.AddListener(SetItemDescriptionPanelVisibility);
        _controller.Enable();
        _controller.Main.Inventory.performed += _ => ToggleInventory();
        _inventoryManager.OnInventoryLoaded += CloseInventory;
    }

    public void Deactivate()
    {
        goToSpellPanel.onClick.RemoveListener(SetSpellCurrentSection);
        goToWeaponPanel.onClick.RemoveListener(SetWeaponCurrentSection);
        goToArtefactPanel.onClick.RemoveListener(SetArtefactCurrentSection);
        closeInventory.onClick.RemoveListener(CloseInventoryWithButton);
        itemDescriptionPanelExitButton.onClick.RemoveListener(SetItemDescriptionPanelVisibility);
        _inventoryManager.OnInventoryLoaded -= CloseInventory;
    }

    public void Initialize()
    {
        _controller = new Controller();
        VisibleAllObjects(true);
    }

    private void SetItemDescriptionPanelVisibility()
    {
        itemDescriptionPanel.gameObject.SetActive(false);
        itemStrengthPanel.gameObject.SetActive(false);
    }

    private void CloseInventoryWithButton() //!!!-------------------
    {
        isOpen = false;
        CloseInventory();
    }

    public void SetInventoryStrength(float strength)
    {
        inventoryStrength.text = strength.ToString();
    }

    public void SetItemDescriptionText(string itemName, string descriptionText, Sprite icon, float itemStrength, EItemType type)
    {
        itemDescriptionPanel.gameObject.SetActive(true);
        
        if (type != EItemType.Weapon)
        {
            itemStrengthPanel.gameObject.SetActive(true);
        }
        else
        {
            itemStrengthPanel.gameObject.SetActive(false);
        }
        
        itemStrengthText.text = itemStrength.ToString();
        itemIcon.sprite = icon;
        itemDescriptionText.text = descriptionText;
        itemNameText.text = itemName;
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            OpenInventory();
        }
        else
        {
            CloseInventory();
        }
        strengthPanel.gameObject.SetActive(isWeaponInventoryPanel);
    }

    private void OpenInventory()
    {
        SetWeaponCurrentSection();
        inventoryMainPanel.gameObject.SetActive(true);
        inventoryPanel.gameObject.SetActive(true);
        equipmentPanel.gameObject.SetActive(true);
    }

    private void CloseInventory()
    {
        SetWeaponCurrentSection();
        inventoryPanel.gameObject.SetActive(false);
        equipmentPanel.gameObject.SetActive(false);
        inventoryMainPanel.gameObject.SetActive(false);
    }

    private void SetSpellCurrentSection()
    {
        SetItemDescriptionPanelVisibility();
        isWeaponInventoryPanel = false;
        OnChangeInventorySection?.Invoke(EItemType.Spell);
        
        inventorySpellPanel.gameObject.SetActive(true);
        equipmentSpellPanel.gameObject.SetActive(true);
        
        equipmentArtefactPanel.gameObject.SetActive(false);
        inventoryArtefactPanel.gameObject.SetActive(false);
        
        strengthPanel.gameObject.SetActive(true);
        
        equipmentWeaponPanel.gameObject.SetActive(false);
        inventoryWeaponPanel.gameObject.SetActive(false);
    }
    private void SetWeaponCurrentSection()
    {
        SetItemDescriptionPanelVisibility();
        isWeaponInventoryPanel = true;
        OnChangeInventorySection?.Invoke(EItemType.Weapon);
        
        equipmentSpellPanel.gameObject.SetActive(false);
        inventorySpellPanel.gameObject.SetActive(false);
        
        equipmentArtefactPanel.gameObject.SetActive(false);
        inventoryArtefactPanel.gameObject.SetActive(false);
        
        strengthPanel.gameObject.SetActive(false);
        
        equipmentWeaponPanel.gameObject.SetActive(true);
        inventoryWeaponPanel.gameObject.SetActive(true);
    }
    private void SetArtefactCurrentSection()
    {
        SetItemDescriptionPanelVisibility();
        isWeaponInventoryPanel = false;
        OnChangeInventorySection?.Invoke(EItemType.Artefact);
        
        equipmentSpellPanel.gameObject.SetActive(false);
        inventorySpellPanel.gameObject.SetActive(false);
        
        equipmentArtefactPanel.gameObject.SetActive(true);
        inventoryArtefactPanel.gameObject.SetActive(true);
        
        strengthPanel.gameObject.SetActive(true);
        
        equipmentWeaponPanel.gameObject.SetActive(false);
        inventoryWeaponPanel.gameObject.SetActive(false);
    }

    public void VisibleAllObjects(bool value)
    {
        strengthPanel.gameObject.SetActive(value);
        itemDescriptionPanel.gameObject.SetActive(value);
        equipmentSpellPanel.gameObject.SetActive(value);
        equipmentWeaponPanel.gameObject.SetActive(value);
        equipmentArtefactPanel.gameObject.SetActive(value);
        inventorySpellPanel.gameObject.SetActive(value);
        inventoryWeaponPanel.gameObject.SetActive(value);
        inventoryArtefactPanel.gameObject.SetActive(value);
    }
}
