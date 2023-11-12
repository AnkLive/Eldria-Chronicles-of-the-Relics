using UnityEngine;

public class SlotInfo : MonoBehaviour
{
    [SerializeField] private int slotId;
    
    public int GetSlotId()
    {
        return slotId;
    }

    public void SetSlotId(int id)
    {
        slotId = id;
    }
}