using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace StardustLib.UI
{
    [RequireComponent(typeof(Image))]
    public class MultiImage : MonoBehaviour
    {
        public Image Image;
        public List<Sprite> StateSprites;

        [SerializeField]
        private bool m_autoSetOnAwake;

        [SerializeField]
        private int m_curIndex = 0;

        public int CurIndex { get { return m_curIndex; } }

        public void Awake()
        {
            if(m_autoSetOnAwake)
            SetState(m_curIndex);
        }

        public void SetState(int index)
        {
            if (index < 0 || index >= StateSprites.Count)
                return;
            m_curIndex = index;
            Image.sprite = StateSprites[index];
        }

        public void Loop(int delta)
        {
            var index = (CurIndex + delta) % StateSprites.Count;
            SetState(index);
        }
    }
}