using UnityEngine;

public class PlayerStatsSaveLoader : MonoBehaviour, IInitialize
{
    private string _path;
    private IDataHandler<Player> _dataHandler;
    private Player _player;

    public void Initialize()
    {
        _path = Application.dataPath + "/SaveFile/player_stats.json";
        _dataHandler = new DataHandler<Player>(_path);
        LoadData();
    }

    public Player GetPlayer()
    {
        return _player;
    }

    public void SetPlayer(Player player)
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