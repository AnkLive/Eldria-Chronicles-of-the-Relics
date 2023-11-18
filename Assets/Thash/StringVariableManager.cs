// using System;
// using System.Collections.Generic;
// using Newtonsoft.Json;
// using UnityEngine;
// using Zenject;
//
// [Serializable]
// [CreateAssetMenu(fileName = "New Global String Vars", menuName = "System/Global String Vars", order = 0)]
// public class StringVariableManager : ScriptableObject, IVariableHandler, IInitialize<StringVariableManager>
// { 
//         [Inject, JsonIgnore] private ISaveLoader<StringVariableManager> _inputVarsSaveLoader;
//         [field: SerializeField] private List<StringVariable> stringVarsList = new();
//
//         public void Initialize()
//         {
//                 SetVars(_inputVarsSaveLoader.GetData());
//         }
//         
//         public void SetVars(StringVariableManager data)
//         {
//                 stringVarsList = data.stringVarsList;
//         }
//
//         public KeyCode GetVars(string keyCodeString)
//         {
//                 foreach (var stringVars in stringVarsList)
//                 {
//                         if (stringVars.keyCode == keyCodeString)
//                         {
//                                 return (KeyCode)Enum.Parse(typeof(KeyCode), stringVars.vars);
//                         }
//                 }
//
//                 Debug.LogError(
//                         $"[GlobalStringVars] Ошибка: при попытке получить код клавиши - {keyCodeString}\n клавиши с таким кодом нету.");
//                 return KeyCode.F;
//         }
//         
//         // public List<StringVariable> GetVarsList ()
//         // {
//         //         return stringVarsList;
//         // }
// }
