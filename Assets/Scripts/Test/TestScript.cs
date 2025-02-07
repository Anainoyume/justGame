using System.Collections.Generic;
using UnityEngine;

namespace Test
{
    public class TestScript : MonoBehaviour
    {
        
        public void SaySomething(int a) {
            Debug.Log("Hello!");
        }
        
        public void PlayAnimation(string AnimationName, float playSpeed, bool isRepeat) {
            Debug.Log($"动画{AnimationName}, 正在以 {playSpeed} 的速度播放!");
        }
        
        private void NonParamFunciton() {
            Debug.Log("这是无参方法");
        }
    }
}