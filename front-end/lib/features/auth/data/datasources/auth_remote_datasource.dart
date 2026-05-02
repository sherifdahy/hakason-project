import 'package:dio/dio.dart';
import '../models/parent_model.dart';

abstract class AuthRemoteDataSource {
  Future<ParentModel> login({
    required String email,
    required String password,
  });

  Future<void> registerParent({
    required String fullName,
    required String email,
    required String password,
    required String phone,
  });

  Future<void> registerChild({
    required String name,
    required String email,
    required String password,
    required String avatar,
  });

  Future<void> connectChildToParent({
    required String connectionCode,
  });

  Future<String> generateConnectionCode();
}

class AuthRemoteDataSourceImpl implements AuthRemoteDataSource {
  final Dio dio;

  AuthRemoteDataSourceImpl(this.dio);

  @override
  Future<ParentModel> login({
    required String email,
    required String password,
  }) async {
    final response = await dio.post(
      '/api/Auth/get-token',
      data: {'email': email, 'password': password},
    );
    return ParentModel.fromJson(response.data);
  }

  @override
  Future<void> registerParent({
    required String fullName,
    required String email,
    required String password,
    required String phone,
  }) async {
    await dio.post(
      '/api/Auth/register',
      data: {
        'fullName': fullName,
        'email': email,
        'password': password,
        'phone': phone,
      },
    );
  }

  @override
  Future<void> registerChild({
    required String name,
    required String email,
    required String password,
    required String avatar,
  }) async {
    // TODO: endpoint not ready from backend
    throw UnimplementedError();
  }

  @override
  Future<void> connectChildToParent({
    required String connectionCode,
  }) async {
    // TODO: endpoint not ready from backend
    throw UnimplementedError();
  }

  @override
  Future<String> generateConnectionCode() async {
    // TODO: endpoint not ready from backend
    throw UnimplementedError();
  }
}