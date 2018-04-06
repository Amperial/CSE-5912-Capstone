#! /bin/sh

BASE_URL=http://download.unity3d.com/download_unity
HASH=bfcbae508940

download() {
  file=$1
  url="$BASE_URL/$HASH/$package"

  echo "Downloading from $url: "
  curl -o `basename "$package"` "$url"
}

install() {
  package=$1
  download "$package"

  echo "Installing "`basename "$package"`
  sudo installer -dumplog -package `basename "$package"` -target /
}

# See $BASE_URL/$HASH/unity-$VERSION-$PLATFORM.ini for complete list
# of available packages, where PLATFORM is `osx` or `win`

install "MacEditorInstaller/Unity.pkg"
install "MacEditorTargetInstaller/UnitySetup-Windows-Support-for-Editor-2017.3.0p3.pkg"
install "MacEditorTargetInstaller/UnitySetup-Linux-Support-for-Editor-2017.3.0p3.pkg"