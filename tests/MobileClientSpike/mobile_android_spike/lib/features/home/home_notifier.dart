import 'package:dio/dio.dart';
import 'package:riverpod_annotation/riverpod_annotation.dart';

import '../../core/providers.dart';

part 'home_notifier.g.dart';

@riverpod
class HomeNotifier extends _$HomeNotifier {
  @override
  Future<String> build() async {
    return 'Tap the button to call a protected API';
  }

  Future<void> callProtectedApi() async {
    state = const AsyncLoading();
    state = await AsyncValue.guard(() async {
      final dio = ref.read(apiClientProvider);
      try {
        final response = await dio.get('/weatherforecast');
        return 'Status: ${response.statusCode}\n\n${response.data}';
      } on DioException catch (e) {
        if (e.response != null) {
          return 'Error ${e.response!.statusCode}: ${e.response!.data}';
        }
        rethrow;
      }
    });
  }
}
