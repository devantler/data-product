#!/bin/bash
echo "📦 Publishing Data Product"
cp /app/config.yaml /build/src/Devantler.DataMesh.DataProduct/config.yaml
cd /build/src/Devantler.DataMesh.DataProduct
dotnet publish -c Release --no-restore -o /app/publish

echo "Copy published files to /app"
cp -r /app/publish/* /app

echo "🧹 Cleaning up"
apt-get autoremove -y
rm -rf /usr/share/dotnet
rm -rf /var/lib/apt/lists/*
rm -rf /build
rm -rf /app/publish
rm -rf /tmp/*

echo "⚙️ Install aspnetcore runtime"
wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh
chmod +x ./dotnet-install.sh
./dotnet-install.sh --channel 7.0 --runtime aspnetcore
rm -rf dotnet-install.sh

echo "🚀 Starting Data Product"
cd /
adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
su appuser

cd /app
$HOME/.dotnet/dotnet Devantler.DataMesh.DataProduct.dll
