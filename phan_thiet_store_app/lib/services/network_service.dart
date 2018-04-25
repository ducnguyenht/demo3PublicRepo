import 'dart:async';
import 'dart:convert';

import 'package:flutter/foundation.dart' as fo;

import 'package:http/http.dart' as http;

class NetworkService {
  static String authToken;

  Future<String> getAuthToken() async {
    if (authToken == null) {
      fo.debugPrint('get token');
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
      fo.debugPrint('got token');
      authToken = bodyMap['BearerToken'];
    }
    return authToken;
  }
}
