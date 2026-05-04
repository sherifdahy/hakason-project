import '../repositories/auth_repository.dart';

class RegisterChildUseCase {
  final AuthRepository repository;

  RegisterChildUseCase(this.repository);

  Future<void> call({
    required String name,
    required String email,
    required String password,
    required String avatar,
  }) {
    return repository.registerChild(
      name: name,
      email: email,
      password: password,
      avatar: avatar,
    );
  }
}