import 'dart:async';
import 'dart:convert';
import 'package:http/http.dart' as http;

class NetworkService {
  static String authToken;

  Future<String> getAuthToken() async {
    if (authToken == null) {
      final String tokenUrl =
          "https://nzt.kiotviet.com/api/auth/credentials?format=json";
      var requester = new http.Client();
      var response = await requester.post(tokenUrl, body: {
        "provider": "credentials",
        "UserName": "0944505039",
        "Password": "@vanlinh123",
        "UseTokenCookie": "false"
      });
      var bodyStr = response.body;
      var bodyMap = json.decode(bodyStr);
      authToken = bodyMap['BearerToken'];
    }
    return authToken;
  }
}
