using System.IO;
using UnityEditor;
using UnityEngine;
using Utilities.DynamicEnumsCreator;

namespace Game.Editor.DynamicEnumsCreator
{
    /// <summary>
    /// Окно редактора для создания динамических перечислений
    /// </summary>
    public class DynamicEnumsCreator : EditorWindow
    {
        private static DynamicEnumsCreatorTemplates Templates => DynamicEnumsCreatorTemplates.Instance;
        private static string Path
        {
            get => EditorPrefs.GetString("DynamicEnumsPath", "Assets/Scripts/Gameplay/DynamicEnums/");
            set => EditorPrefs.SetString("DynamicEnumsPath", value);
        }
        
        private static string Name { get; set; } = "";
        
        private static string TemplateFileName => DynamicEnumName + "_Template.txt";
        private static string OverriddenFileName => DynamicEnumName + "_Overridden.cs";
        private static string ClassName => Name + "Editor";
        private static string ElementName => ClassName + "Element";
        private static string DynamicEnumName => Name + "Enum";

        [MenuItem("Tools/Dynamic Enums Creator")]
        private static void ShowWindow()
        {
            var window = GetWindow<DynamicEnumsCreator>();
            window.titleContent = new GUIContent("Создание динамического Enum");
            window.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("Дополнительные проверки в коде не осуществляются. Пожалуйста, будьте внимательны при заполнении полей ниже.", EditorStyles.helpBox);
            
            EditorGUILayout.Space();
            
            if (GUILayout.Button("Выбрать папку"))
            {
                Path = EditorUtility.SaveFolderPanel("Выберите папку для создания динамического Enum", Path, "");
                EditorGUILayout.TextField("Путь для создания", Path);
            }
            Path = EditorGUILayout.TextField("Путь для создания", Path);
            
            EditorGUILayout.Space();
            
            GUILayout.Label("Имя должно соответствовать именованию классов в C#: только латиница, CamelCase с большой буквы и т.д.", EditorStyles.helpBox);
            Name = EditorGUILayout.TextField("Имя", Name);

            EditorGUILayout.Space();
            
            if (GUILayout.Button("Создать"))
            {
                CreateDynamicEnum();
                AssetDatabase.Refresh();
                Debug.Log("[DynamicEnumsCreator] - Динамический Enum создан.");
            }
        }

        private static void CreateDynamicEnum()
        {
            CreateDirectories();
            CreateClasses();
            CreateScriptableObject();
        }

        private static void CreateDirectories()
        {
            if (string.IsNullOrEmpty(Path))
            {
                Path = "Assets/";
            }
            if (Path.Substring(Path.Length - 1, 1) != "/")
            {
                Path += "/";
            }
            
            if (!Directory.Exists($"{Path}{Name}"))
            {
                Directory.CreateDirectory($"{Path}{Name}");
            }
        }

        private static void CreateClasses()
        {
            var templateText = Templates.templateFile.text;
            templateText = templateText.Replace("@ENUM_NAME", Name);
            File.WriteAllText($"{Path}{Name}/{TemplateFileName}", templateText);

            var overriddenText = Templates.overriddenFile.text;
            overriddenText = overriddenText.Replace("@ENUM_NAME", Name);
            File.WriteAllText($"{Path}{Name}/{OverriddenFileName}", overriddenText);
        }

        private static void CreateScriptableObject()
        {
            var scriptableObjectText = Templates.scriptableObjectFile.text;
            scriptableObjectText = scriptableObjectText.Replace("@CLASS_NAME", ClassName);
            scriptableObjectText = scriptableObjectText.Replace("@ELEMENT_NAME", ElementName);
            scriptableObjectText = scriptableObjectText.Replace("@DYNAMIC_ENUM_NAME", DynamicEnumName);
            File.WriteAllText($"{Path}{Name}/{ClassName}.cs", scriptableObjectText);
        }
    }
}