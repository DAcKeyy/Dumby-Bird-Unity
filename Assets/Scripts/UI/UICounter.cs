using System;
using TMPro;
using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(TMP_Text))]
    public class UICounter : MonoBehaviour
    {
        public bool _isCounter;
    
        private TMP_Text _tmpText;
        private int _count = 0;

        private void OnEnable() => _tmpText = GetComponent<TMP_Text>();

        public int? GetCountValue
        {
            get 
            { 
                if(_isCounter)
                {
                    return _count;
                }
                else
                {
                    return null;
                }
            }
        }
    
        public void Increment()
        {
            _tmpText.text = _count++.ToString();
        }
    
        public void Decrement()
        {
            _tmpText.text = _count--.ToString();
        }
    
        public void SetText(string value)
        {
            //TODO Абстрактный TextElement и наследоваться от него 
            _tmpText.text = value.ToString();
            if (_isCounter) _count = Convert.ToInt32(_tmpText.text);
        }

        public void SetNumber(float value)
        {
            _tmpText.text = value.ToString();
            if (_isCounter) _count = Convert.ToInt32(_tmpText.text);
        }
    }
}
