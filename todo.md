# DBWatcher

## Description
Database maintenance and monitoring system

## Features
1. Create and save scripts
    * Save as simple script
    * save as a procedure
2. Execute scripts
    * result as a table
    * save result to file
3. Dashboards
4. Monitoring

## Details
### Scripts
Script can be created as a simple script or as a procedure.



## Repositories
### Script repository
    1. Get all info by id
    2. Get list of scripts with name and description
    3. Search by name
    4. Search by name and description
    5. Create script
    6. Update script
    
### ConnectionProperty repository
    1. Get all info by id
    2. Get all cp with name and server


## Services
### Connection service
    Responsible for connection management: create connection, use connection through all app
    Cachig
    Need create class that which will Create SQLConnection using SQLCredential, 
        and server name from ConnectionProperties, and database from selected user database
    Password can be saved or not, if it saved then it can be encrypted or not
    1. Create connection

### Database service
    responsible for discovering database structure
    1. Get server databases
    2. Select database as current (??)

### Script service 
    responsible for script management: run script, save to database, 
    scripts will run for defined database selected from server databases list
    Need use scriptExecutorBuilder 
    1. Run script
    2. Install scrip to daabase
    3. Check is script installed by DbObjectName
    
