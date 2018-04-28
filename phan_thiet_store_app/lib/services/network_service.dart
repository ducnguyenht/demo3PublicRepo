import 'dart:async';
import 'dart:convert';

import 'package:flutter/foundation.dart' as fo;

import 'package:http/http.dart' as http;

import '../models/config.dart';

class NetworkService {
  static String authToken;

  Future<String> getAuthToken() async {
    if (authToken == null) {
      var config = new Config.getTestConfig();
      fo.debugPrint('get token');
      final String tokenUrl = config.getFullUrl("/api/auth/credentials?format=json");
      var requester = new http.Client();
      var response = await requester.post(tokenUrl, body: {
        "provider": "credentials",
        "UserName": config.tokenUserName,
        "Password": config.tokenPassword,
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
