using UnityEngine;

public class PlayerStatsSaveLoader : MonoBehaviour, IInitialize<PlayerStatsSaveLoader>, ISaveLoader<Player>
{
    private string _path;
    private IDataHandler<Player> _dataHandler;
    private Player _player;

    public void Initialize()
    {
        _path = Application.dataPath + "/SaveFile/player_stats.json";
        _dataHandler = new DataHandler<Player>(_path);
        LoadData();
        Debug.LogWarning("PlayerStatsSaveLoader");
    }

    public Player GetData()
    {
        return _player;
    }

    public void SetData(Player player)
    {
        _player = player;
        _dataHandler.SaveData(player);
    }

    private void LoadData()
    {
        _player = _dataHandler.LoadData();

        if (_player == null)
        {
            _player = new Player();
        }
        Debug.LogWarning("Данные игрока загружены");
    }
}