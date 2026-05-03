import 'package:dio/dio.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';

import 'auth/token_storage.dart';
import 'auth/auth_service.dart';
import 'api/api_client.dart';

part 'providers.g.dart';

@riverpod
TokenStorage tokenStorage(ref) => TokenStorage();

@riverpod
AuthService authService(ref) =>
    AuthService(ref.watch(tokenStorageProvider));

@riverpod
Dio apiClient(ref) => buildApiClient(
    ref.watch(tokenStorageProvider),
    ref.watch(authServiceProvider));
