using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    public class GameMenu : EditorWindow
    {
        [MenuItem("Game/Data/Clear Vars data file")]
        public static void ClearVarsData()
        {
            string path = Application.dataPath + "/SaveFile/save_vars.json";
            File.WriteAllText(path, "");
        }
    
        [MenuItem("Game/Data/Clear Inventory data file")]
        public static void ClearInventoryData()
        {
            string path = Application.dataPath + "/SaveFile/save_inventory.json";
            File.WriteAllText(path, "");
        }
    
        [MenuItem("Game/Data/Clear all data files")]
        public static void ClearAllData()
        {
            List<string> dataPathList = new()
            {
                Application.dataPath + "/SaveFile/save_inventory.json",
                Application.dataPath + "/SaveFile/save_vars.json"
            };
            foreach (var path in dataPathList)
            {
                File.WriteAllText(path, "");
            }
        }
        
        [MenuItem("Game/Change Scene/Main Menu Scene")]
        public static void ChangeToMainMenuScene()
        {
            string scenePath = "Assets/Scenes/MainMenuScene.unity";
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(scenePath);
        }
        
        [MenuItem("Game/Change Scene/Game Play Scene")]
        public static void ChangeToGamePlayScene()
        {
            string scenePath = "Assets/Scenes/GamePlayScene.unity";
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(scenePath);
        }
        
        [MenuItem("Game/Change Scene/Loading Game Scene")]
        public static void ChangeToLoadingGameScene()
        {
            string scenePath = "Assets/Scenes/LoadingGameScene.unity";
            EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            EditorSceneManager.OpenScene(scenePath);
        }
    }
}