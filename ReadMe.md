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
* A PC running Windows 10, version 1803 or later
* Visual Studio 2019 Visual Studio 2019, version 16.9 Preview with requisite workloads for WinUI 3 detailed [here](https://docs.microsoft.com/en-us/windows/apps/winui/winui3/).

When running from Visual Studio, if you don't have Logitech G Hub installed (or you just want to play it safe), set the solution configuration to **Debug**.
This will load a dummy version of G Hub's JSON state file instead of the actual one on your file system.

##### Logitech G Hub Notes

(The following section is for notes and
observations about things discovered in the process of attempting to 
reverse-engineer GHub's behavior)

* Applications in GHub always have a default profile which can't be deleted, even if it's renamed to something other than "Default".
* How does GHub know not to delete an application's default profile, even after it's been renamed? One thing I've observed is that a default profile always seems to have the same GUID as its application. So right now my best guess is that when GHub detects that a profile has the same GUID as an existing application - even if that profile isn't active for its application or has been renamed - it disallows deleting it.
* `C:\ProgramData\LGHUB\depots\73248\core\applications` is where most of the image files served from `localhost` appear to be stored.
* `C:\ProgramData\LGHUB\current.json` may hold the list of images available to be served locally. It's possible it'll have to be edited to allow adding new images.
* Alternatively, for custom games with custom poster images G HUB also seems to take the approach 
  of serializing entire image files into base64 strings and sticking that data into
  its JSON. So we may also be able to try this approach.


##### Todos
* U̶n̶i̶f̶y̶ ̶d̶i̶s̶p̶l̶a̶y̶ ̶o̶f̶ ̶p̶o̶s̶t̶e̶r̶ ̶i̶m̶a̶g̶e̶ ̶l̶o̶c̶a̶t̶i̶o̶n̶ ̶f̶o̶r̶ ̶b̶o̶t̶h̶ ̶s̶t̶a̶n̶d̶a̶r̶d̶ ̶a̶n̶d̶ ̶c̶u̶s̶t̶o̶m̶ ̶a̶p̶p̶l̶i̶c̶a̶t̶i̶o̶n̶s̶
  - Done
* E̶n̶s̶u̶r̶e̶ ̶o̶n̶l̶y̶ ̶o̶n̶e̶ ̶p̶r̶o̶f̶i̶l̶e̶ ̶o̶f̶ ̶a̶n̶ ̶a̶p̶p̶l̶i̶c̶a̶t̶i̶o̶n̶ ̶i̶s̶ ̶a̶l̶w̶a̶y̶s̶ ̶a̶c̶t̶i̶v̶e̶ ̶w̶h̶e̶n̶ ̶u̶s̶e̶r̶ ̶c̶h̶a̶n̶g̶e̶s̶ ̶"̶A̶c̶t̶i̶v̶e̶"̶ ̶s̶t̶a̶t̶e̶ ̶o̶f̶ ̶a̶ ̶p̶r̶o̶f̶i̶l̶e̶
  - Done
* If user sets all profiles to inactive, set the first one back to active (maybe)
* Implement setting custom poster images
  - Done for custom applications
* D̶e̶f̶a̶u̶l̶t̶P̶r̶o̶f̶i̶l̶e̶s̶ ̶d̶o̶n̶'̶t̶ ̶r̶e̶a̶l̶l̶y̶ ̶w̶o̶r̶k̶ ̶(̶c̶a̶n̶'̶t̶ ̶c̶h̶a̶n̶g̶e̶ ̶t̶h̶e̶i̶r̶ ̶n̶a̶m̶e̶ ̶a̶n̶d̶ ̶a̶c̶t̶u̶a̶l̶l̶y̶ ̶d̶i̶s̶p̶l̶a̶y̶ ̶t̶h̶e̶ ̶r̶e̶s̶u̶l̶t̶)̶
  - Done
* Confirm before deleting
* Delete on delete key
* S̶h̶o̶w̶ ̶A̶p̶p̶l̶i̶c̶a̶t̶i̶o̶n̶ ̶t̶y̶p̶e̶ ̶i̶n̶ ̶r̶e̶c̶o̶r̶d̶ ̶v̶i̶e̶w̶
  - Done