# TilesDashboard.Backend [![Actions Status](https://github.com/Carq/TilesDashboard.Backend/workflows/.NET%20Core/badge.svg)](https://github.com/Carq/TilesDashboard.Backend/actions)

- WebApi with .NET Core 3.0
- MongoDB.Driver 2
- MongoDB

## Set up MongoDB

- Install MongoDB engine
- Install MongoDB Compass (GUI, something like SSMS)
- Add MongoDB bin to path (`C:\Program Files\MongoDB\Server\4.2\bin`)
- Run DB server (cmd): `mongod --dbpath <data_directory_path>`
- Run Compass and connect to `localhost:27017`
- Create Database `TilesDatabase` with `Tiles`, `Metrics` and `MetricSettings` collections
- Import data to those collections from proper json files in `TilesDashboard.Core\Data` directory
