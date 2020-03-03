using System.Threading;
using UnityEngine;


namespace gtfohack
{
     public class Loader
    {
        private static GameObject go;
        public static void Load()
        {
            //new Thread(() =>
            //{
                go = new GameObject();
                go.AddComponent<menu>();
                Object.DontDestroyOnLoad(go);

            //}).Start();
        }
        public static void Unload()
        {
            Object.Destroy(go);
        }
    }
}


