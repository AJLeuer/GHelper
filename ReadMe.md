(For the time being, this ReadMe is being used only to store notes and
observations about things discovered in the process of attempting to 
reverse-engineer GHub's behavior)

* Applications in GHub always have a default profile which can't be deleted, even if it's renamed to something other than "Default".
* How does GHub know not to delete an application's default profile, even after it's been renamed? One thing I've observed is that a default profile always seems to have the same GUID as its application. So right now my best guess is that when GHub detects that a profile has the same GUID as an existing application - even if that profile isn't active for its application or has been renamed - it disallows deleting it.