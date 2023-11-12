using UnityEngine;
using UnityEngine.UI;

public class UIItemIconSetter : MonoBehaviour
{
    [field: SerializeField] private Image Icon { get; set; }

    public void SetIcon(Sprite item, bool isEquipped = false)
    {
        Icon.sprite = item;
        
        if (isEquipped)
        {
            Icon.color = new Color(1, 1, 1, 0.5f);
            return;
        }
        Icon.color = new Color(1, 1, 1, 1);
    }
}