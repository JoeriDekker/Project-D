#!/bin/bash
docker container stop backend
docker container rm backend
docker container stop frontend
docker container rm frontend
docker rmi project-d_backend
docker rmi project-d_frontend
docker-compose up -d
