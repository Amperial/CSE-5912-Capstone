#! /bin/sh

project="SGoopas"

ls -a $(pwd)/$project

#
# This is a hack!
# For some reason the 'projectPath' var does not get set the first time you open Unity.
# The workaround is to open Unity and start a new project in the desired path.
#
echo "Opening Unity for first time"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile "$(pwd)/$project/unity-first-open.log" \
  -createProject "$(pwd)/$project" \
  -quit

echo "Attempting to build $project for Windows"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile "$(pwd)/$project/unity-windows.log" \
  -buildWindowsPlayer "$(pwd)/$project/Build/windows/$project.exe" \
  -quit

echo "Windows build:"
cat $(pwd)/$project/unity-windows.log

zip -r $(pwd)/$project/Build/windows.zip $(pwd)/$project/Build/windows/

echo "Attempting to build $project for OS X"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile "$(pwd)/$project/unity-mac.log" \
  -buildOSXUniversalPlayer "$(pwd)/$project/Build/osx/$project.app" \
  -quit

echo "OSX build:"
cat $(pwd)/$project/unity-mac.log

zip -r $(pwd)/$project/Build/mac.zip $(pwd)/$project/Build/osx/

echo "Attempting to build $project for Linux"
/Applications/Unity/Unity.app/Contents/MacOS/Unity \
  -batchmode \
  -nographics \
  -silent-crashes \
  -logFile "$(pwd)/$project/unity-linux.log" \
  -buildLinuxUniversalPlayer "$(pwd)/$project/Build/linux/$project.exe" \
  -quit

echo "Linux build:"
cat $(pwd)/$project/unity-linux.log

zip -r $(pwd)/$project/Build/linux.zip $(pwd)/$project/Build/linux/

echo "Log from Unity Open:"
cat $(pwd)/$project/unity-first-open.log
