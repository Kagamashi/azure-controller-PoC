
go.mod: A new Go module matching our project, with basic dependencies

Makefile: Make targets for building and deploying your controller

PROJECT: Kubebuilder metadata for scaffolding new components

config/ : launch configurations
  config/manager: launch controllers as pods in the cluster
  config/rbac: permissions required to run controllers under their own service account
  config/default: contains Kustomize base for launching controller

cmd/main.go: entrypoint for the project
