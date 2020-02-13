# MetricsDashboard.Backend [![Actions Status](https://github.com/Carq/MetricsDashboard.Backend/workflows/.NET%20Core/badge.svg)](https://github.com/Carq/MetricsDashboard.Backend/actions)

* WebApi with .NET Core 3.0
* MongoDB.Driver 2
* MongoDB

## Set up MongoDB
* Install MongoDB engine
* Install MongoDB Compass (GUI, something like SSMS)
* Add MongoDB bin to path (`C:\Program Files\MongoDB\Server\4.2\bin`)
* Run DB server (cmd): `mongod --dbpath <data_directory_path>`
* Run Compass and connect to `localhost:27017`
* Create Database `MetricsDatabase` with `Metrics` collection
* Add some documents to `Metrics` collection, e.g. 
`{
    "metricKind": "percentage",
    "metricName": "Humidity",
    "value": 33.1,
    "addedOn": "2020-02-12T14:20:00Z"
}`