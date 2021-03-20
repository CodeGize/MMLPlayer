using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MidiPlayer {
    public interface IMidiPlayer {
        TimeSpan Elapsed { get; }
        void SetInstrument(int instrument);
        bool Normalize { get; set; }
        bool Loop { get; set; }
        bool Playing { get; }
        bool Paused { get; }
        bool Muted { get; set; }
        void CalculateNormalization();
        void Play(TimeSpan currentTime);
        void Update(TimeSpan currentTime);
        void Stop();
        void CloseDevice();
        void Pause();
        void Unpause();
        void Seek(TimeSpan currentTime, TimeSpan position);
        TimeSpan Duration { get; }
    }
}
