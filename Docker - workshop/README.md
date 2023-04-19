# Workshop – Docker
### Duration: 3 days
### Level: Medium
### Language: Lithuanian + English
### Speaker: Valentinas Kaminskas


# Day 1

## Preparation
* Install docker desktop

## Workshop notes
* If you want to debug output add --progress=plain
* docker build -f .\Dockerfile-echo --progress=plain .
* docker run -d --name demo-4 --rm -p 5000:80 demo-4
* docker image ls
* docker system prune --all

| Keyword    |      Description                                                                                 | 
|------------|:------------------------------------------------------------------------------------------------:|
| FROM       |Sets the base image for subsequent                                                                |
| MAINTAINER |Sets the author field of the generated images                                                     |
| RUN        |Execute commands in a new layer on top of the current image and commit the results                |
| CMD        |Allowed only once (if many then last one takes effect)                                            |
| LABEL      |Adds metadata to an image                                                                         |
| EXPOSE     |Informs container runtime that the container listens on the specified network ports at runtime    |
| ENV        |Sets an environment variable                                                                      |
| ADD        |Copy new files, directories, or remote file URLs from into the filesystem of the container        |
| COPY       |Copy new files or directories into the filesystem of the container                                |
| ENTRYPOINT |Allows you to configure a container that will run as an executable                                |
| USER       |Sets the username or UID to use when running the image                                            |
| WORKDIR    |Sets the working directory for any RUN, CMD, ENTRYPOINT, COPY, and ADD commands                   |
| ARG        |Defines a variable that users can pass at build-time to the builder using --build-arg             |

## Post exercises
* Pull docker images such as Redis or RabbitMq and run and access this instance locally

### Readings:
* https://docs.docker.com/engine/reference/commandline/build/
* https://docs.docker.com/engine/reference/commandline/run/


# Day 2

## Preparation
* Install docker desktop

## Workshop notes
* Talked about docker file basic commands. The video has not been recorded

### Entrypoint vs Cmd

```
# shell form
# When instruction is executed in shell form it calls /bin/sh -c
<command>
# under the hood and normal shell processing happens.
# <instruction> <command>
```
```
# exec form
# When instruction is executed in exec form it calls executable
directly,
# and shell processing does not happen
# <instruction> ["executable", "param1", "param2", ...]
```
```
FROM debian:sid-slim
RUN echo "Hello world"
CMD ["/bin/echo", "Hello world"]
```
```
FROM debian:sid-slim
RUN echo "Hello world"
ENTRYPOINT ["/bin/echo", "Hello world"]
```
CMD sets DEFAULT command and/or parameters, which can be overwritten from command line when docker container runs.
ENTRYPOINT configures a container that will run as an executable.

We can use them both
```
FROM debian:sid-slim
ENTRYPOINT ["/bin/echo", "Hello"]
CMD ["world"]
```

```
FROM debian:sid-slim
# environment variable. Set there or change on docker RUN
ENV mood happy

# parametras panaudojamas, nes naudojame bash,
# taciau tokiu stiliumi NEGALIMA nieko paduot is vartotojo

# pvz: docker run demo-3 "Valentinas" -> I feel really happy today!
ENTRYPOINT echo "I feel really $mood today!"

# pvz: docker run demo-3 "Valentinas" -> I feel really $mood today!
Valentinas
ENTRYPOINT ["/bin/echo", "I feel really $mood today!"]

# pvz: docker run demo-3 "Valentinas" -> I feel really happy today!
ENTRYPOINT ["/bin/bash", "-c", "echo I feel really $mood today!"]

# pvz: docker run demo-3 "Valentinas" -> I feel really $mood today!
Valentinas
ENTRYPOINT ["/bin/bash", "-c", "echo I feel really $mood today!"]
CMD ["word"]
# Naudojame juos kartu
# veikia ir su ir be parametro. kodel? $0 priima parametra.
# kas butu jeigu CMD nebutu arba nepaduociau parametro kviesdamas?
# shell form
ENTRYPOINT /bin/echo "I feel really $mood today! $0"
CMD [""]

# exec form
ENTRYPOINT ["/bin/sh", "-c", "/bin/echo \"I feel really $mood today!
$0\""]
CMD [""]
Can I replace env variable value?
docker run -e mood="bad" demo "Valentinas"
# set env variable on docker BUILD you need to use ARG: --build-arg
# you could give this a default value as well
ARG mood_arg=happy
ENV mood=$mood_arg

ENTRYPOINT ["/bin/sh", "-c", "/bin/echo \"I feel really $mood today!
$0\""]
CMD [""]
```
## Post exercises
### Readings
* https://goinbigdata.com/docker-run-vs-cmd-vs-entrypoint/
* https://docs.docker.com/language/python/build-images/



# Day 3

## Preparation
* Install docker desktop

## Workshop notes
If you want to run a python flask application you can use python:3.10 and python:3.10-slim-buster. The slim version will be size
optimized so if you have everything you need in your requirements, use the slim version instead.

```
# FROM python:3.10-slim-buster
FROM python:3.10

WORKDIR /app

COPY requirements.txt requirements.txt
RUN pip install -r requirements.txt

COPY . .

CMD [ "python", "-m" , "flask", "run", "--host=0.0.0.0"]
```

## Post exercises
Run new webapp --no-https
* Create a docker file for this project
* Create docker commands to run and access this project started from docker locally
### Readings:
* https://blog.alexellis.io/mutli-stage-docker-builds/
### Interesting things to do:
* https://code.visualstudio.com/docs/remote/containers
