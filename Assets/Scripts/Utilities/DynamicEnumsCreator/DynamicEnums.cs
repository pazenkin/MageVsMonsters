using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Utilities.DynamicEnumsCreator
{
#if UNITY_EDITOR
    /// <summary>
    /// Кодогенератор динамических перечислений
    /// </summary>
    public static class DynamicEnums
    {
        private static string GetTemplateAndOverridenFiles(string className, out string overridenFile)
        {
            var files = Directory
                .EnumerateFiles(Application.dataPath, $"{className}_Overridden.cs", SearchOption.AllDirectories).ToList();
            if (files.Count == 0)
            {
                Debug.LogError(
                    $"[DynamicEnums] Can`t find file |{className}_Overridden.cs| in dirs under |{Application.dataPath}|");
                overridenFile = null;
                return null;
            }

            var templateFile = File.ReadAllText($"{Path.GetDirectoryName(files[0])}/{className}_Template.txt");
            overridenFile = $"{Path.GetDirectoryName(files[0])}/{className}_Overridden.cs";

            return templateFile;
        }
        
        public static void ReplaceAndSave(string className, List<string> valuesNames)
        {
            var templateFile = GetTemplateAndOverridenFiles(className, out var overridenFile);
            if (templateFile == null) return;

            var values = valuesNames.Where(e => !string.IsNullOrEmpty(e)).ToList();

            if (values.Count > 0)
            {
                foreach (var val in values)
                {
                    var valHashCode = val.GetHashCode().ToString();
                    if (templateFile.Contains(valHashCode)) continue;
                    templateFile = templateFile.Replace("@1", $"{val}={valHashCode},\n@1");
                }
            }

            templateFile = templateFile.Replace("@1", "").Replace(",\n\n}", "\n}");

            File.WriteAllText(overridenFile, templateFile);

            AssetDatabase.Refresh();

            Debug.Log($"[DynamicEnums] Enum({className}) was written to file |{Path.GetFullPath(overridenFile)}|");
        }
    }
#endif
}