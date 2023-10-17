using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Global String Vars", menuName = "System/Global String Vars", order = 0)]
public class GlobalStringVars : ScriptableObject
{ 
        [SerializeField, JsonIgnore] private InputVarsSaveLoader inputVarsSaveLoader;
        [field: SerializeField] private List<StringVar> StringVarsList = new();
        
        private void SetVars()
        {
                StringVarsList = inputVarsSaveLoader.GetData().StringVarsList;
        }

        public KeyCode GetVars(string keyCodeString)
        {
                foreach (var stringVars in StringVarsList)
                {
                        if (stringVars.keyCode == keyCodeString)
                        {
                                return (KeyCode)Enum.Parse(typeof(KeyCode), stringVars.vars);
                        }
                }

                Debug.LogError(
                        $"[GlobalStringVars] Ошибка: при попытке получить код клавиши - {keyCodeString}\n клавиши с таким кодом нету.");
                return KeyCode.F;
        }
        
        public List<StringVar> GetVarsList ()
        {
                return StringVarsList;
        }
}
