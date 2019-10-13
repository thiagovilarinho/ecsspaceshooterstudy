using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SimpleSpace.UI
{
    [RequireComponent(typeof(Animator),typeof(Canvas))]
    public class UIWindow : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator = null;

        [SerializeField]
        private Canvas _canvas = null;

        private int _openTrigger = Animator.StringToHash("Open");
        private int _closeTrigger = Animator.StringToHash("Close");

        // Start is called before the first frame update
        void Start()
        {
            if(_animator == null)
            {
                GetComponents();
            }
        }

        [ContextMenu("Get Components")]
        private void GetComponents()
        {
            _animator = GetComponent<Animator>();
            _canvas = GetComponent<Canvas>();
        }

        public void Open()
        {
            SetCanvasState(1);
            _animator.SetTrigger(_openTrigger);
        }

        public void Close()
        {
            _animator.SetTrigger(_closeTrigger);
        }

        public void SetCanvasState(int state)
        {
            if(state == 0)
            {
                _animator.enabled = false;
                _canvas.enabled = false;
            }else
            {
                _canvas.enabled = true;
                _animator.enabled = true;
            }
        }
    }
}
