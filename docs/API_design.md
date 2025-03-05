
- serialized fields must be camelCase
- omitempty struct tag to mark a field should be omitted from serialization when empty

- fields support most of primitive types, for numbers:
  - int32, int64, resource.Quantity
  - metav1.Time (like time.Time but has fixed portable serialization format)
