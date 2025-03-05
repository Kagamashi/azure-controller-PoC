
kubebuilder init --domain tutorial.kubebuilder.io --repo tutorial.kubebuilder.io/project
  based on the domain every group will be named
  so when we create new group 'batch'
  it will correspond to 'batch.tutorial.kubebuilder.io'

kubebuilder create api --group batch --version v1 --kind CronJob
  this command creates directory for new group-version
