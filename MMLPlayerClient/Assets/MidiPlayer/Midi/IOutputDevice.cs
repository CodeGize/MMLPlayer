namespace Midi
{
    public interface IOutputDevice
    {
        void SendNoteOn(Channel channel, Pitch pitch, int velocity);
        void SendNoteOff(Channel channel, Pitch pitch, int velocity);
        void SendControlChange(Channel channel, Control control, int value);
        void SendPitchBend(Channel channel, int value);
        void SendProgramChange(Channel channel, Instrument instrument);
        void SendSysEx(byte[] data);
    }
}
