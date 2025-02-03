using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalCustom;
namespace LoadCustom
{
    public class Loadmanager : MonoBehaviour
    {
        private GameObject root;//获取到canvas对象
        private Image ui_img_process;//进度条
        private Text ui_text_process;//显示进度条信息的文本
        AsyncOperation async;//异步加载对象
        int i_process_value;//整型化后的加载进度
        // Use this for initialization
        void Start()
        {
            //获取到需要的组件并重置
            root = GameObject.Find("Canvas");
            ui_img_process = root.transform.Find("process/processfw").gameObject.GetComponent<Image>();
            ui_text_process = root.transform.Find("process/processvalue").gameObject.GetComponent<Text>();
            ui_img_process.fillAmount = 0;
            ui_text_process.text = "0%";
            StartCoroutine(loadScene());
        }
        //开启协程、加载下一场景的场景资源
        IEnumerator loadScene()
        {
            //async为0-1之间的浮点数
            async = SceneManager.LoadSceneAsync(GlobalValue.s_next_sceneName);
            yield return async;
        }
        void FixedUpdate()
        {
            ui_img_process.fillAmount = async.progress;//进度条进度更新
            i_process_value = (int)(async.progress * 100);//整型化进度条信息
            ui_text_process.text = i_process_value.ToString() + "%";//显示进度条信息
        }
    }
}

