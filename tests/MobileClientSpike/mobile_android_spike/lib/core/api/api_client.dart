import 'package:dio/dio.dart';

import '../auth/auth_interceptor.dart';
import '../auth/auth_service.dart';
import '../auth/token_storage.dart';
import '../config/app_config.dart';

Dio buildApiClient(TokenStorage storage, AuthService authService) {
  final dio = Dio(
    BaseOptions(
      baseUrl: AppConfig.apiBase,
      connectTimeout: const Duration(seconds: 10),
      receiveTimeout: const Duration(seconds: 30),
      headers: {'Accept': 'application/json'},
    ),
  );

  dio.interceptors.addAll([
    AuthInterceptor(storage, authService, dio),
    LogInterceptor(requestBody: true, responseBody: true), // remove in prod
  ]);

  return dio;
}
