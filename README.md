# RF.CrownsOnSongSelect
 A Rhythm Festival mod that adjusts how the crowns are displayed on the song select menu.\
 You can set which difficulties will be displayed in the config file. 
 
  <a href="https://shorturl.at/9J9Dy"> <img src="Resources/InstallButton.png" alt="One-click Install using the Taiko Mod Manager" width="256"/> </a>\
  Easy, Normal, and Hard enabled\
 <img src="Resources/EasyNormalHard50.png" width="500">\
 Oni and Ura enabled\
 <img src="Resources/OniUra50.png" width="500">

# Requirements
 Visual Studio 2022 or newer\
 Taiko no Tatsujin: Rhythm Festival
 

# Build
 Install [BepInEx 6.0.0-pre.2](https://github.com/BepInEx/BepInEx/releases/tag/v6.0.0-pre.2) into your Rhythm Festival directory and launch the game.\
 This will generate all the dummy dlls in the interop folder that will be used as references.\
 Make sure you install the Unity.IL2CPP-win-x64 version.\
 Newer versions of BepInEx could have breaking API changes until the first stable v6 release, so those are not recommended at this time.
 
 Attempt to build the project, or copy the .csproj.user file from the Resources file to the same directory as the .csproj file.\
 Edit the .csproj.user file and place your Rhythm Festival file location in the "GameDir" variable.\
 Download or build the [SaveProfileManager](https://github.com/Deathbloodjr/RF.SaveProfileManager) mod, and place that dll full path in SaveProfileManagerPath.

Add BepInEx as a nuget package source (https://nuget.bepinex.dev/v3/index.json)


# Links 
 [My Other Rhythm Festival Mods](https://docs.google.com/spreadsheets/d/1xY_WANKpkE-bKQwPG4UApcrJUG5trrNrbycJQSOia0c)\
 [My Taiko Discord Server](https://discord.gg/6Bjf2xP)
 