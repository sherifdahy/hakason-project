abstract class Failure {
  final String message;
  const Failure(this.message);
}
class ServerFailure  extends Failure { const ServerFailure(super.message); }
class NetworkFailure extends Failure { const NetworkFailure([super.message = 'No internet']); }
class CacheFailure   extends Failure { const CacheFailure(super.message); }
class AuthFailure    extends Failure { const AuthFailure(super.message); }
class OtpFailure     extends Failure { const OtpFailure(super.message); }
