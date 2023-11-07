using NaughtyAttributes;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [SerializeField, Tag] private string detectObjectTag;
    private PlayerUnit _playerUnit;
    private Controller _controller;
    
    public CheckpointManager CheckpointManager { get; set; }
    public int CheckpointId { get; set; }
    public ELevels LevelId { get; set; }
    
    private void OnEnable()
    {
        _controller.Main.Interact.performed += _ => CheckpointManager.SaveData();
        _controller.Main.Move.performed += _ => ExitCheckpoint();
        _controller.Main.Jump.performed += _ => ExitCheckpoint();
    }

    private void OnDisable()
    {
        _controller.Disable();
    }

    private void Awake()
    {
        _controller = new Controller();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(detectObjectTag))
        {
            _playerUnit = other.gameObject.GetComponent<PlayerUnit>();
            
            if (_playerUnit != null)
            {
                _controller?.Enable();
                return;
            }
            Debug.LogError($"Ошибка [CheckpointManager] - не найден компонент PlayerUnit при попытке сохранить данные");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(detectObjectTag))
        {
            _controller?.Disable();
        }
    }

    private void ExitCheckpoint()
    {
        if (CheckpointManager.IsCheckpoint)
        {
            CheckpointManager.UpdateData(_playerUnit, CheckpointId, LevelId);
        }
    }
}
