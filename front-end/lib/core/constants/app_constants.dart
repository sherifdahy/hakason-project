class AppConstants {
  AppConstants._();

  static const String baseUrl               = 'https://your-api.kidbridge.com/api/v1';
  static const int    otpLength             = 4;
  static const int    otpExpirySeconds      = 60;
  static const int    defaultDailyLimitMins = 120;
  static const String defaultBedtimeLock    = '20:00';
  static const String defaultBedtimeOpen    = '07:00';
  static const List<int> childAgeOptions    = [5, 6, 7, 8];
  static const List<String> avatarEmojis    = ['🐼','🦊','🐸','🤖','🦄','🐧'];
  static const int totalRegisterSteps       = 5;
}
