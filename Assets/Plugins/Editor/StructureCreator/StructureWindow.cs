using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Plugins.StructureCreator
{
    public class StructureWindow: EditorWindow
    {
        private static readonly Dictionary<string, bool> DefaultMenuNames = new()
        {
            {"Bootstrap", true},
            {"Configs", true},
            {"Data", true},
            {"Models", true},
            {"Repositories", true},
            {"Services", true},
            {"Views", true},
            {"Interfaces", true},
        };
        
        List<string> _menuNames = new List<string>();
        
        [MenuItem("Assets/Create/Custom/Default folder Structure", false, 1)]
        private static void CreateDefaultModuleStructure()
        {
            EditorWindow.GetWindow(typeof(StructureWindow));
        }
        
        private void OnGUI () 
        {
            GUILayout.Label ("Module structure settings", EditorStyles.boldLabel);
            
            var name = EditorGUILayout.TextField ("Module name", string.Empty);
            _menuNames.Clear();

            foreach (var menuItem in DefaultMenuNames)
            {
                if (EditorGUILayout.Toggle(menuItem.Key, menuItem.Value)) _menuNames.Add(menuItem.Key);
            }

            if (!GUILayout.Button("Create")) return;
            CreateStructure(_menuNames);
            Close();
        }

        private static void CreateStructure(List<string> menuItems)
        {
            var path = GetSelectedPathOrFallback();

            menuItems.ForEach(x=> AssetDatabase.CreateFolder(path, x));
        }
        
        private static string GetSelectedPathOrFallback()
        {
            string path = "Assets";
		
            foreach (Object obj in Selection.GetFiltered(typeof(Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if ( !string.IsNullOrEmpty(path) && File.Exists(path) ) 
                {
                    path = Path.GetDirectoryName(path);
                    break;
                }
            }
            return path;
        }
    }
}