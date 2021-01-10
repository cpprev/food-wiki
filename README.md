# Web app to display info about food
---

:pushpin: Back-end requirements : C# (net5.0), MongoDB ; Front-end requirements : Angular

:pushpin: The aim of this project is to enhance my fullstack skills and understand how apps are made.

:pushpin: The page design part (HTML/CSS) was not really where my main focus, this is why pages are not necessarily aesthetic.

---

| Requirements      | Use                                                                      | Download |
| ------------------ | ------                                                                   | -------- |
| MongoDB            | Storage for food objects                                                 | https://www.mongodb.com/ |
| Angular CLI        | To deploy/build/launch the portal (Cf "launchPortal.bat")                | https://cli.angular.io/ |
| dotnet CLI         | To build the backend/apis (Cf "launchRestApis.bat")                      | https://dotnet.microsoft.com/download/dotnet-core |
| npm                | To install the Angular modules (npm install in "~/src_front")                | https://nodejs.org/en/ |
| nuget CLI          | To install the C# packages (ie: "nuget.exe install Microsoft.AspNetCore.Mvc -Version 1.0") | https://www.nuget.org/downloads |

---

### How to build & Usage

    To launch the rest apis : ~/launchRestApis.bat
  
    To launch the portal : ~/launchPortal.bat
  
---

### The portal

:pushpin: Main page, with a search bar involving an autocompletion tool directly listing the closest food objects stored in database |  :pushpin: This is the template for each food element stored in database (as I said I did not have my focus on the CSS part for this project)
:-------------------------:|:-------------------------:
![alt text](https://github.com/cpprev/food-wiki/blob/master/images/search_bar.png?raw=true)  |  ![alt text](https://github.com/cpprev/food-wiki/blob/master/images/food_template.png?raw=true)
