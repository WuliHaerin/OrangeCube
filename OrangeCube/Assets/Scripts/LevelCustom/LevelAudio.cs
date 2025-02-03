using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalCustom;
namespace LevelCustom
{
    public class LevelAudio : MonoBehaviour
    {
        //所有音源
        private AudioSource[] Levelbgm=new AudioSource[5];
        // start方法中获取到所有的音源
        void Start()
        {
            Levelbgm[0] = transform.Find("LevelBGM").GetComponent<AudioSource>();
            Levelbgm[1] = transform.Find("cubeClickbgm").GetComponent<AudioSource>();
            Levelbgm[2] = transform.Find("btnClickbgm").GetComponent<AudioSource>();
            Levelbgm[3] = transform.Find("completedbgm").GetComponent<AudioSource>();
            Levelbgm[4] = transform.Find("failedbgm").GetComponent<AudioSource>();
            //设置音源音量
            for (int temp=0;temp<Levelbgm.Length;temp++)
            {
                Levelbgm[temp].volume = GlobalValue.f_soundvalue;
            }
        }
        //根据索引值播放相应的音乐
        public void Playbgm(int bgmindex)
        {
            Levelbgm[bgmindex].Play();
        }
    }
}

