using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace MMLPlayer
{
    public class MMLApp:MonoBehaviour
    {
        public MMLPlayerMain Main;

        public IEnumerator Start()
        {
#if UNITY_ANDROID || UNITY_IOS

            var listsavepath = Application.persistentDataPath + "/Musics/list.txt";
            if(File.Exists(listsavepath))
            {
                OnAppLoaded();
                yield break;
            }
            var req = new UnityWebRequest(Application.streamingAssetsPath + "/Musics/list.txt");
            req.downloadHandler = new DownloadHandlerFile(listsavepath);
            yield return req.SendWebRequest();
            var lines = File.ReadAllLines(listsavepath);
            for (int i = 0; i < lines.Length; i++)
            {
                var fpath = Application.streamingAssetsPath + "/Musics/" + lines[i].TrimEnd();
                var dreq = new UnityWebRequest(fpath);
                dreq.downloadHandler = new DownloadHandlerFile(Application.persistentDataPath + "/Musics/" + lines[i]);
                yield return dreq.SendWebRequest();
                if(!string.IsNullOrEmpty(dreq.error))
                {
                    Debug.LogError(dreq.error+" "+ fpath);
                }
            }
#endif
            OnAppLoaded();
            yield break;
        }

        private void OnAppLoaded()
        {
            Main.Init();
        }
    }
}
