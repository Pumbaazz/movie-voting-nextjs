#!/bin/bash
# build image
docker build -t nguyntunphng/movie-voting-combine .

# run image
docker run -p 3000:3000 -p 5005:5005 nguyntunphng/movie-voting-combine