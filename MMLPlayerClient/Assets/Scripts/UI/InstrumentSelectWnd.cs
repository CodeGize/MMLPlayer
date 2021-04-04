using System;
using Midi;
using UnityEngine;
namespace MMLPlayer
{
    public class InstrumentSelectWnd : MonoBehaviour
    {
        public GameObject ItemPrefab;

        void Start()
        {
            var instruments = Enum.GetValues(typeof(Instrument));
            foreach (var instrument in instruments)
            {
                var itemgo = Instantiate(ItemPrefab, ItemPrefab.transform.parent);
                itemgo.SetActive(true);
                var btn = itemgo.GetComponent<InstrumentBtn>();
                btn.Init((int)instrument);
            }
        }


        public void OnItemClick(InstrumentBtn go)
        {
            gameObject.SetActive(false);
            m_callback?.Invoke(go.Instrument);
        }

        private Action<int> m_callback;
        internal void Show(Action<int> callback)
        {
            m_callback = callback;
            gameObject.SetActive(true);
        }
    }
}