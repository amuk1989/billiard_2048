using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

namespace Plugins.StructureCreator
{
    public class StructureCreator : MonoBehaviour
    {
        [MenuItem("Assets/Create/Custom/Default folder Structure", false, 1)]
        private static void CreateDefaultModuleStructure()
        {
            var path = GetSelectedPathOrFallback();
            
            AssetDatabase.CreateFolder(path, "Bootstrap");
            AssetDatabase.CreateFolder(path, "Configs");
            AssetDatabase.CreateFolder(path, "Data");
            AssetDatabase.CreateFolder(path, "Models");
            AssetDatabase.CreateFolder(path, "Repositories");
            AssetDatabase.CreateFolder(path, "Services");
            AssetDatabase.CreateFolder(path, "Views");
            
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