class ConnectionCodeEntity {
  final String code;
  final DateTime expiresAt;
  final bool isExpired;

  const ConnectionCodeEntity({
    required this.code,
    required this.expiresAt,
    required this.isExpired,
  });
}