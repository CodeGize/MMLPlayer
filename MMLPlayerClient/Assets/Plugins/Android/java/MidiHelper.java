package com.codegize.midihelper;


import android.media.midi.*;
import android.util.Log;
import java.io.IOException;

import org.billthefarmer.mididriver.MidiDriver;

public class MidiHelper {

    private static String TAG = "MidiHelper";

    //private static MidiManager mMidiManager;
    private static MidiDriver midiDriver;

    //public static void Init(MidiManager mgr) {
    //    mMidiManager = mgr;
    //    if (mMidiManager == null)
    //        Log.e(TAG, "Init faild: Context.MIDI_SERVICE");
    //    else
    //    {
    //        Log.i(TAG, "Init: success");
    //        midiDriver = new MidiDriver();
    //   }
    //}

    public static void OpenDevice()
    {
		if(midiDriver== null)
		{
			midiDriver = new MidiDriver();
		}
        midiDriver.start();
        // Get the configuration.
        int[] config = midiDriver.config();

        Log.i(TAG, "OpenDevice:");
        // Print out the details.
        Log.d(TAG, "maxVoices: " + config[0]);
        Log.d(TAG, "numChannels: " + config[1]);
        Log.d(TAG, "sampleRate: " + config[2]);
        Log.d(TAG, "mixBufferSize: " + config[3]);
    }


    public static void CloseDevice()
    {
        Log.i(TAG, "CloseDevice:");
        midiDriver.stop();
    }

    public static void SendMsg(int cmd) throws IOException {
        Log.i(TAG, "SendMsg:" + cmd);
        byte[] keyMsgBuff = intToByteArray(cmd);
        midiDriver.write(keyMsgBuff);
    }

    public static byte[] intToByteArray(int a) {
        return new byte[]{
			(byte) (a & 0xFF),
			(byte) ((a >> 8) & 0xFF),
			(byte) ((a >> 16) & 0xFF),
            (byte) ((a >> 24) & 0xFF)  
        };
    }
}