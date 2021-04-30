using UnityEngine;
using UnityEngine.UI;

namespace MMLPlayer
{
    public class InstrumentBtn : MonoBehaviour
    {
        
        public int Instrument { get; private set; }
        public Text Text;

        public void Init(int instrument)
        {
            Instrument = instrument;
            Text.text = InstrumentSelectWnd.GetInstrumentName(instrument);
        }
    }
}