using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GlobalCustom;
using LevelCustom;
namespace StartCustom
{
    public class GameManager : MonoBehaviour
    {
        private Slider ui_slider_sound;//控制整个游戏的音量
        StartAudio SA;//控制开始场景所有音源的对象
        void Start()
        {
            //ui_slider_sound = GameObject.Find("Canvas/sound").GetComponent<Slider>();
            //TextAsset text = (TextAsset)Resources.Load("U3d");
            SA = GameObject.Find("AudioManager").GetComponent<StartAudio>();//获取到对象
        }

        //退出游戏
        public void GameExit()
        {
            SA.PlayBtnClickBgm();//播放按钮音
            Application.Quit();
        }
        //进入闯关模式
        //把目标场景的场景名赋给全局变量 这样在过渡场景中才可以进入目标场景
        public void BarrierClick()
        {
            SA.PlayBtnClickBgm();
            GlobalValue.s_next_sceneName = "Barrier";
            SceneManager.LoadScene("Load");
        }
        //进入无限模式
        public void InfiniteClick()
        {
            SA.PlayBtnClickBgm();
            GlobalValue.s_next_sceneName = "Infinite";
            SceneManager.LoadScene("Load");
        }
    }

}
