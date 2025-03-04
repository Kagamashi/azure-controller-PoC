
API definitions
api/v1/

reconciliation business logic for Kind(CRD)
internal/controllers/

after editing API definitions generate manifests such as CRs or CRDs:
make manifests


run agains cluster:
make install
make run

install custom resoure:
k apply -k config/samples/


build and push controller to docker registry/acr:
make docker-build docker-push IMG=<some-registry>/<project-name>:tag

deploy via image:
make deploy IMG=<some-registry>/<project-name>:tag

