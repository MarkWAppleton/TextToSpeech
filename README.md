# TextToSpeech
OpenSource TextToSpeech project now using the Windows 10 API.

If you find the app good then consider supporting me with the links in the Sponsor section  

# Requirments
* You'll need Windows 10 Build 19041 
* [.Net5 Runtime "Run desktop apps"](https://dotnet.microsoft.com/download/dotnet/5.0/runtime) 

# How to use
## Modes
The program currently has two modes
* Standard Speech - Simpley highlight some text and press the Speak hotkey. This works on most programs and works by sending a copy command (control + c) to the active window.(warning this replaces the text on your clipboard)
* Instant Screenshot - Press the Instant Screenshot hotkey and drag a box over the text you want to speak. Currently only works on the main monitor.
The hotkeys for these modes can be customised in the settings window found in the notification tray, right click on the TextToSpeech icon and select Settings, along with options for changing voice, rate and volume.

## General
* Pressing the Speak hotkey while the program is speaking will stop the current speech.
* While using instant screenshot you can reset the box position by pressing escape once, pressing escape while not dragging the box will exit the instant screenhot mode.
* Currently instant screenshot only works dragging from top left to bottom right.
* There are two supported speech engines, if one doesn't work for you then try the other. Legacy sounds better to me but the windows 10 one is newer.
* Hotkeys cannot be in use by other programs.

If you find any issues open a ticket on github.
