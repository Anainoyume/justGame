using System;
using System.Linq;
using BehaviourTree;
using BehaviourTree.BehaviourTreeSO;
using qjklw.InspectorAttribute;
using UnityEngine;

namespace BehaviourTree.Test
{
    public class TestBT : MonoBehaviour
    {
        /*
         *  由于我们暂时不用 Odin, 因此先用代码实现
         */
        public BehaviourTreeData BTree;

        private void Update() {
            BTree?.Tick();
        }
    }
}