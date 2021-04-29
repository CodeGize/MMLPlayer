#if UNITY_ANDROID
using Midi;
using UnityEngine;

namespace MidiPlayer
{
    public class AndroidMidiDevice : DeviceBase, IOutputDevice
    {
        public AndroidMidiDevice()
            : base("AndroidOutputDevice")
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            m_jc = new AndroidJavaClass("com.codegize.midihelper.MidiHelper");
        }

        private AndroidJavaClass m_jc;

        public void Open()
        {
            Debug.Log("OpenDevice");
            m_jc.CallStatic("OpenDevice");
        }

        public void Close()
        {
            Debug.Log("CloseDevice");
            m_jc.CallStatic("CloseDevice");
        }

        public void SendNoteOn(Channel channel, Pitch pitch, int velocity)
        {
            var cmd = (int)ShortMsg.EncodeNoteOn(channel, pitch, velocity);
            Debug.LogFormat("SendNoteOn:{0},{1},{2}={3}", channel, pitch, velocity, cmd);
            m_jc.CallStatic("SendMsg", cmd);
        }

        public void SendNoteOff(Channel channel, Pitch pitch, int velocity)
        {
            var cmd = (int)ShortMsg.EncodeNoteOff(channel, pitch, velocity);
            Debug.LogFormat("EncodeNoteOff:{0},{1},{2}={3}", channel, pitch, velocity, cmd);
            m_jc.CallStatic("SendMsg", cmd);
        }

        public void SendControlChange(Channel channel, Control control, int value)
        {

        }

        public void SendPitchBend(Channel channel, int value)
        {

        }

        public void SendProgramChange(Channel channel, Instrument instrument)
        {
            var cmd = (int)ShortMsg.EncodeProgramChange(channel, instrument);
            Debug.LogFormat("EncodeNoteOff:{0},{1}={2}", channel, instrument, cmd);

            m_jc.CallStatic("SendMsg", cmd);
        }

        public void SilenceAllNotes()
        {

        }

        public void SendSysEx(byte[] data)
        {

        }
    }
}
#endif