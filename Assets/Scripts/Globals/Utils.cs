using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.UI;
using System.Reflection;
using System.Linq;
namespace Global
{

    public class Utils : MonoBehaviour
    {

        private Slider LoadingBar;
        private Text LoadingText;
        private List<string> loadMessages = new List<string>()
        { "Want to work with us? Send us an email inquiring about jobs: InertiaGamesJobs@gmail.com",
            "Send feedback to InertiaGamesFeedback@gmail.com",
            "Don't forget to feed your pet today! :)",
            "Tell your mom you love her today!" };


        public static bool SoundOn, MusicOn;

        //Used to do MonoBehaviour calls such as StartCoroutine()
        private static Utils Instance;

        #region MonoBehaviour Methods

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                DestroyImmediate(this.gameObject);
                return;
            }
            Instance = this;

            DontDestroyOnLoad(this);

        }
        private void OnLevelWasLoaded(int level)
        {
            if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == "LoadingScreen")
            {

                LoadingBar = FindObjectOfType<Slider>();
                LoadingText = FindObjectOfType<Text>();
            }

        }


        #endregion

        /// <summary>
        ///     Returns a random enum
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetRandomEnum<T>()
        {
            var values = Enum.GetValues(typeof(T));
            var enumType = values.GetValue(Random.Range(0, values.Length));
            return (T)enumType;
        }

        /// <summary>
        ///     Repeat any action an amount of times
        /// </summary>
        /// <param name="func"></param>
        /// <param name="delay"></param>
        /// <returns></returns>
        public static IEnumerator RepeatActionForeverWithDelay(Action func, float delay)
        {
            while (true)
            {
                func();
                yield return new WaitForSeconds(delay);
            }
        }

        public static IEnumerator RepeatActionNumberOfTimes(Action func, int numOfTimes)
        {
            int i = 0;
            while (i < numOfTimes)
            {
                i++;
                func();
                yield return null;
            }
        }
        public static IEnumerator RepeatActionNumberOfTimesWithDelay(Action func, int numOfTimes, float delay)
        {
            int i = 0;
            while (i < numOfTimes)
            {
                i++;
                func();
                yield return new WaitForSeconds(delay);
            }
        }

        public static void LoadScene(string sceneName)
        {
            Instance.StartCoroutine(Instance.LoadSceneAsync(sceneName));
        }


        /// <summary>
        /// If we all use this the loading screen will work fine and should be on screen for 7 seconds unless the actual loading fucks up and takes long.
        /// </summary>
        /// <param name="sceneName"></param>
        /// <returns></returns>
        private IEnumerator LoadSceneAsync(string sceneName)
        {
            var loadingScreen = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("LoadingScreen");
            while (loadingScreen.progress < .9f)
                yield return null;
            loadingScreen.allowSceneActivation = true;
            while (LoadingBar == null || LoadingText == null)
                yield return null;
            LoadingBar.minValue = 0;
            LoadingBar.maxValue = 100;
            int messageIndex = Random.Range(0, loadMessages.Count);
            LoadingText.text = loadMessages[messageIndex];

            float timer = 0f;
            var load = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
            load.allowSceneActivation = false;

            float textTimer = 0;

            //7 is arbitrary and can be changed but you should also change the 8 below if you do change this.
            while (timer < 6f)
            {
                if (timer < 5f)
                    if (Random.Range(0, 100) > 94)
                    {

                        yield return new WaitForSeconds(Random.value / 1.5f);
                    }

                LoadingBar.value = (timer / 6.2f) * 100;
                timer += Time.deltaTime;
                if (textTimer > 2f)
                {
                    textTimer = 0;
                    var nextMessage = Random.Range(0, loadMessages.Count);
                    while (messageIndex == nextMessage)
                        nextMessage = Random.Range(0, loadMessages.Count);
                    messageIndex = nextMessage;
                    LoadingText.text = loadMessages[messageIndex];
                }



                textTimer += Time.deltaTime;
                yield return null;
            }
            while (load.progress < .9f)
            {
                yield return null;
            }

            yield return new WaitForSeconds(1);
            load.allowSceneActivation = true;
            yield break;
        }


        /// <summary>
        /// Save file to persistant data path with name fileName
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SaveFile<T>(T obj, string fileName)
        {
            var fullPath = Application.persistentDataPath + "/" + fileName + ".txt";
            FileStream fStream;
            if (!File.Exists(fullPath))
            {
                fStream = File.Create(fullPath);
            }
            else
                fStream = new FileStream(fullPath, FileMode.OpenOrCreate);
            if (!fStream.CanWrite)
                return false;
            if ((obj as ScriptableObject) != null)
            {
                fStream.Close();
                SaveToFileScriptable<T>(fullPath, obj);
                return true;
            }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            byte[] byteObj;
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                byteObj = ms.ToArray();
            }
            fStream.Write(byteObj, 0, byteObj.Length);

            fStream.Close();
            return true;
        }


        /// <summary>
        /// Save file to persistant data path inside of a folder 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static bool SaveFile<T>(T obj, string folderPath, string fileName)
        {
            var fullPath = Application.persistentDataPath + "/" + fileName;
            FileStream fStream;
            if (!File.Exists(fullPath))
            {
                fStream = File.Create(fullPath);
            }
            else
                fStream = new FileStream(fullPath, FileMode.OpenOrCreate);
            if (!fStream.CanWrite)
                return false;
            if ((obj as ScriptableObject) != null)
            {
                fStream.Close();
                SaveToFileScriptable<T>(fullPath, obj);
                return true;
            }
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            byte[] byteObj;
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                byteObj = ms.ToArray();
            }
            fStream.Write(byteObj, 0, byteObj.Length);

            fStream.Close();
            return true;
        }


        public static T LoadFileToObject<T>(string fileName)
        {

            string fullPath = Application.persistentDataPath + "/" + fileName + ".txt";

            if (!File.Exists(fullPath))
                return default(T);
            FileStream fStream = new FileStream(fullPath, FileMode.Open);
            if (!fStream.CanRead)
                return default(T);
            if (typeof(T).BaseType == typeof(ScriptableObject))
            {
                fStream.Close();
                return LoadFileScriptable<T>(fullPath);
            }



            byte[] obj = new byte[fStream.Length];
            fStream.Read(obj, 0, (int)fStream.Length);
            fStream.Close();
            

            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(obj))
            {

                return (T)bf.Deserialize(ms);
            }

        }

        
        private static T LoadFileScriptable<T>(string fullPath)// where T: ScriptableObject
        {
            var content = File.ReadAllText(fullPath);
            T obj = Activator.CreateInstance<T>();
            JsonUtility.FromJsonOverwrite(content,obj);
            return obj;
        }


        private static void SaveToFileScriptable<T>(string fullPath, T obj)
        {
            var json = JsonUtility.ToJson(obj);
            File.WriteAllText(fullPath, json);
        }
    }
}