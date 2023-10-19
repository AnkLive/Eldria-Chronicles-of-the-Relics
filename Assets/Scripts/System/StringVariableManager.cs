using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "New Global String Vars", menuName = "System/Global String Vars", order = 0)]
public class StringVariableManager : ScriptableObject
{ 
        [SerializeField, JsonIgnore] private InputVarsSaveLoader inputVarsSaveLoader;
        [field: SerializeField] private List<StringVariable> StringVarsList = new();
        
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
        
        public List<StringVariable> GetVarsList ()
        {
                return StringVarsList;
        }
}
