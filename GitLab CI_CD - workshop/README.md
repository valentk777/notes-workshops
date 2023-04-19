# Workshop – GitLab CI/CD 
* Duration: 3 days
* Level: Medium
* Language: Lithuanian + English
* Speaker: [Valentinas Kaminskas](https://github.com/valentk777)

# Day 1

## Preparation
* Install docker desktop
* Get all files from this workshop. It will contains demo project.
* GitLab has really fantastic documentation: https://docs.gitlab.com/ee/ci/
* Cheatsheet: https://cheatography.com/violaken/cheat-sheets/gitlab-ci-cd-pipeline-configuration/

## Workshop notes

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

# copy all files from current dir to src file in docker file
COPY . ./src

# create (if needed) and go to dirrectory
WORKDIR /src

# use dotnet publish because dotnet publish will always guarantee that
your build will run.
# use publish forlder for result because this is fixed file name for
stable access in the future
RUN dotnet publish -c Release -o /publish

# run script in separate layer
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as base

# copy only results from isolated container
COPY --from=build /publish .
# not really needed, but usefull to know in the future
EXPOSE 80

# run actual publish script
ENTRYPOINT ["dotnet", "DotNet.Docker.dll"]
```
* builder vs multi-stage-docker-build: https://blog.alexellis.io/mutli-stage-docker-builds/
* the second half of the video will cover GitLab CI/CD general overview:

## Post exercises
* dotnet build vs dotnet publish
* play around with GitLab CI jobs and stages. Get familiar with the differences between stages and jobs.

### Readings:
* https://blog.alexellis.io/mutli-stage-docker-builds/

# Day 2

## Preparation
* Install docker desktop
* GitLab has really fantastic documentation: https://docs.gitlab.com/ee/ci/
* Cheatsheet: https://cheatography.com/violaken/cheat-sheets/gitlab-ci-cd-pipeline-configuration/

## Workshop notes
* post exercises recap
* types: hashes vs arrays
* GitLab pre-defined stages
* GitLab environment variables
* GitLab stage 'default'
* GitLab .pre/.post stages

```yaml
# stages is array, so you override them
stages:
- stage1
- stage2

# .pre stages are guaranteed to be the first stage in a pipeline
job-always-first:
image: alpine # required
stage: .pre # required
variables:
ENVIRONMENT: "test"
script: # required
# if you want to print current env variables, you can do this as a
step in you pipeline
# - set
# - env
# - printenv
- echo "Hello, $GitLab_USER_LOGIN. You are in $ENVIRONMENT
environment"
tags:
- Your-Runner

job-1-stage1:
image: alpine
stage: stage1
variables:
ENVIRONMENT: "test"
script:
- echo "Hello, $GitLab_USER_LOGIN. You are in $ENVIRONMENT
environment"
tags:
- Your-Runner

job-2-stage1:
image: alpine
stage: stage1
variables:
ENVIRONMENT: "test"
script:
- echo "Hello, $GitLab_USER_LOGIN. You are in $ENVIRONMENT
environment"
tags:
- Your-Runner

job-1-stage2:
image: alpine
stage: stage2
variables:
ENVIRONMENT: "random"
script:
- echo "Hello, $GitLab_USER_LOGIN. You are in $ENVIRONMENT
environment"
tags:
- Your-Runner

# .post stages are guaranteed to be the last stage in a pipeline
job-always-last:
image: alpine
stage: .post
variables:
ENVIRONMENT: "production"
script:
- echo "Hello, $GitLab_USER_LOGIN. You are in $ENVIRONMENT
environment"
tags:
- Your-Runner
```

## Post exercises
* Get familiarized with everything we learned in these workshops

### Readings:
* https://docs.gitlab.com/ee/ci/yaml/#stages
* https://docs.gitlab.com/ee/ci/variables/predefined_variables.html
* https://docs.gitlab.com/ee/ci/yaml/#default
* https://docs.gitlab.com/ee/ci/yaml/#before_script
* https://docs.gitlab.com/ee/ci/yaml/#after_script
* https://docs.gitlab.com/ee/ci/yaml/#rules
* https://docs.gitlab.com/ee/ci/yaml/#when
* https://docs.gitlab.com/ee/ci/yaml/#needs
* Write your own multiple stages and multiple jobs in the same stage pipeline. Would be nice if you add some meaningful script,
before_script and after_script codes.

# Day 3

## Preparation
* Create your own GitLab pipeline with a meaningful purpose using the scheduled GitLab run.

## Workshop notes
* post exercises recap
* GitLab environment keyword
* GitLab rules keyword
* GitLab CLI option

```yaml
# Environment
default:
image: alpine
tags:
- Your-Runner
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
stages:
- stage1
- stage2
job-1-stage1:
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
job-2-stage1:
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
job-1-stage2-ci:
stage: stage2
rules:
- if: $CI_COMMIT_REF_NAME=="Valentinas"
when: on_success
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
environment:
name: ci # case sensitive!!!
allow_failure: true
job-1-stage2-uat:
stage: stage2
rules:
- if: $CI_COMMIT_REF_NAME=="Valentinas"
when: on_success
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
environment:
name: uat # case sensitive!!!
allow_failure: true
job-1-stage2-production:
stage: stage2
rules:
- if: $CI_COMMIT_REF_NAME=="Valentinas"
when: on_success
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
environment:
name: production # case sensitive!!!
allow_failure: true
# Rules
default:
image: alpine
tags:
- Your-Runner
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
stages:
- stage1
- stage2
job-1-stage1:
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
job-2-stage1:
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
job-1-stage2-ci:
stage: stage2
rules:
- if: $TEST_ENV=="ci_env"
when: on_success
- if: $CI_MERGE_REQUEST_ID && $CI_COMMIT_REF_NAME =~ /^review-/
when: manual
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
environment:
name: ci # case sensitive!!!
allow_failure: true
job-1-stage2-uat:
stage: stage2
rules:
- if: $TEST_ENV=="uat_env"
when: on_success
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
environment:
name: uat # case sensitive!!!
allow_failure: true
job-1-stage2-production:
stage: stage2
rules:
- if: $TEST_ENV=="production_env"
when: on_success
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
environment:
name: production # case sensitive!!!
allow_failure: true
```

## Post exercises
* Get familiarized with everything we learned in this workshop.


# Day 4

## Preparation
* None

## Workshop notes
* GitLab extends keyword
* GitLab hidden job
* GitLab anchors
* Current DevOps prepared scripts that could be re-used
* GitLab include keyword

```yaml
# YAML anchors V1
default:
image: alpine
tags:
- Your-Runner
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
stages:
- stage1
- stage2
job-1-stage1: &job-1-stage1
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
job-2-stage1:
<<: *job-1-stage1
job-1-stage2-ci: &job-1-stage2-ci
stage: stage2
rules:
- if: $CI_COMMIT_REF_NAME=="Valentinas"
when: on_success
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
environment:
name: ci
allow_failure: true
job-1-stage2-uat:
<<: *job-1-stage2-ci
environment:
name: uat
job-1-stage2-production:
<<: *job-1-stage2-ci
environment:
name: production
# YAML anchors V2
default:
image: alpine
tags:
- Your-Runner
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
stages:
- stage1
- stage2
.stage1-shared: &stage1-shared
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
job-1-stage1:
<<: *stage1-shared
job-2-stage1:
<<: *stage1-shared
.stage2-shared: &stage2-shared
stage: stage2
rules:
- if: $CI_COMMIT_REF_NAME=="Valentinas"
when: on_success
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
environment:
name: ci
allow_failure: true
job-1-stage2-ci:
<<: *stage2-shared
environment:
name: ci
job-1-stage2-uat:
<<: *stage2-shared
environment:
name: uat
job-1-stage2-production:
<<: *stage2-shared
environment:
name: production
# Extends v1
default:
image: alpine
tags:
- Your-Runner
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
stages:
- stage1
- stage2
job-1-stage1:
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
job-2-stage1:
extends: job-1-stage1
job-1-stage2-ci:
stage: stage2
rules:
- if: $CI_COMMIT_REF_NAME=="Valentinas"
when: on_success
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
environment:
name: ci
allow_failure: true
job-1-stage2-uat:
extends: job-1-stage2-ci
environment:
name: uat
job-1-stage2-production:
extends: job-1-stage2-ci
environment:
name: production
# Extends V2
default:
image: alpine
tags:
- Your-Runner
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
stages:
- stage1
- stage2
.stage1-shared:
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
job-1-stage1:
extends: .stage1-shared
job-2-stage1:
extends: .stage1-shared
.stage2-shared:
stage: stage2
rules:
- if: $CI_COMMIT_REF_NAME=="Valentinas"
when: on_success
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
job-1-stage2-ci:
extends: .stage2-shared
environment:
name: ci
job-1-stage2-uat:
extends: .stage2-shared
environment:
name: uat
job-1-stage2-production:
extends: .stage2-shared
environment:
name: production
```

From now, all our pipelines will be split to separate files:

my_build.yaml

```yaml
.stage1-shared:
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
job-1-stage1:
extends: .stage1-shared
job-2-stage1:
extends: .stage1-shared

```
my_push.yaml
```yaml
.stage2-shared:
stage: stage2
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
allow_failure: true
job-1-stage2-ci:
extends: .stage2-shared
environment:
name: ci
job-1-stage2-uat:
extends: .stage2-shared
environment:
name: uat
job-1-stage2-production:
extends: .stage2-shared
environment:
name: production

```

main: .gitlab-ci.yml

```yaml
# Include
include:
# NOTE: will need for your homeworks
- project: 'gitlab/templates/ci.cd
/docker.build.push'
file: 'ecr-push.yml'
ref: master
# include local files
- local: 'my_build.yaml'
- local: 'my_push.yaml'
stages:
- stage1
- stage2
default:
image: alpine
tags:
- Your-Runner
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
```
## Post exercises
* Get familiarized with everything we learned in this workshop day.
* Pay attention to how to change default inheritance. This is not used anywhere in our pipelines (not at least I have seen) and not sure this
is a best practice, but just for information
    *  https://docs.gitlab.com/ee/ci/jobs/index.html#control-the-inheritance-of-default-keywords-and-global-variables
* Pay attention to YAML anchors for scripts. Pretty nice way to share only the script part. Not covered during the workshop, so
try this by ourselves.
    * https://docs.gitlab.com/ee/ci/yaml/yaml_optimization.html#yaml-anchors-for-scripts
* Try to improve your existing pipeline by adding reusable DevOps prepared scripts in order to create your application docker
image and push this image to sandbox ECR


### Readings:
* https://docs.gitlab.com/ee/ci/yaml/yaml_optimization.html#anchors
* https://docs.gitlab.com/ee/ci/yaml/index.html#extends
* https://docs.gitlab.com/ee/ci/yaml/#include


# Day 5

## Preparation
* None

## Workshop notes
* GitLab artifacts
```yaml
stages:
- stage1
- stage2
default:
image: alpine
tags:
- Your-Runner
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
.stage1-shared:
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
artifacts:
paths:
- build/
job-1-stage1:
extends: .stage1-shared
script:
- mkdir build
- echo "INITIAL TEXT" > build/info1.txt
- cat build/info1.txt

job-2-stage1:
extends: .stage1-shared
script:
- mkdir build
- echo "INITIAL TEXT 2" > build/info2.txt
- cat build/info2.txt
.stage2-shared:
stage: stage2
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
- echo "print $TEST_ENV"
- cat build/info1.txt
- echo "Some text here." > build/info1.txt
- cat build/info1.txt
- cat build/info2.txt
- echo "Some text here 2." > build/info2.txt
- cat build/info2.txt
allow_failure: true
# this will say that you want to same your artifacts
artifacts:
name: updated_artifacts # name of the file you download
paths:
- build/
job-1-stage2-ci:
extends: .stage2-shared
environment:
name: ci
artifacts:
exclude:
- build/*2.txt

job-1-stage2-uat:
extends: .stage2-shared
environment:
name: uat
artifacts:
expire_in: 10 seconds
job-1-stage2-production:
extends: .stage2-shared
environment:
name: production
```

* GitLab keyword reference

```yaml
stages:
- stage1
- stage2
default:
image: alpine
tags:
- Your-Runner
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
.stage1-shared:
stage: stage1
variables:
JOB_ENV_VARIABLE: "job local variable"
script:
- echo "Hello, $GITLAB_USER_LOGIN. You use $JOB_ENV_VARIABLE
variable"
allow_failure: true
job-1-stage1:
extends: .stage1-shared
script:
- echo "Additional script section"
# NOTE: you can inherit array type and reuse inherited script
inside your own script
- !reference [.stage1-shared, script]
- echo "Additional script section"
```
* GitLab trigger

Possible inputs:
* For multi-project pipelines, the path to the downstream project.
* For child pipelines, path to the child pipeline CI/CD configuration file.

<!-- TODO: IMPORT IMAGE -->
@import "Images\microservices.png"

```yaml
stages:
- stage1
- stage2
default:
image: alpine
tags:
- YOUR-RUNNER
before_script:
- echo "this is before script"
after_script:
- echo "this is after script"
trigger_remote:
trigger:
project: YOUR_REPO/learning.gitlabci.trigger
branch: main # optional
strategy: depend # optional
trigger_local:
trigger:
include:
- local: 'my_build.yaml'
```

## Post exercises
* Get familiarized with everything we learned in this workshop
* Additionally read about the GitLab
    * cache: https://blog.nimbleways.com/let-s-make-faster-gitlab-ci-cd-pipelines/
    * parallel: https://docs.gitlab.com/ee/ci/yaml/#parallel

### Readings:
* https://docs.gitlab.com/ee/ci/yaml/yaml_optimization.html#reference-tags
* https://docs.gitlab.com/ee/ci/yaml/#artifacts




<!-- TODO: MOVE TO FILES AND FORMAT -->
[plugin: filesource] (DemoFiles/.gitlab-ci.yml)

file: DemoFiles/.gitlab-ci.yml

@import "DemoFiles/.gitlab-ci.yml"

https://github.com/valentk777/notes-workshops/blob/cf9fcf358246cc89e5666675a3da4800c100a5d8/GitLab%20CI_CD%20-%20workshop/DemoFiles/.gitlab-ci.yml#L1-L136