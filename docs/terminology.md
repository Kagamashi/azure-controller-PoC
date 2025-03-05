
Groups
collection of related functionality - each group has one or more versions

Versions

Kinds
API type

CRD

CR

controller

operator

Scheme
a way to keep track of what Go type corresponds to a given GVK

---

Example":
mark "tutorial.kubebuilder.io/api/v1".CronJob{} type as being in the
batch.tutorial.kubebuilder.io/v1 API group (it implicitly says it has Kind CronJob)

later we can construct new &CronJob{} given some JSON from API server:
'''json
{
    "kind": "CronJob",
    "apiVersion": "batch.tutorial.kubebuilder.io/v1",
    ...
}
'''

