Cheat/mod menu for the game GTFO
Compile, drop the files in the GTFO_DATA/MANAGED folder and inject using any mono injector as follows:

Namespace: gtfohack
class name: Loader
Method name: Load

I recommend this injector:
https://gitlab.com/CainPwnzer/SharpMonoInjector

"smi.exe inject -p GTFO -a gtfohack.dll -n gtfohack -c Loader -m Load"

visit https://gitlab.com/CainPwnzer/gtfohack/-/releases for releases.
This is the first time I've messed with unity engine, It was fun.
All the functions work online as host. about 70% work as non-host. 
Don't be a jerk and mess up other people's game by using this when it's unwanted.
Have fun.