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
  -stackTraceLogType Full \
  -quit

cat $(pwd)/$project/unity-first-open.log

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

zip -r $(pwd)/Build/windows.zip $(pwd)/Build/windows/

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

zip -r $(pwd)/Build/mac.zip $(pwd)/Build/osx/

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

zip -r $(pwd)/Build/linux.zip $(pwd)/Build/linux/
