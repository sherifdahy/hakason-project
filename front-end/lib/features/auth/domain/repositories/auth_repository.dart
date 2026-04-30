import '../entities/parent_entity.dart';

abstract class AuthRepository {
  Future<ParentEntity> login({
    required String email,
    required String password,
  });

  Future<void> register({
    required String fullName,
    required String email,
    required String password,
  });
}