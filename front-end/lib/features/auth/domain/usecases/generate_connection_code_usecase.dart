import '../repositories/auth_repository.dart';

class GenerateConnectionCodeUseCase {
  final AuthRepository repository;

  GenerateConnectionCodeUseCase(this.repository);

  Future<String> call() {
    return repository.generateConnectionCode();
  }
}