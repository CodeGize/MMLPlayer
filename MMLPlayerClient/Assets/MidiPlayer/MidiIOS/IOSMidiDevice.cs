#if UNITY_IOS

using System.Runtime.InteropServices;
using Midi;

namespace MidiPlayer
{
    public class IOSMidiDevice : DeviceBase, IOutputDevice
    {
        [DllImport("__Internal")]
        public static extern void OpenDevice();

        [DllImport("__Internal")]
        public static extern void CloseDevice();

        [DllImport("__Internal")]
        public static extern void SendMsg(int cmd);

        void IOutputDevice.Open()
        {
            throw new System.NotImplementedException();
        }

        void IOutputDevice.Close()
        {
            throw new System.NotImplementedException();
        }

        void IOutputDevice.SilenceAllNotes()
        {
            throw new System.NotImplementedException();
        }

        void IOutputDevice.SendNoteOn(Channel channel, Pitch pitch, int velocity)
        {
            throw new System.NotImplementedException();
        }

        void IOutputDevice.SendNoteOff(Channel channel, Pitch pitch, int velocity)
        {
            throw new System.NotImplementedException();
        }

        void IOutputDevice.SendControlChange(Channel channel, Control control, int value)
        {
            throw new System.NotImplementedException();
        }

        void IOutputDevice.SendPitchBend(Channel channel, int value)
        {
            throw new System.NotImplementedException();
        }

        void IOutputDevice.SendProgramChange(Channel channel, Instrument instrument)
        {
            throw new System.NotImplementedException();
        }

        void IOutputDevice.SendSysEx(byte[] data)
        {
            throw new System.NotImplementedException();
        }

        public IOSMidiDevice() : base("IOSOutputDevice")
        {
        }
    }
}
#endif