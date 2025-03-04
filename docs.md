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


