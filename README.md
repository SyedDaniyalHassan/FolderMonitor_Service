# FolderMonitor_Service
Folder Monitor Windows Service


Folder Monitor client is supposed to track changes on a specific folder. You are required to create the
following windows services:
• Your service is supposed to check the specified folder initially every minute for any changes. If there
are any changes, it should copy the new or updated files to another fixed location. If there are no changes
then it should increase the delay by additional 2 minutes until it reaches 1-hour delay. The delay should
not exceed 1-hour gap.
Note: You cannot use the File Watcher class.
