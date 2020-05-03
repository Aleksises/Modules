# Module 8 (Logging and Monitoring)
## Testing
* For the first run, you should configure connection strings in web.config for your instance;
* For testing **Monitoring Counters** you have to run application as **Administrator**;

### Task 3
As Logging report generator was used **Log Parser**. [Here you can download it](https://www.microsoft.com/en-us/download/details.aspx?id=24659)
All required stuff you can find by the following path ./MvcMusicStore/logs

### Testing Task 3
* Run **Powershell** as **Administrator** and execute in ./logs **GenerateReport.ps1**. If a notification about changing policy appears you should enter **A**;
* If problems appear when running script, you can copy all commands and run directly by **Powershell**
* Open generated **Report.csv** file;
