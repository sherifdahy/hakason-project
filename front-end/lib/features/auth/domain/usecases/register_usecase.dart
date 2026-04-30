import '../repositories/auth_repository.dart';

class RegisterUseCase {
  final AuthRepository repository;

  RegisterUseCase(this.repository);

  Future<void> call({
    required String fullName,
    required String email,
    required String password,
  }) {
    return repository.register(
      fullName: fullName,
      email: email,
      password: password,
    );
  }
}