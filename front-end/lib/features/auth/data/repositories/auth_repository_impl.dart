import '../../domain/entities/parent_entity.dart';
import '../../domain/repositories/auth_repository.dart';
import '../datasources/auth_remote_datasource.dart';

class AuthRepositoryImpl implements AuthRepository {
  final AuthRemoteDataSource remoteDataSource;

  AuthRepositoryImpl(this.remoteDataSource);

  @override
  Future<ParentEntity> login({
    required String email,
    required String password,
  }) async {
    return await remoteDataSource.login(
      email: email,
      password: password,
    );
  }

  @override
  Future<void> registerParent({
    required String fullName,
    required String email,
    required String password,
    required String phone,
  }) async {
    await remoteDataSource.registerParent(
      fullName: fullName,
      email: email,
      password: password,
      phone: phone,
    );
  }

  @override
  Future<void> registerChild({
    required String name,
    required String email,
    required String password,
    required String avatar,
  }) async {
    await remoteDataSource.registerChild(
      name: name,
      email: email,
      password: password,
      avatar: avatar,
    );
  }

  @override
  Future<void> connectChildToParent({
    required String connectionCode,
  }) async {
    await remoteDataSource.connectChildToParent(
      connectionCode: connectionCode,
    );
  }

  @override
  Future<String> generateConnectionCode() async {
    return await remoteDataSource.generateConnectionCode();
  }
}