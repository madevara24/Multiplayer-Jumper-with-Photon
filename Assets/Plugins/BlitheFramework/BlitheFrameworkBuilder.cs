#if UNITY_EDITOR

using BlitheFramework;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class BlitheUIEditorBuilder
{
    public static MonoScript Clases { get => clases; set => clases = value; }
    public static string ClassBaseName { get => classBaseName; set => classBaseName = value; }
    public static string ClassSingletonName { get => classSingletonName; set => classSingletonName = value; }
    public static string DatabaseName { get => databaseName; set => databaseName = value; }
    public static string EventName { get => eventName; set => eventName = value; }
    public static MonoScript ChildClass { get => childClass; set => childClass = value; }
    public static MonoScript ParentClass { get => parentClass; set => parentClass = value; }
    public static Button Button { get => button; set => button = value; }
    public static MonoScript ButtonClassReference { get => buttonClassReference; set => buttonClassReference = value; }
    public static MonoScript FactoryClass { get => factoryClass; set => factoryClass = value; }
    public static MonoScript ClientFactoryClass { get => clientFactoryClass; set => clientFactoryClass = value; }

    private static MonoScript clases;
    private static string classBaseName;
    private static string classSingletonName;
    private static string databaseName;
    private static string eventName;
    private static MonoScript childClass;
    private static MonoScript parentClass;
    private static Button button;
    private static MonoScript buttonClassReference;
    private static MonoScript factoryClass;
    private static MonoScript clientFactoryClass;
}


public class BlitheFrameworkBuilder : EditorWindow
{
    string myString = "Hello World";
    bool groupEnabled;
    bool myBool = false;
    float myFloat = 1.23f;

    // Add menu item named "My Window" to the Window menu
    [MenuItem("Blithe/Framework")]
    public static void ShowWindow()
    {
        GetWindow<BlitheFrameworkBuilder>(false, "Blite Framwork v.0.1", true);
    }
    int index = 0;
    EditorStyles styleSubTitle = new EditorStyles();
    private void OnGUI()
    {
        /*EditorGUI.DrawRect(new Rect(0, 0, position.width, 60), new Color(45.9f / 100, 78.8f / 100, 78.8f / 100));
        EditorGUI.DrawRect(new Rect(0, 60, position.width, 60), new Color(50.2f / 100, 62.7f / 100, 83.1f / 100));
        EditorGUI.DrawRect(new Rect(0, 120, position.width, 60), new Color(75.3f / 100, 72.5f / 100, 86.7f / 100));
        EditorGUI.DrawRect(new Rect(0, 180, position.width, 60), new Color(87.1f / 100, 85.1f / 100, 88.6f / 100));
        EditorGUI.DrawRect(new Rect(0, 240, position.width, 100), new Color(96.9f / 100, 95.7f / 100, 91.8f / 100));
        EditorGUI.DrawRect(new Rect(0, 340, position.width, 80), new Color(100f / 100, 89f / 100, 58f / 100));
        EditorGUI.DrawRect(new Rect(0, 420, position.width, 100), new Color(100f / 100, 71f / 100, 56.5f / 100));*/
        //create base class
        
        
        GUI.Label(new Rect(0, 0, position.width - 10, 20), "Class Creator", EditorStyles.label);
        BlitheUIEditorBuilder.ClassBaseName = EditorGUI.TextField(new Rect(10, 20, position.width - 100, 15),
            new GUIContent("Class Name"),
            BlitheUIEditorBuilder.ClassBaseName);
        if (GUI.Button(new Rect(position.width - 80, 20, 70, 15), "Create"))
        {
            ClassGeneratorCalls(BlitheUIEditorBuilder.ClassBaseName);
            Debug.Log("create class : " + BlitheUIEditorBuilder.ClassBaseName);
            BlitheUIEditorBuilder.ClassBaseName = "";
        }

        //create factory method
        GUI.Label(new Rect(0, 60, position.width - 10, 20), "Factory Method Creator", EditorStyles.label);
        BlitheUIEditorBuilder.Clases = (MonoScript)EditorGUI.ObjectField(new Rect(10, 80, position.width - 100, 15), new GUIContent("Base class") ,BlitheUIEditorBuilder.Clases, typeof(MonoScript), false);
        if (GUI.Button(new Rect(position.width - 80, 80, 70, 15), "Create"))
        {
            FactoryMethodClassnGenerator(BlitheUIEditorBuilder.Clases.name);
            Debug.Log("created factory method class : " + BlitheUIEditorBuilder.Clases.name);
            BlitheUIEditorBuilder.Clases = null;
        }

        //create singleton class
        GUI.Label(new Rect(0, 120, position.width - 10, 20), "Singleton Class Creator", EditorStyles.label);
        BlitheUIEditorBuilder.ClassSingletonName = EditorGUI.TextField(new Rect(10, 140, position.width - 100, 15),
            "Class Name",
            BlitheUIEditorBuilder.ClassSingletonName);
        if (GUI.Button(new Rect(position.width - 80, 140, 70, 15), "Create"))
        {
            SingletonClassGeneratorCalls(BlitheUIEditorBuilder.ClassSingletonName);
            Debug.Log("created singleton class : " + BlitheUIEditorBuilder.ClassSingletonName);
            BlitheUIEditorBuilder.ClassSingletonName = "";
        }

        //create database class
        GUI.Label(new Rect(0, 180, position.width - 10, 20), "Database Creator", EditorStyles.label);
        BlitheUIEditorBuilder.DatabaseName = EditorGUI.TextField(new Rect(10, 200, position.width - 100, 15),
            "Database Name",
            BlitheUIEditorBuilder.DatabaseName);
        if (GUI.Button(new Rect(position.width - 80, 200, 70, 15), "Create"))
        {
            DatabaseClassGeneratorCalls(BlitheUIEditorBuilder.DatabaseName);
            Debug.Log("created database : " + BlitheUIEditorBuilder.DatabaseName);
            BlitheUIEditorBuilder.DatabaseName = "";
        }

        //create event listener
        GUI.Label(new Rect(0, 240, position.width - 10, 20), "Event Listener Creator", EditorStyles.label);
        BlitheUIEditorBuilder.EventName = EditorGUI.TextField(new Rect(10, 260, position.width - 100, 15),
            "Event Name",
            BlitheUIEditorBuilder.EventName);
        BlitheUIEditorBuilder.ChildClass = (MonoScript)EditorGUI.ObjectField(new Rect(10, 280, position.width - 100, 15), new GUIContent("Child class"), BlitheUIEditorBuilder.ChildClass, typeof(MonoScript), false);
        BlitheUIEditorBuilder.ParentClass = (MonoScript)EditorGUI.ObjectField(new Rect(10, 300, position.width - 100, 15), new GUIContent("Parent class"), BlitheUIEditorBuilder.ParentClass, typeof(MonoScript), false);
        if (GUI.Button(new Rect(position.width - 80, 260, 70, 60), "Create"))
        {
            EventDelegateGeneratorCalls(BlitheUIEditorBuilder.EventName, BlitheUIEditorBuilder.ChildClass.name, BlitheUIEditorBuilder.ParentClass.name);
            Debug.Log(BlitheUIEditorBuilder.ChildClass);
            BlitheUIEditorBuilder.ChildClass = null;
            BlitheUIEditorBuilder.ParentClass = null;
            BlitheUIEditorBuilder.EventName = "";
        }

        //create factory aggregation
        GUI.Label(new Rect(0, 340, position.width - 10, 20), "Factory Method Aggregation Creator", EditorStyles.label);
        BlitheUIEditorBuilder.FactoryClass = (MonoScript)EditorGUI.ObjectField(new Rect(10, 360, position.width - 100, 15), new GUIContent("Factory Method class"), BlitheUIEditorBuilder.FactoryClass, typeof(MonoScript), false);
        BlitheUIEditorBuilder.ClientFactoryClass = (MonoScript)EditorGUI.ObjectField(new Rect(10, 380, position.width - 100, 15), new GUIContent("Client class"), BlitheUIEditorBuilder.ClientFactoryClass, typeof(MonoScript), false);
        if (GUI.Button(new Rect(position.width - 80, 360, 70, 40), "Create"))
        {
            FactoryAggregationGeneratorCalls(BlitheUIEditorBuilder.FactoryClass.name);
            BlitheUIEditorBuilder.ClientFactoryClass = null;
            BlitheUIEditorBuilder.FactoryClass = null;
        }

        //create checkbox dark theme
        /*GUI.Label(new Rect(0, 420, position.width - 10, 20), "Enable Light Theme", EditorStyles.label);
        myBool = (bool)EditorGUI.Toggle(new Rect(position.width/4+60, 420, position.width - 100, 15), myBool);
        if (myBool) {
            //styleSubTitle.normal.textColor = Color.black;
        }
        else
        {
            //styleSubTitle.normal.textColor = Color.grey;
        }*/
        /*
        //create button listener
        GUI.Label(new Rect(0, 420, position.width - 10, 20), "Button Listener Creator", styleSubTitle);
        BlitheUIEditorBuilder.Button = (Button)EditorGUI.ObjectField(new Rect(10, 440, position.width - 100, 15), new GUIContent("Button"), BlitheUIEditorBuilder.Button, typeof(Button), true);
        BlitheUIEditorBuilder.ButtonClassReference = (MonoScript)EditorGUI.ObjectField(new Rect(10, 460, position.width - 100, 15), new GUIContent("Factory Method class"), BlitheUIEditorBuilder.ButtonClassReference, typeof(MonoScript), true);
        BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        bool classExist = false;
        if(BlitheUIEditorBuilder.ButtonClassReference != null)
        {
            MethodInfo[] monoMethods = BlitheUIEditorBuilder.ButtonClassReference.GetClass().GetMethods(flags);
            classExist = true;
            if (classExist)
            {
                string[] methods = new string[monoMethods.Length];
                for (int i = 0; i < monoMethods.Length; i++)
                {
                    methods[i] = monoMethods[i].Name;
                }
                index = EditorGUI.Popup(new Rect(10, 480, position.width - 110, 20), index, methods);
                if (GUI.Button(new Rect(position.width - 80, 440, 70, 60), "Create"))
                {
                    Debug.Log(BlitheUIEditorBuilder.Button.name);
                }
            }
        }
        else
        {
            classExist = false;
            index = 0;
        }
        */

        //draw lock//
        bool isAuth = BliteFramework.IsHasAuthority();
        if (!isAuth)
        {
            EditorGUI.DrawRect(new Rect(0, 0, position.width, position.height), new Color(45.9f / 100, 78.8f / 100, 78.8f / 100));
            GUI.Label(new Rect(position.width / 4, position.height / 4, position.width, 20), "You are not authorized to use Blithe !", EditorStyles.label);
        }
        
    }

    //[MenuItem("Assets/Blithe Framwork/C# Base Class")]
    private static void ClassGeneratorCalls()
    {
        // Create and add a new ScriptableObject for storing configuration
        string path = Application.dataPath;
        string savePath = EditorUtility.SaveFilePanel("Save class", path, "", "cs");
        if(savePath.Length != 0)
        {
            string[] pathSaved = savePath.Split('/');
            string fileName = pathSaved[pathSaved.Length - 1];
            fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
            pathSaved[pathSaved.Length - 1] = fileName;
            savePath = null;
            for (int i = 0; i < pathSaved.Length; i++)
            {
                savePath += pathSaved[i];
                if (i < pathSaved.Length - 1)
                {
                    savePath += "/";
                }
            }
            if (savePath.Length != 0)
            {
                BlitheFramework.CodeTemplateBaseClass.generateBaseClass(fileName.Substring(0, fileName.Length - 3));
                string content = BlitheFramework.CodeTemplateBaseClass.getData();
                if (content != null)
                {
                    File.WriteAllText(savePath, content);
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    private static void ClassGeneratorCalls(string _className)
    {
        // Create and add a new ScriptableObject for storing configuration
        string path = Application.dataPath+ _className;
        string savePath = EditorUtility.SaveFilePanelInProject("Save class", _className, "cs", "class created");
        if (savePath.Length != 0)
        {
            string[] pathSaved = savePath.Split('/');
            string fileName = pathSaved[pathSaved.Length - 1];
            fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
            pathSaved[pathSaved.Length - 1] = fileName;
            savePath = null;
            for (int i = 0; i < pathSaved.Length; i++)
            {
                savePath += pathSaved[i];
                if (i < pathSaved.Length - 1)
                {
                    savePath += "/";
                }
            }
            if (savePath.Length != 0)
            {
                BlitheFramework.CodeTemplateBaseClass.generateBaseClass(fileName.Substring(0, fileName.Length - 3));
                string content = BlitheFramework.CodeTemplateBaseClass.getData();
                if (content != null)
                {
                    File.WriteAllText(savePath, content);
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    //[MenuItem("Assets/Blithe Framwork/Factory Method Class")]
    private static void FactoryMethodClassnGenerator()
    {
        // Create and add a new ScriptableObject for storing configuration
        string path = Application.dataPath;
        //string savePath = EditorUtility.SaveFilePanel("Save class", path, "", "cs");
        string baseClassPath = EditorUtility.OpenFilePanel("Choose Base Class", path, "cs");
        if(baseClassPath.Length != 0)
        {
            string[] pathSaved = baseClassPath.Split('/');
            string fileName = pathSaved[pathSaved.Length - 1];
            fileName = "Factory" + fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
            pathSaved[pathSaved.Length - 1] = fileName;
            baseClassPath = null;
            for (int i = 0; i < pathSaved.Length; i++)
            {
                baseClassPath += pathSaved[i];
                if (i < pathSaved.Length - 1)
                {
                    baseClassPath += "/";
                }
            }
            if (baseClassPath.Length != 0)
            {
                string tempClassName = fileName.Substring(7);
                BlitheFramework.CodeTemplateFactoryMethod.generateFactoryMethodClass(tempClassName.Substring(0, tempClassName.Length - 3));
                string content = BlitheFramework.CodeTemplateFactoryMethod.getData();
                if (content != null)
                {
                    File.WriteAllText(baseClassPath, content);
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    private static void FactoryMethodClassnGenerator(string _baseClassName)
    {
        string path = Application.dataPath + _baseClassName;
        string savePath = EditorUtility.SaveFilePanelInProject("Save class", "Factory"+_baseClassName, "cs", "class created");
        BlitheFramework.CodeTemplateFactoryMethod.generateFactoryMethodClass(_baseClassName);
        string content = BlitheFramework.CodeTemplateFactoryMethod.getData();
        if (content != null)
        {
            File.WriteAllText(savePath, content);
            AssetDatabase.Refresh();
        }
    }

    //[MenuItem("Assets/Blithe Framwork/Singleton Class")]
    private static void SingletonClassGeneratorCalls()
    {
        // Create and add a new ScriptableObject for storing configuration
        string path = Application.dataPath;
        string savePath = EditorUtility.SaveFilePanel("Save class", path, "", "cs");
        if (savePath.Length != 0)
        {
            string[] pathSaved = savePath.Split('/');
            string fileName = pathSaved[pathSaved.Length - 1];
            fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
            pathSaved[pathSaved.Length - 1] = fileName;
            savePath = null;
            for (int i = 0; i < pathSaved.Length; i++)
            {
                savePath += pathSaved[i];
                if (i < pathSaved.Length - 1)
                {
                    savePath += "/";
                }
            }
            if (savePath.Length != 0)
            {
                BlitheFramework.CodeTemplateSingleton.generateSingletonClass(fileName.Substring(0, fileName.Length - 3));
                string content = BlitheFramework.CodeTemplateSingleton.getData();
                if (content != null)
                {
                    File.WriteAllText(savePath, content);
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    private static void SingletonClassGeneratorCalls(string _className)
    {
        // Create and add a new ScriptableObject for storing configuration
        string path = Application.dataPath + _className;
        string savePath = EditorUtility.SaveFilePanelInProject("Save class", _className, "cs", "singleton created");
        if (savePath.Length != 0)
        {
            string[] pathSaved = savePath.Split('/');
            string fileName = pathSaved[pathSaved.Length - 1];
            fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
            pathSaved[pathSaved.Length - 1] = fileName;
            savePath = null;
            for (int i = 0; i < pathSaved.Length; i++)
            {
                savePath += pathSaved[i];
                if (i < pathSaved.Length - 1)
                {
                    savePath += "/";
                }
            }
            if (savePath.Length != 0)
            {
                BlitheFramework.CodeTemplateSingleton.generateSingletonClass(fileName.Substring(0, fileName.Length - 3));
                string content = BlitheFramework.CodeTemplateSingleton.getData();
                if (content != null)
                {
                    File.WriteAllText(savePath, content);
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    //[MenuItem("Assets/Blithe Framwork/Database Class")]
    private static void DatabaseClassGeneratorCalls()
    {
        // Create and add a new ScriptableObject for storing configuration
        string path = Application.dataPath;
        string savePath = EditorUtility.SaveFilePanel("Save class", path, "", "cs");
        if (savePath.Length != 0)
        {
            string[] pathSaved = savePath.Split('/');
            string fileName = pathSaved[pathSaved.Length - 1];
            fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
            pathSaved[pathSaved.Length - 1] = fileName;
            savePath = null;
            for (int i = 0; i < pathSaved.Length; i++)
            {
                savePath += pathSaved[i];
                if (i < pathSaved.Length - 1)
                {
                    savePath += "/";
                }
            }
            if (savePath.Length != 0)
            {
                BlitheFramework.CodeTemplateDatabase.generateDatabaseClass(fileName.Substring(0, fileName.Length - 3));
                string content = BlitheFramework.CodeTemplateDatabase.getData();
                if (content != null)
                {
                    File.WriteAllText(savePath, content);
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    private static void DatabaseClassGeneratorCalls(string _className)
    {
        // Create and add a new ScriptableObject for storing configuration
        string path = Application.dataPath+ _className;
        string savePath = EditorUtility.SaveFilePanelInProject("Save class", _className, "cs", "database created");
        if (savePath.Length != 0)
        {
            string[] pathSaved = savePath.Split('/');
            string fileName = pathSaved[pathSaved.Length - 1];
            fileName = fileName.Substring(0, 1).ToUpper() + fileName.Substring(1);
            pathSaved[pathSaved.Length - 1] = fileName;
            savePath = null;
            for (int i = 0; i < pathSaved.Length; i++)
            {
                savePath += pathSaved[i];
                if (i < pathSaved.Length - 1)
                {
                    savePath += "/";
                }
            }
            if (savePath.Length != 0)
            {
                BlitheFramework.CodeTemplateDatabase.generateDatabaseClass(fileName.Substring(0, fileName.Length - 3));
                string content = BlitheFramework.CodeTemplateDatabase.getData();
                if (content != null)
                {
                    File.WriteAllText(savePath, content);
                    AssetDatabase.Refresh();
                }
            }
        }
    }

    //[MenuItem("Assets/Blithe Framwork/Create Event Delegate")]
    private static void EventDelegateGeneratorCalls(string _eventName, string _subClassName, string _parentClasName)
    {
        string path = Application.dataPath;
        string savePath = EditorUtility.SaveFilePanelInProject("Save class", _subClassName, "cs", "sub class created");
        BlitheFramework.CodeEventDelegate.generateEventListener(BlitheUIEditorBuilder.ChildClass.ToString(), _eventName);
        string content = BlitheFramework.CodeEventDelegate.getData();
        if (content != null)
        {
            File.WriteAllText(savePath, content);
        }
        savePath = EditorUtility.SaveFilePanelInProject("Save class", _parentClasName, "cs", "sub class created");
        BlitheFramework.CodeEventDelegate.generateEventHandler(BlitheUIEditorBuilder.ParentClass.ToString(), _eventName, _subClassName);
        content = BlitheFramework.CodeEventDelegate.getData();
        if (content != null)
        {
            File.WriteAllText(savePath, content);
            AssetDatabase.Refresh();
        }
    }

    private static void FactoryAggregationGeneratorCalls(string _factoryClassName)
    {
        string path = Application.dataPath;
        string savePath = EditorUtility.SaveFilePanelInProject("Save class", BlitheUIEditorBuilder.ClientFactoryClass.name, "cs", "client class created");
        BlitheFramework.CodeFactoryAggregator.generateFactoryAggregation(BlitheUIEditorBuilder.ClientFactoryClass.ToString(), _factoryClassName);
        string content = BlitheFramework.CodeFactoryAggregator.getData();
        if (content != null)
        {
            File.WriteAllText(savePath, content);
            AssetDatabase.Refresh();
        }
    }
}
#endif