using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Midi;

namespace MidiPlayer {
    public struct NoteTimeOut {
        public TimeSpan End;
        public Midi.Pitch Pitch;
        public Midi.Channel Channel;
        public int Velocity;
    }

    public class MidiDevice {
#if UNITY_STANDALONE
        private Midi.OutputDevice outputDevice;
#elif UNITY_ANDROID
        private AndroidMidiDevice outputDevice;
#elif UNITY_EDITOR

#endif
        private List<NoteTimeOut> timeOuts;
        private bool muted;

        public MidiDevice() {
            timeOuts = new List<NoteTimeOut>();
#if UNITY_STANDALONE
            outputDevice = Midi.OutputDevice.InstalledDevices[0];
            
#elif UNITY_ANDROID
            outputDevice = new AndroidMidiDevice();
#endif
            outputDevice.Open();
            outputDevice.SilenceAllNotes();
            System.Threading.Thread.Sleep(200); // fixes delay during initial playing, possibly due to midi device initialization
        }

        public void SetInstrument(Instrument instrument) {
            foreach (var c in Enum.GetValues(typeof(Midi.Channel))) {
                outputDevice.SendProgramChange((Midi.Channel)c, instrument);
            }
        }

        public void Close() {
            outputDevice.Close();
            System.Threading.Thread.Sleep(200); // fixes delay during initial playing, possibly due to midi device initialization
        }

        public void StopNotes() {
            timeOuts = new List<NoteTimeOut>();
            outputDevice.SilenceAllNotes();
        }

        protected void NoteOn(Channel channel, Pitch pitch, int velocity) {
            if (muted)
                return;
            outputDevice.SendNoteOn(channel, pitch, velocity);
        }

        public void PlayNote(int track, TextPlayer.Note note, TimeSpan end) {
            PlayNote(
                TrackToChannel(track),
                NoteToPitch(note),
                NoteToVelocity(note),
                end);
        }

        public void PlayNote(Channel channel, Pitch pitch, int velocity, TimeSpan end) {
            NoteOn(channel, pitch, velocity);

            var timeOut = new NoteTimeOut() {
                Channel = channel,
                End = end - TimeSpan.FromMilliseconds(10), // subtract 10 ms to prevent errors with turning off notes
                Pitch = pitch,
                Velocity = velocity
            };
            timeOuts.Add(timeOut);
        }

        private Channel TrackToChannel(int track) {
            if (track >= 10) // skip percussion track
                track++;
            return (Midi.Channel)Enum.Parse(typeof(Midi.Channel), "Channel" + (track + 1), false);
        }

        private Pitch NoteToPitch(TextPlayer.Note note) {
            string type = note.Type.ToString().ToUpperInvariant();
            type += note.Sharp ? "Sharp" : "";
            type += note.Octave;
            return (Midi.Pitch)Enum.Parse(typeof(Midi.Pitch), type);
        }

        private int NoteToVelocity(TextPlayer.Note note) {
            return (int)(note.Volume * 127);
        }

        private int ChannelToIndex(Channel channel) {
            string s = channel.ToString();
            s = s.Replace("Channel", "");
            int index = Convert.ToInt32(s) - 1;
            if (index >= 11) // we skip channel 10 so subtract one to get the track number
                index--;
            return index;
        }

        public void HandleTimeOuts(TimeSpan elapsed) {
            for (int i = timeOuts.Count - 1; i >= 0; i--) {
                var timeOut = timeOuts[i];
                if (elapsed >= timeOut.End) {
                    outputDevice.SendNoteOff(timeOut.Channel, timeOut.Pitch, timeOut.Velocity);
                    timeOuts.RemoveAt(i);
                }
            }
        }

        public List<NoteTimeOut> TimeOuts { get { return timeOuts; } }
        public bool Muted { get { return muted; } set { muted = value; } }
    }
}