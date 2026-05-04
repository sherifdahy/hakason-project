import '../repositories/auth_repository.dart';

class RegisterParentUseCase {
  final AuthRepository repository;

  RegisterParentUseCase(this.repository);

  Future<void> call({
    required String fullName,
    required String email,
    required String password,
    required String phone,
  }) {
    return repository.registerParent(
      fullName: fullName,
      email: email,
      password: password,
      phone: phone,
    );
  }
}