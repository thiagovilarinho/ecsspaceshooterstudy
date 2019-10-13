using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SimpleSpace.Core;
using System;
using TMPro;

namespace SimpleSpace.UI
{
    public class UIHealthListener : UILabelListener
    {
        // Start is called before the first frame update
        void Start()
        {
            if(GameManager.instanceExists)
            {
                GameManager.instance.UpdateHealth += UpdateHealth;
            }
        }

        private void UpdateHealth(string score)
        {
            Label.text = score;
        }

        private void OnDestroy()
        {
            if(GameManager.instanceExists)
            {
                GameManager.instance.UpdateHealth -= UpdateHealth;
            }
        }
    }
}
