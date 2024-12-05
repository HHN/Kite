// TextToSpeechPlugin.h
#import <Foundation/Foundation.h>

#ifdef __cplusplus
extern "C" {
#endif

void _InitializeTTS();
void _Speak(const char* message);
void _StopSpeaking();
bool _IsSpeaking();

#ifdef __cplusplus
}
#endif
