Before session, pre-load the following

__ Demo1 source
__ Magnifier 
__ This TXT file 
__ EventLog 
__ Services control panel 
__ Empty Visual Studio 2019 16.2 Preview 
__ Increase system fonts

-----------------------------------
Demo 1
-----------------------------------

Replace this

With this 

<BlazorGrid Items="@forecasts" PageSize="2">
        <GridHeader>
            <th>Date</th>
            <th>Temp. (C)</th>
            <th>Temp. (F)</th>
            <th>Summary</th>
        </GridHeader>
        <GridRow>
            <td>@context.Date.ToShortDateString()</td>
            <td>@context.TemperatureC</td>
            <td>@context.TemperatureF</td>
            <td>@context.Summary</td>
        </GridRow>
    </BlazorGrid>

-----------------------------------
Demo 3 - Full Stack 
-----------------------------------
First change directory to worker process

cd c:\Code\fwdnug\InsiderDevTourDemos19\Sessions\aspNet\03-Orders\01-Start\BlazorInsider.Worker

Then, Complete build 
    dotnet build
    ```
Then let's publish the project in a folder in C:\:

    ```powershell
    dotnet publish -o C:\OrdersService
    ```
Now let's create a service using the output of the build. Copy and paste the following command:

    ```powershell
    sc create worker1 binPath=C:\OrdersService\BlazorInsider.Worker.exe
    ```
    
Once the service has been installed, run it with the following command:

    ```powershell
    sc start worker1

To clean up

    sc stop worker1
    sc delete worker1