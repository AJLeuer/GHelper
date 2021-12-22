##### Overview

This project is an attempt to create a simple desktop app - with the working name of _GHelper_ -
that can fill in some of the missing functionality of Logitech's G Hub application.

Some of the things that G Hub cannot currently do, but that I hope for G Helper to be able to do, include but
are not limited to:
* Deleting an application profile
* Renaming a profile
* Setting custom application and profile images
* Providing a more clear and obvious way for the user to see which profile is currently active for a given application
* Allowing the user to see (and possibly edit) application and profile metadata that is stored by G Hub behind the scenes but not exposed through G Hub's UI

_Disclaimer: the developer of this software is not affiliated with Logitech International S.A._

##### Building & Running

Building the application will require the following:
* A PC running Windows 10, version 1803 or later, or Windows 11
* Visual Studio 2022 or later, configured for Windows App SDK/WinUI 3 development as detailed [here](https://docs.microsoft.com/en-us/windows/apps/windows-app-sdk/set-up-your-development-environment?tabs=vs-2022).

When running from Visual Studio, if you don't have Logitech G Hub installed (or you just want to play it safe), set the solution configuration to **Debug**.
This will load a dummy version of G Hub's JSON state file instead of the actual one on your file system.