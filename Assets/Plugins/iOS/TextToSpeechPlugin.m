// TextToSpeechPlugin.m
#import "TextToSpeechPlugin.h"
#import <AVFoundation/AVFoundation.h>

static AVSpeechSynthesizer *synthesizer = nil;

void _InitializeTTS() {
    if (synthesizer == nil) {
        synthesizer = [[AVSpeechSynthesizer alloc] init];
    }
}

void _Speak(const char* message) {
    if (synthesizer == nil) {
        _InitializeTTS();
    }
    NSString *nsMessage = [NSString stringWithUTF8String:message];
    AVSpeechUtterance *utterance = [AVSpeechUtterance speechUtteranceWithString:nsMessage];
    utterance.voice = [AVSpeechSynthesisVoice voiceWithLanguage:@"de-DE"];
    [synthesizer speakUtterance:utterance];
}

void _StopSpeaking() {
    if (synthesizer != nil) {
        [synthesizer stopSpeakingAtBoundary:AVSpeechBoundaryImmediate];
    }
}

bool _IsSpeaking() {
    if (synthesizer != nil) {
        return [synthesizer isSpeaking];
    }
    return false;
}
