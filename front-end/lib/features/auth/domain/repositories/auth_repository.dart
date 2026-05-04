import '../entities/parent_entity.dart';

abstract class AuthRepository {
  // ── Parent ──────────────────────────
  Future<ParentEntity> login({
    required String email,
    required String password,
  });

  Future<void> registerParent({
    required String fullName,
    required String email,
    required String password,
    required String phone,
  });

  // ── Child ───────────────────────────
  Future<void> registerChild({
    required String name,
    required String email,
    required String password,
    required String avatar,
  });

  Future<void> connectChildToParent({
    required String connectionCode,
  });

  // ── Connection Code ──────────────────
  Future<String> generateConnectionCode();
}