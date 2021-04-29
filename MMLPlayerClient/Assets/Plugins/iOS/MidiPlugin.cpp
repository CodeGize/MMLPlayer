#import "MidiWrapper.h"

#if defined(__cplusplus)
extern "C"
#endif
{
	void OpenDevice()
	{
		[MidiWrapper OpenDevice];
	}
	
	void CloseDevice()
	{
		[MidiWrapper CloseDevice];
	}
	
	void SendMsg(int cmd)
	{
		[MidiWrapper SendMsg:cmd]
	}
}