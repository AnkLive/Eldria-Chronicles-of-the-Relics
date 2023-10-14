using System;
using System.Collections.Generic;
using UnityEngine;

    public class InputVarsSaveLoader : MonoBehaviour, IInitialize
    {
        private VarsData _data;
        
        private string _path;
        private IDataHandler<VarsData> _dataHandler;

        public void Initialize()
        {
            _path = Application.dataPath + "/SaveFile/save_vars.json";
            _dataHandler = new DataHandler<VarsData>(_path);
            LoadData();
        }

        public void SetDataVars(List<StringVar> stringVars)
        {
            var data = new VarsData();
            
            for (int i = 0; i < stringVars.Count; i++)
            {
                data.StringVars = stringVars;
            }
            _dataHandler.SaveData(data);
        }

        public List<StringVar> GetDataVars()
        {
            return _dataHandler.LoadData().StringVars;
        }
        
        private void LoadData()
        {
            var data = _dataHandler.LoadData();

            if (data == null)
            {
                data = new VarsData();
            }

            if (data.StringVars == null)
            {
                data.StringVars = new List<StringVar>();
            }
        }
        
        [Serializable]
        public class VarsData
        {
            public List<StringVar> StringVars = new();
        }
    }