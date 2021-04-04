package com.codegize.midihelper;

import android.media.midi.*;
import android.util.Log;
import java.io.IOException;

import org.billthefarmer.mididriver.MidiDriver;

public class MidiHelper {

    private static String TAG = "MidiHelper";

    private static MidiDriver midiDriver;

    public static void OpenDevice()
    {
		if(midiDriver== null)
		{
			midiDriver = new MidiDriver();
		}
        midiDriver.start();
        int[] config = midiDriver.config();
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