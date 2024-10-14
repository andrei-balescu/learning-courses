# SQL Tutorial - Full Database Course for Beginners
## by Mike Dane (Giraffe Academy)
In this course, we'll be looking at database management basics and SQL using the MySQL RDBMS.  
The course is designed for beginners to SQL and database management systems, and will introduce common database management topics.  
Throughout the course we'll be looking at various topics including schema design, basic C.R.U.D operations, aggregation, nested queries, joins, keys and much more.

## Resources
- Video tutorial: https://www.youtube.com/watch?v=HXV3zeQKqGY&t=17s
- Sample Company Database: https://www.giraffeacademy.com/databases/sql/creating-company-database/

## Database Management Systems
DBMS - a special software program that helps users create and maintain a database
- Makes it easy to manage large amounts of information
- Handles security
- Backups
- Importing/exporting data
- Concurrency
- Interacts with software applications

### C.R.U.D.
The main operations for working with a database:
- Create
- Read/Retrieve
- Update
- Delete

### Relational databases (SQL)
Organize data into one or more tables
- Each table has columns and rows
- A unique key identifies each row

Student table
|*ID#   |Name   |Major      |
|--     |--     |--         |
|1      |Jack   |Biology    |
|2      |Kate   |Sociology  |
|3      |Claire |English    |
|4      |John   |Chemistry  |

Users table
|*Username  |Password   |Email  |
|--         |--         |--     |
|jsmith22   |wordpass   |...    |
|catlover45 |apple223   |...    |
|gamerkid   |...        |...    |
|giraffe    |...        |...    |

- Relational Database Management Systems (RDBMS)
    - help users create and maintain a database
        - mySQL, Oracle, postgreSQL, mariaDB, etc
- Structure Query Language (SQL)
    - Standardized language for interacting with RDBMS
    - Used to perform C.R.U.D. operations, as well as other administrative tasks (user management, security, backup, etc.)
    - Used to define tables and structures
    - SQL code used in one RDBMS is not always portable to another without modification.

### Non-relational (noSQL / not just SQL)
Organize data is anything but a traditional table
- Key-value stores
- Documents (JSON, XML etc,)
- Graphs
- Flexible tables

Document (JSON, BLOB, XML etc)
```json
[{
    "_id": 12345,
    "name": "Jack",
    "major": "Biology"
}, {
    "_id": 2267,
    "name": "Kate",
    "major": "Sociology"
}, {
    "_id": 2453,
    "name": "Claire",
    "major": "English"
}, {
    "_id": 1957,
    "name": "John",
    "major": "Chemistry"
}]
```

Graph (relational nodes)
![noSQL - Graph](nosql_graph.svg)

Key-value Hash (Keys are mapped to values - strings, json, blob etc)
|Key    |Value  |
|--     |--     |
|"xyz"  |string |
|"abc"  |JSON   |
|"pqr   |BLOB   |
|"lmno" |etc... |

- Non-relational Database Management Systems (NRDBMS)
    - Help users create and maintain a non-relational database
        - mongoDB, dynamoDB, apache cassandra, firebase, etc.
- Implementation specific
    - Any non-relational database falls under this category, so there's no set language standard.
    - Most NRDBMS will implement their own language for performing C.R.U.D. and administrative operations on the database.

### Database Queries
Queries are requests made to the database management system for specific information.  
As the database's structure become more and more complex, it becomes more difficult to get the specific pieces of information we want.  
A google search is a query.  