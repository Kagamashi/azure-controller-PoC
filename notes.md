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

--

make generate also refresh files located under config/rbac
based on RBAC markers

---

Makefile installs tools at versions defined during project creation
kustomize
controller-gen
setup-envtest

go.mod specified dependency versions

