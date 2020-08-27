var
  shell = require('shelljs'),
  path = require('path')

const { execSync } = require('child_process')

//clean build
shell.rm('-rf', path.resolve(__dirname, 'dist/*'))
shell.rm('-rf', path.resolve(__dirname, 'dist/.*'))

shell.rm('-rf', path.resolve(__dirname, '../HCAM.API/wwwroot/*'))
shell.rm('-rf', path.resolve(__dirname, '../HCAM.API/wwwroot/.*'))

shell.rm('-rf', path.resolve(__dirname, '../HCAM.API/bin/*'))
shell.rm('-rf', path.resolve(__dirname, '../HCAM.API/bin/.*'))

console.log('Cleaned build artifacts.\n')

//build quasar
shell.exec("quasar build --clean")

//Copy quasar build to .net core, and build 
shell.cp('-rf', 'dist/spa-mat/.', '../HCAM.API/wwwroot')
shell.exec('dotnet publish ./HCAM.API')

//Delete app settings
var dotNetBuildPath = '../HCAM.API/bin/Debug/netcoreapp2.0/publish/'

shell.rm('-f', path.join(dotNetBuildPath, 'appsettings.Development.json'))
shell.rm('-f', path.join(dotNetBuildPath, 'appsettings.Test.json'))
shell.rm('-f', path.join(dotNetBuildPath, 'appsettings.json'))

console.log("Build complete")
