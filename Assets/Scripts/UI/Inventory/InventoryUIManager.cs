using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIManager : MonoBehaviour
{
    [SerializeField] public InputVarsSaveLoader inputVarsSaveLoader;
    public GlobalStringVars stringVars;
    [Space]
    public Transform inventoryPanel;
    public Transform equipmentPanel;
    public Transform inventoryMainPanel;
    public Transform strengthPanel;
    public TMP_Text itemDescriptionText;
    [Space]
    public Transform equipmentSpellPanel;
    public Transform equipmentWeaponPanel;
    public Transform equipmentArtefactPanel;
    [Space]
    public Transform inventorySpellPanel;
    public Transform inventoryWeaponPanel;
    public Transform inventoryArtefactPanel;
    [Space]
    public bool isOpen = false;
    public bool isWeaponInventoryPanel;
    [Space]
    public TMP_Text inventoryStrength;
    [Space] 
    public Button goToSpellPanel;
    public Button goToWeaponPanel;
    public Button goToArtefactPanel;
    [Space] 
    public Button closeInventory;
    
    public event Action OnOpenInventory;
    public event Action OnCloseInventory;
    public event Action<EItemType> OnChangeInventorySection;

    public void Init()
    {
        goToSpellPanel.onClick.AddListener(SetSpellCurrentSection);
        goToWeaponPanel.onClick.AddListener(SetWeaponCurrentSection);
        goToArtefactPanel.onClick.AddListener(SetArtefactCurrentSection);
        closeInventory.onClick.AddListener(CloseInventoryWithButton);
    }

    private void OnDisable()
    {
        goToSpellPanel.onClick.RemoveListener(SetSpellCurrentSection);
        goToWeaponPanel.onClick.RemoveListener(SetWeaponCurrentSection);
        goToArtefactPanel.onClick.RemoveListener(SetArtefactCurrentSection);
        closeInventory.onClick.RemoveListener(CloseInventoryWithButton);
    }

    public void CloseInventoryWithButton()
    {
        OnOpenInventory?.Invoke();
        isOpen = false;
        CloseInventory();
    }

    public void SetInventoryStrength(float strength)
    {
        inventoryStrength.text = strength.ToString();
    }

    public void SetItemDescriptionText(string text)
    {
        itemDescriptionText.text = text;
    }

    private void Update()
    {
        ToggleInventory();
    }

    private void ToggleInventory()
    {
        if (Input.GetKeyDown(stringVars.GetVars("OPEN_CLOSE_INVENTORY")))
        {
            isOpen = !isOpen;

            if (isOpen)
            {
                OpenInventory();
            }
            else
            {
                OnCloseInventory.Invoke();
                CloseInventory();
            }
        }

        if (isWeaponInventoryPanel)
        {
            strengthPanel.gameObject.SetActive(false);
        }
        else
        {
            strengthPanel.gameObject.SetActive(true);
        }
    }

    public void OpenInventory()
    {
        inventoryMainPanel.gameObject.SetActive(true);
        inventoryPanel.gameObject.SetActive(true);
        equipmentPanel.gameObject.SetActive(true);
    }

    public void CloseInventory()
    {
        SetWeaponCurrentSection();
        inventoryPanel.gameObject.SetActive(false);
        equipmentPanel.gameObject.SetActive(false);
        inventoryMainPanel.gameObject.SetActive(false);
    }

    public void SetSpellCurrentSection()
    {
        SetItemDescriptionText("");
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
    public void SetWeaponCurrentSection()
    {
        SetItemDescriptionText("");
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
    public void SetArtefactCurrentSection()
    {
        SetItemDescriptionText("");
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
}
