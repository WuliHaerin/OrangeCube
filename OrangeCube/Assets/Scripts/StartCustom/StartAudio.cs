using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GlobalCustom;
namespace StartCustom
{
    public class StartAudio : MonoBehaviour
    {
        public Slider soundslider;//音量条
        private AudioSource BtnClick;//按钮点击声
        private AudioSource StartBGM;//开始场景背景音乐
        //在start方法中获取到相应资源
        void Start()
        {
            StartBGM = transform.Find("StartBGM").GetComponent<AudioSource>();
            BtnClick = transform.Find("BtnClick").GetComponent<AudioSource>();
            soundslider.value = PlayerPrefs.GetFloat("bgsound",0.5F);//如果已设置音量，则按设置值，若未设置则默认为0.5
        }
        
        public void PlayBtnClickBgm()
        {
            BtnClick.Play();//播放按钮点击声
        }
        //音量改变时调用，将改变后的音量值赋给全局音量变量，并保存改变后的音量值到本地
        //同时改变当前场景的所有音源的音量
        public void sliderChange()
        {
            GlobalValue.f_soundvalue = soundslider.value;
            PlayerPrefs.SetFloat("bgsound", GlobalValue.f_soundvalue);
            StartBGM.volume = GlobalValue.f_soundvalue;
            BtnClick.volume = GlobalValue.f_soundvalue;
        }
    }
}

