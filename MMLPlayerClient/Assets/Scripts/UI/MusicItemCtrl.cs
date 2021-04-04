using UnityEngine;
using UnityEngine.UI;
namespace MMLPlayer
{
    public class MusicItemCtrl : MonoBehaviour
    {
        [SerializeField]
        private Text m_txNum;
        [SerializeField]
        private Text m_txName;
        [SerializeField]
        private Image m_imgPlaying;
        [SerializeField]
        private Image m_imgSelecting;

        [SerializeField]
        private Color m_selectColor = Color.white;

        [SerializeField]
        private Color m_unSelectColor = new Color(140f / 255f, 140f / 255f, 140f / 255f);

        public void Select(bool bf)
        {
            if (m_txNum != null)
            {
                m_txNum.enabled = !bf;
                m_txNum.color = bf ? m_selectColor : m_unSelectColor;
            }
            if (m_imgPlaying != null)
                m_imgPlaying.enabled = bf;

            if (m_imgSelecting != null)
                m_imgSelecting.enabled = bf;

            if (m_txName != null)
                m_txName.color = bf ? m_selectColor : m_unSelectColor;
        }

        internal void Init(int index, string musicName)
        {
            if (m_txNum != null)
                m_txNum.text = index.ToString();
            if (m_txName != null)
                m_txName.text = musicName;
        }
    }
}
