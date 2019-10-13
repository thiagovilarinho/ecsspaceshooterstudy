using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SimpleSpace.UI
{
    public class UILabelListener : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _label = null;

        public TextMeshProUGUI Label { get { return _label; } }

        // Start is called before the first frame update
        private void Awake()
        {
            if (_label == null)
            {
                GetLabel();
            }
        }

        [ContextMenu("Get Label")]
        private void GetLabel()
        {
            _label = GetComponent<TextMeshProUGUI>();
        }

    }
}

