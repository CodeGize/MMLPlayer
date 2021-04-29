
@interface MidiWrapper : NSObject

+ (void) OpenDevice;

+ (void) CloseDevice;

+ (void) SendMsg:(int)cmd;