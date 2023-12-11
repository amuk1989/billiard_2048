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
        
        private readonly List<string> _menuNames = new List<string>();
        private string _moduleName = "SomeModule";
        
        [MenuItem("Assets/Create/Custom/Default folder Structure", false, 1)]
        private static void CreateDefaultModuleStructure()
        {
            EditorWindow.GetWindow(typeof(StructureWindow));
        }
        
        private void OnGUI () 
        {
            GUILayout.Label ("Module structure settings", EditorStyles.boldLabel);

            foreach (var menuItem in DefaultMenuNames)
            {
                if (EditorGUILayout.Toggle(menuItem.Key, _menuNames.Contains(menuItem.Key)))
                {
                    if (_menuNames.Contains(menuItem.Key)) continue;
                    _menuNames.Add(menuItem.Key);
                }
                else
                {
                    if (!_menuNames.Contains(menuItem.Key)) continue;
                    _menuNames.Remove(menuItem.Key);
                }
            }
            
            _moduleName = EditorGUILayout.TextField ("Module name", _moduleName);
            
            if (!GUILayout.Button("Create")) return;
            
            CreateStructure(_menuNames, _moduleName);
            Close();
        }

        private static void CreateStructure(List<string> menuItems, string moduleName)
        {
            var path = GetSelectedPathOrFallback();

            menuItems.ForEach(x=> AssetDatabase.CreateFolder(path, x));
        }
        
        private static void CreateScriptAsset(string templatePath, string destName) 
        {
#if UNITY_2019_1_OR_NEWER
            UnityEditor.ProjectWindowUtil.CreateScriptAssetFromTemplateFile(templatePath, destName);
#else
	typeof(UnityEditor.ProjectWindowUtil)
		.GetMethod("CreateScriptAsset", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic)
		.Invoke(null, new object[] { templatePath, destName });
#endif
            
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