# Course summary
## Topics
- Positioning OData in relation to REST
- Creating OData services
- Advanced data querying
- Actions, functions, open types, batch processing

## REST
Rest = Representational State Transfer  

REST is an architectural style for designing networked applications, intended to evoke an image of how a well designed application behaves. (Roy Fielding)  
- REST is an architectural style - Bound by a set of constraints  
- REST is NOT a standard - Common design decisions are often reinvented

## OData
OData = Open Data Protocol  

An open protocol to allow the creation and consumption of queriable and interoperable RESTful APIs in a simple and standard way. 
- An OASIS-approved industry standard
    - Developed by Microsoft
    - Supported by Microsoft, IBM, SAP and others
    - Defines best practices for creating and consuming RESTful APIs
- Consistency, standardization
- Solves common design issues (naming resources, querying data)
- Uniform way to describe data and data model
- Machine-readable metadata enables automatic interaction, generating client proxies, tools etc

Interaction between OData services
- no more need to reinvent the wheel (handling paging naming resources etc)
- lower learning curve
- Automation becomes a real possibility

An OData request (eg. `http//localhost:5000/odata/People(2)/VinylRecords?$filter=Title eq 'Nirvana'`) has the following components:
- service root: `http//localhost:5000/odata`
- resource path: `/People(2)/VinylRecords`
    - `People` - People entity set
    - `(2)` - entity key
    - `/VinylRecords` - navigation property
- optional query options: `?$filter=Title eq 'Nirvana'`