using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using DebugAttribute;

namespace DebugUI
{
    public class DebugBuilder : MonoBehaviour
    {
        /* private string[] samplePaths = new string[]{
            "Settings/Sound/Volume",
            "Settings/Sound/Pitch",
            "Settings/Graphics",
            "Settings/Ramdom",
            "Cheat/Fly",
            "Cheat/No-clip",
            "Say *boop*"
        }; */

        public Button btnPrefab;
        public Panel panelPrefab;
        public Canvas canvas;

        private List<string> _existingMenus = new List<string>();
        public static Dictionary<string, Panel> livingPanels = new Dictionary<string, Panel>();

        private void OnGUI() 
        {
            if(GUILayout.Button("Build"))
            {
                //BuildMenus(samplePaths);
                BuildMenus(DebugAttributeRegistry.GetPaths());
            }
        }

        #region Main
            
        private void BuildMenus(string[] paths)
        {
            GenerateRoot(paths);

            var folderTree = SlicePaths(paths);
            for (int i = 0; i < folderTree.Length; i++)
            {
                var path = folderTree[i];
                for (int depth = 0; depth < path.Length; depth++)
                {
                    var folderPath = BuildPath(path, depth);
                    if(_existingMenus.Contains(folderPath)) continue;
                    
                    var subfolders = GetSubfolders(paths, folderPath);
                    //PrintSubfolder(folderPath, subfolders);
                    if (subfolders.Length == 0) continue;
                    
                    _existingMenus.Add(folderPath);
                    GeneratePanel(folderPath, subfolders);
                }
            }
        }

        private void GenerateRoot(string[] paths)
        {
            var folderPath = "";
            
            var subfolders = GetSubfolders(paths, folderPath);
            //PrintSubfolder(folderPath, subfolders);
            
            _existingMenus.Add("__root");
            GeneratePanel("__root", subfolders);
        }

        private void GeneratePanel(string folderPath, string[] subfolderPaths)
        {
            var parentPanel = GetParentPath(folderPath);
            var newPanel = Instantiate(panelPrefab, canvas.transform);
            newPanel.Setup(folderPath, subfolderPaths, parentPanel);
            livingPanels.Add(folderPath, newPanel);

            foreach (var subfolderPath in subfolderPaths)
            {
                GenerateButton(newPanel, subfolderPath);
            }
            
            if (folderPath != "__root")
            {
                GenerateReturnButton(newPanel);
            }

            newPanel.AdaptSize();
        }

        private void GenerateReturnButton(Panel parent)
        {
            var newButton = Instantiate(btnPrefab, parent.buttonWrapper.transform);
            var buttonLabel = newButton.GetComponentInChildren<Text>();
            buttonLabel.text = "Return";
            newButton.onClick.AddListener(() => parent.Return());
        }

        private void GenerateButton(Panel parent, string folderPath)
        {
            var newButton = Instantiate(btnPrefab, parent.buttonWrapper.transform);
            var buttonLabel = newButton.GetComponentInChildren<Text>();
            buttonLabel.text = GetFolderName(folderPath);
            newButton.onClick.AddListener(() => ClickedOnButton(parent, folderPath));
        }

        #endregion


        #region Utils

        private string[][] SlicePaths(string[] paths)
        {        
            List<string[]> slicedPaths = new List<string[]>();
            foreach (var path in paths)
            {
                var slicedPath = path.Split('/');
                slicedPaths.Add(slicedPath);
            }

            return slicedPaths.ToArray();
        }

        private string BuildPath(string[] slicedPath, int depth)
        {
            string path = "";
            for(int i = 0; i < depth + 1; i++)
            {
                path += $"{slicedPath[i]}{(i == depth ? "" : "/")}";
            }

            return path;
        }

        private string GetParentPath(string folderPath)
        {
            var splitedPath = folderPath.Split('/');
            if(splitedPath.Length == 1) return "__root";
            return BuildPath(splitedPath, splitedPath.Length - 2);
        }

        private string GetFolderName(string folderPath)
        {
            var splitedPath = folderPath.Split('/');
            return splitedPath[splitedPath.Length - 1];
        }

        private string[] GetSubfolders(string[] paths, string folderPath)
        {
            var subfolders = new List<string>();
            foreach (var path in paths)
            {
                if (path.Length == folderPath.Length) return subfolders.ToArray();
                if (!path.StartsWith(folderPath)) continue;
                
                var subPath = path.Remove(0, folderPath.Length + (string.IsNullOrEmpty(folderPath) ? 0 : 1));
                if(string.IsNullOrEmpty(subPath)) continue;

                var subfoldersName = subPath.Split('/');
                var subfolder = subfoldersName[0];
                var subfolderPath = $"{folderPath}{(string.IsNullOrEmpty(folderPath) ? "" : "/")}{subfolder}";
                if (subfolders.Contains(subfolderPath)) continue;
                
                subfolders.Add(subfolderPath);
            }

            return subfolders.ToArray();
        }

        private void ClickedOnButton(Panel panel, string path)
        {
            if(_existingMenus.Contains(path))
            {
                panel.SwitchTo(path);
            }

            else
            {
                Debug.Log($"invoking {path}");
                DebugAttributeRegistry.InvokeMethod(path);
            }
        }

        #endregion

        private void PrintSubfolder(string folder, string[] subfolders)
        {
            var subString = "";
            for(int i = 0; i < subfolders.Length; i++)
            {
                subString += $"{(i > 0 ? ", " : "")}{subfolders[i]}";
            }

            Debug.Log($"{folder} => {(subString == "" ? "/" : subString)}");
        }
    }
}