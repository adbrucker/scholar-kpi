# scholar-kpi
A tool for analyzing publication related key performance indicates (KPIs) 
based on the information available at the Google Scholar page of an author. Its 
main purpose is to allow for computing publication metrics not provided by 
Google Scholar as well as to analyze the development of these metrics over 
time.

# Dependencies
Dependencies are managed by paket, with the exception of 
* the SQLite libraries required by the SQLProvider. For Mono, 
  ``configure.sh`` tries to set up everything nicely using a 
  system-wide installed ``Mono.Data.Sqlite.dll``. If this fails, 
  please follow the steps documented at the 
  [SQLProvider website](https://fsprojects.github.io/SQLProvider/core/sqlite.html).
* the ``sqlite3`` binary is used for creating an empty data base used 
  by the SQL type provider

# Installation 
* On Linux/MacOs using mono:
  After cloning the repository, call 
  ```
  ./configure.sh  
  ```
  After that, the project can be build using 
  ```
  ./build.sh
  ```
* On Windows (untested), you need to set up the initial project  
  manually:
  ```
  type src/resources/scholar-kpi-schema.sql | sqlite3 src/resources/scholar-kpi-schema.sqlite
  .paket/paket.bootstrapper.exe
  .paket/paket.exe install
  ````
  After that, the project can be build using 
  ```
  build.cmd
  ```

## Team
* [Achim D. Brucker](https://www.brucker.ch/)

## License
This project is licensed under the GPL 3.0 (or any later version). 
