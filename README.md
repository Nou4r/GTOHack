# GTFOhack
Cheat/mod menu for the game GTFO

## Compilation
Add the dll's in the GTFO_DATA/MANAGED folder as references in the project. Compile and enjoy.
## Usage

Precompiled dll here: https://gitlab.com/CainPwnzer/gtfohack/-/releases
drop the file in the GTFO_DATA/MANAGED folder and inject using any mono injector as follows:

- Namespace: gtfohack
- class name: Loader
- Method name: Load

I recommend this injector:
https://gitlab.com/CainPwnzer/SharpMonoInjector
With the following command:
"smi.exe inject -p GTFO -a gtfohack.dll -n gtfohack -c Loader -m Load"


This is the first time I've messed with unity engine, It was fun.
All the functions work online as host. about 70% work as non-host. 
Don't be a jerk and mess up other people's game by using this when it's unwanted.
Have fun.