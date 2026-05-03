abstract final class AppConfig {
  static const keycloakBase = 'http://10.0.2.2:8180/realms/planthor';
  static const authEndpoint = '$keycloakBase/protocol/openid-connect/auth';
  static const tokenEndpoint = '$keycloakBase/protocol/openid-connect/token';
  static const endSessionUrl = '$keycloakBase/protocol/openid-connect/logout';

  static const clientId = 'planthor-ios';
  static const redirectUri = 'planthor://callback';
  static const postLogoutUri = 'planthor://callback';
  static const scopes = ['openid', 'profile', 'email', 'offline_access'];

  // Resource API
  static const apiBase = 'http://10.0.2.2:5000';
}
