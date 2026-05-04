class ChildEntity {
  final String id;
  final String name;
  final String email;
  final String avatar;
  final int? age;

  const ChildEntity({
    required this.id,
    required this.name,
    required this.email,
    required this.avatar,
    this.age,
  });
}