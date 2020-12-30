#!/bin/bash
SYSTEM_SQLITE_VERSION="1.0.108.0"

set -e
shopt -s nocasematch

echo "Installing development tools"
dotnet tool restore 

if test "$OS" = "Windows_NT"
then
  echo "Please install System.Data.SQLite.dll." 
else
  echo "Installing libSQLite.Interop.so"
  BUILDIR=$(mktemp -d)
  CWD=$(pwd)
  cd $BUILDIR
  ARCHIVE=sqlite-netFx-source-$SYSTEM_SQLITE_VERSION.zip
  echo "  Downloading $ARCHIVE"
  curl -s -o $ARCHIVE https://system.data.sqlite.org/blobs/$SYSTEM_SQLITE_VERSION/$ARCHIVE > /dev/null
  echo "  Extracting $ARCHIVE"
  unzip -q $ARCHIVE > /dev/null
  echo "  Compiling libSQLite.Interop.dll.so"
  bash Setup/compile-interop-assembly-release.sh > /dev/null
  echo "  Installing libSQLite.Interop.so"
  mkdir -p $CWD/lib/native/
  cp bin/2013/Release/bin/libSQLite.Interop.so  $CWD/lib/native/libSQLite.Interop.dll.so
  cd $CWD
  rm -rf $BUILDIR
fi


echo "Creating empty database for type provider."
rm -f src/resources/scholar-kpi-schema.sqlite
sqlite3 src/resources/scholar-kpi-schema.sqlite < src/resources/scholar-kpi-schema.sql


echo "Installation complete, you can compile scholar-kpi by executing:"
echo "dotnet fake build"
exit 0
