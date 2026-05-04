import '../repositories/auth_repository.dart';

class ConnectChildUseCase {
  final AuthRepository repository;

  ConnectChildUseCase(this.repository);

  Future<void> call({
    required String connectionCode,
  }) {
    return repository.connectChildToParent(
      connectionCode: connectionCode,
    );
  }
}