#!/bin/bash
if ! git checkout development; then
	echo "Could not switch branch"
	exit -1
fi
if ! git pull; then
	echo "Could not pull"
	exit -1
fi
echo "Stopping containers"
docker container stop backend
docker container rm backend
docker container stop frontend
docker container rm frontend
docker rmi project-d_backend
docker rmi project-d_frontend
echo "Rebuilding"
docker-compose up --build -d
