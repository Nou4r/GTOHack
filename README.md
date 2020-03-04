<!DOCTYPE html []>
<html>
  <head>
    <meta charset="UTF-8" />
    <meta name="author" content="MarkdownViewer++" />
    <title>README.md</title>
    <style type="text/css">
            

td, h1, h2, h3, h4, h5, p, ul, ol, li {
    page-break-inside: avoid; 
}

        </style>
  </head>
  <body>
    <p>Cheat/mod menu for the game GTFO
Compile, drop the files in the GTFO_DATA/MANAGED folder and inject using any mono injector as follows:</p>
    <p>Namespace: gtfohack
class name: Loader
Method name: Load</p>
    <p>I recommend this injector:
<a href="https://gitlab.com/CainPwnzer/SharpMonoInjector">https://gitlab.com/CainPwnzer/SharpMonoInjector</a>
"smi.exe inject -p GTFO -a gtfohack.dll -n gtfohack -c Loader -m Load"</p>
    <p>visit <a href="https://gitlab.com/CainPwnzer/gtfohack/-/releases">https://gitlab.com/CainPwnzer/gtfohack/-/releases</a> for releases.
This is the first time I've messed with unity engine, It was fun.
All the functions work online as host. about 70% work as non-host.
Don't be a jerk and mess up other people's game by using this when it's unwanted.
Have fun.</p>
  </body>
</html>
