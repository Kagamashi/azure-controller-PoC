
# OPERATOR
Operator pattern concept allows extending cluster behavior without modifying code of Kubernetes itself by linking controllers to one or more custom resources.
Operators are clients of Kubernetes API that act as controllers for Custom Resource.

Examples of what operators can automate:
- deploying app on demand
- taking and restoring backups of app state
- handling upgrades of app code alongside related changes such database schemas or extra config settings
- publishing a service to app that dont support Kubernetes APIs to discover them
- simulating failure in all or part of cluster to test its resilience


Deploying operators:
1. Add CRD and associated Controller to the cluster
2. Controller can be run as Deployment (outside of control plane)


Once operator is deployed we can add/modify/delete kind of resource that operator uses.
kubectl get SampleDB
kubectl edit SampleDB/example-database


https://kubernetes.io/docs/concepts/extend-kubernetes/operator/
https://github.com/cncf/tag-app-delivery/blob/163962c4b1cd70d085107fc579e3e04c2e14d59c/operator-wg/whitepaper/Operator-WhitePaper_v1-0.md#operator-for-gitops

---

# CONTROLLERS
control loop - non-terminating loop that regulates the state of a system
in Kubernetes, controllers are control loops that watch the state of our cluster then make or request changes where needed

Controller pattern
controller tracks at least one Kubernetes resource type
these objects have a spec field that represents the desired state

controllers for that resource are responsible for making the current state come closer to that desired state

---

# CUSTOM RESOURCES
https://kubernetes.io/docs/concepts/extend-kubernetes/api-extension/custom-resources/

resources is an endpoint in Kubernetes API that stores collection of API objects of certain kind

custom resource is an extension of Kubernetes API that is not necessarily available in default K8S installation

combining custom resource with custom controller provide true declarative API

declarative - describe what we want, without necessarily describing how to get it
imperative - step by step instructing what we want

---

# DIFFERENCE
technically no difference between typical controller and operator

controller is the implementation, and operator is the pattern of using custom controllers with CRDs

controller which spins up a pod when custom resource is created, and the pod gets destroyed afterwards can be described as SIMPLE CONTROLLER

if controller has operational knowledge like how to upgrade or remediate from errors its an
OPERATOR


# CR and CRD

custom resource are used to store and retrieve structured data in Kubernetes as an extension of default Kubernetes API

in case of operator, a CR contains desired state of the resources but does not contain implementation logic

CRD defines how such objects looks like
CRD can be scaffolded using tools as operator SDK or written by hand

'spec' desired state
'status' operator communicate useful info back to user


Control Loop - reconciliation
reconciliation loop ensures that state declared by user using CRD matches the state of application
but also that the transition between the states works as intended

---


# BIN

## others
CRDs are definitions of our custom Objects,
CRs are instances of them
APIs used to teach Kubernetes about custom objects

* Go structs are used to generate a CRD which includes schema for our data
* instances of Custom Objects are managed by controllers


groups
versions
kinds
resources - use of a Kind in API
usually its 1-to-1 relationship

* pod resource corresponds to Pod Kind
* Scale Kind is returned by all scale subresources, like deployments/scale or replicasets/scale


Spec
Status
Markers and validations
https://book.kubebuilder.io/reference/markers


make generate
creates DeepCopy files

make manifests
generate CRD manifests under config/crd/bases
and samples under config/crd/samples

---

config/crd/bases CRDs
config/crd/samples CRs

---

Reconcilliation Process
we declare desired state and then controllers continuously observe the cluster and take actions to ensure the actual state matches the desired state

Reconcialliation Process functions as a loop, continously checking conditions and performing necessary actions - this process will keep running until all conditions in the system align with the desired state

---

possible return options to restart Reconcile:

with error:
return ctrl.Result{}, err

without an error:
return ctrl.Result{Requeue: true}, nil

to stop Reconcile:
return ctrl.Result{}, nil

reconcile again after X time
return ctrl.Result{RequestAfter: nextRun.Sub(r.Now())}, nil

---

k apply -f config/sample/cache_v1alpha1_memcached.yaml
deployment should be created for our Memcached image

---

manager in cmd.main.go is responsible for managing controllers in our application

---

make generate also refresh files located under config/rbac
based on RBAC markers

---

Makefile installs tools at versions defined during project creation
kustomize
controller-gen
setup-envtest

go.mod specified dependency versions

---
